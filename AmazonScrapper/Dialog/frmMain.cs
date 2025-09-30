using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmazonScrapper.Web;
using AmazonScrapper.Data;
using AmazonScrapper.Tools;
using System.Threading;
using AmazonScrapper.Settings;
using System.Diagnostics;
using AmazonScrapper.Web.Searcher;

namespace AmazonScrapper.Dialog
{
	public partial class frmMain : BaseForm
    {
        #region Properties
        private bool doSearchOnOpen = false;
        public BindingList<AmazonLink> Results { get; set; }
        public string SearchText { get; private set; }
        public string SearchNumber { get; private set; }
        public bool SortByDate { get; private set; }
        public bool StrictSearch { get; private set; } = false;
        public TLDs Domain { get; private set; } = TLDs.com;
		public bool GroupBySerie { get; private set; }
        public AmazonBookInfo BookInfo { get; private set; }
        public CancellationToken Token { get; }
        bool IsSkipped = true;

        public event EventHandler BookChosen;
        public event EventHandler BookSkipped;
        #endregion

        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            SetTitleBar();
			CreateDomainList();
            SetConfigItems();
		}

        public frmMain(string searchText, string searchNumber, CancellationToken token = default)
            : this()
        {
            SearchNumber = searchNumber == "-1" ? string.Empty: searchNumber;
            SearchText = $"{searchText} {SearchNumber}".Trim();
            doSearchOnOpen = true;
            Token = token;
            Token.Register(() => this.SafeInvoke(x => x.Close()));

			if (Token.IsCancellationRequested)
                Token.ThrowIfCancellationRequested();
        }
        #endregion

        private void DoSearch()
        {

            try
            {
                if (Token.IsCancellationRequested)
                    Token.ThrowIfCancellationRequested();

                var task = Task.Run(() =>
                {
                    using (HourGlass hourglass = new HourGlass(this))
                    {
                        ISearcher searcher = Searcher.Create(SearchText, SortByDate, StrictSearch, Domain);
                        Results = searcher.GetResults(Token);
                        ChangeGrouping();
                    }
                }, Token);
                //task.Wait(Token);
            }
            catch (OperationCanceledException ex)
            {
                this.Close();
            }

        }

        #region Subscribed Events
        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.Activate();

            //TODO: have better positional logic, test for DPI
            if (doSearchOnOpen)
            {
                this.Location = new Point(this.Location.X + 190, this.Location.Y);
                DoSearch();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var frmSearch = new frmSearch(SearchText, SortByDate, StrictSearch);
            frmSearch.ShowDialog(this);

            if (frmSearch.DialogResult == DialogResult.OK)
            {
                SearchText = frmSearch.SearchText;
                SortByDate = frmSearch.SortByDate;
                StrictSearch = frmSearch.StrictSearch;
                DoSearch();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (GroupBySerie)
                btnIssues_Click(sender, e);
            else
                SetCurrentBookInfo();

			this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BookInfo = null;
			this.Close();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsSkipped)
				OnBookSkipped(EventArgs.Empty);

			SaveSetting();
			IsSkipped = true; //Reset flag
		}

		private void btnIssues_Click(object sender, EventArgs e)
        {
            AmazonLinkSerie link = GetCurrentRow() as AmazonLinkSerie;
            if (link != null)
            {
                frmIssue frm = new frmIssue(link, doSearchOnOpen, Token);
                frm.ShowDialog(this);

                if (frm.DialogResult == DialogResult.OK)
                {
                    BookInfo = frm.BookInfo;
                    this.DialogResult = DialogResult.OK;
					IsSkipped = false;
                    OnBookChosen(EventArgs.Empty);
					this.Close();
                }
            }
        }
        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfig frm = new frmConfig();
            frm.ShowDialog(this);
        }

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResults.Focused)
                LoadImage();
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AmazonLink link = GetCurrentRow();
            if (link != null)
                System.Diagnostics.Process.Start(link.Link);
        }

        private void chkGroupBySerie_CheckedChanged(object sender, EventArgs e)
		{
			GroupBySerie = chkGroupBySerie.Checked;
			ChangeGrouping();
		}

		private void SaveSetting()
		{
			var user = new OtherConfig();
			user.GroupBySerie = chkGroupBySerie.Checked;
            user.TLD = Domain;
			user.WriteOtherConfigToFile();
		}

		private void cbDomains_SelectedIndexChanged(object sender, EventArgs e)
		{
			Domain = cbDomains.SelectedIndex < 0 ? TLDs.com : (TLDs)cbDomains.SelectedIndex;
		}
		#endregion

		#region Form Events
		protected virtual void OnBookChosen(EventArgs e)
        {
            BookChosen?.Invoke(this, e);
        }

        protected virtual void OnBookSkipped(EventArgs e)
        {
            BookSkipped?.Invoke(this, e);
        }
        #endregion

        #region Methods
        private void SetCurrentBookInfo()
        {
            using (HourGlass hourGlass = new HourGlass(this))
            {
                BookInfo = null;
                AmazonLink link = GetCurrentRow();
                if (link != null)
                {
                    BookInfo = GroupBySerie ? null : ((AmazonLinkIssues)link).ScrapeData(Token);
					IsSkipped = false;
					OnBookChosen(EventArgs.Empty);
                }
            }
        }

        private AmazonLink GetCurrentRow()
        {
            int current = dgvResults.SelectedRows.Count > 0 ? dgvResults.SelectedRows[0].Index : 0;

            if (dgvResults.RowCount > 0 && current >= 0)
                return dgvResults.Rows[current].DataBoundItem as AmazonLink;

            return null;
        }

        private void LoadImage()
        {
            pbCover.Image = null;

            AmazonLink link = GetCurrentRow();
            if (link != null)
                pbCover.SafeInvoke(x => x.Image = Fetcher.Instance.GetImage(link.ImageLink));
        }

        private void ChangeGrouping()
        {
            if (Results != null)
            {
                sourceResults.SetBindingSourceDataSource(null, this);
                sourceResults.SetBindingSourceDataSource(GroupBySerie ? Searcher.GroupResultsBySerie(Results) : Results, this);
            }
            LoadImage();
            SetIssueButtonVisibility();
            SetLabelText();
        }

        private void SetLabelText()
        {
            var text = string.Empty;

            if (Results?.Count > 0)
                text = $"Searched for '{SearchText}' and found {sourceResults.Count} matching {(GroupBySerie ? "series" : "issues")}. Please choose one from the list below, or click 'Skip' to scrape the comic later. Double-Click to open the Amazon page in your browser. Click Group by Series, to see only the series, an Issues button will appear giving the opportunity to select an issue. When in Group series mode, the double-click will get you to the collection page.";
            else if (Results != null)
                text = $"Searched for '{SearchText}' and nothing found, press the Search button and modify your query.";

            label.SafeInvoke(l => l.Text = text);
        }

        private void SetIssueButtonVisibility()
        {
            if (GroupBySerie)
                btnIssues.SafeInvoke(i => { i.Enabled = true; i.Visible = true; });
            else
                btnIssues.SafeInvoke(i => { i.Enabled = false; i.Visible = false; });
        }

        private void SetConfigItems()
        {
            var user = Config.ReadOtherConfigFromFile();
            if (user == null)
                return;

            chkGroupBySerie.Checked = user.GroupBySerie;
            cbDomains.SelectedIndex = (int)user.TLD;
        }

        private void SetTitleBar()
        {
            var title = Tools.Version.GetCurrentVersionInfo();
            this.Text = $"{title} - {this.Text}";
        }

		private void CreateDomainList()
		{
			var domainList = new List<string>();
			foreach (TLDs t in Enum.GetValues(typeof(TLDs)))
				domainList.Add($"amazon.{t}");

			cbDomains.Items.AddRange(domainList.ToArray());
			cbDomains.SelectedIndex = 0;
		}
		#endregion
	}
}
