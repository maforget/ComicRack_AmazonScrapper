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

namespace AmazonScrapper.Dialog
{
    public partial class frmIssue : BaseForm
    {
        public AmazonLinkSerie Link { get; }
        public AmazonBookInfo BookInfo { get; private set; }
        public CancellationToken Token { get; private set; }
        bool doSearchOnOpen = false;

        public frmIssue(AmazonLinkSerie link, bool doSearchOnOpen, CancellationToken ct = default)
        {
            InitializeComponent();
            this.doSearchOnOpen = doSearchOnOpen;
            Link = link;
            label.Text = $"The following issues where found for {link.SerieDisplayText}. {label.Text}";
            this.Text = link.SerieDisplayText;
            List<AmazonLinkIssues> issues = null;
            Token = ct;
            Token.Register(() => this.SafeInvoke(x =>
            {
                x.Close();
                x.Dispose();
            }));

            issues = DoSearch(link, issues);
        }

        private List<AmazonLinkIssues> DoSearch(AmazonLinkSerie link, List<AmazonLinkIssues> issues)
        {
            try
            {
                if (Token.IsCancellationRequested)
                    Token.ThrowIfCancellationRequested();

                var task = Task.Run(() =>
                {
                    using (HourGlass hourglass = new HourGlass(this))
                    {
                        issues = link.GetIssues(Token);
                        sourceResults.SetBindingSourceDataSource(issues, this);
                        LoadImage();
                    }
                }, Token);
                //task.Wait(Token);
            }
            catch (OperationCanceledException ex)
            {
                this.Close();
            }

            return issues;
        }

        #region Button Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            SetCurrentBookInfo();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BookInfo = null;
            this.Close();
        }
        #endregion

        #region DataViewGrid Events
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
        #endregion

        #region Helper Methods
        private void SetCurrentBookInfo()
        {
            using (HourGlass hourGlass = new HourGlass(this))
            {
                BookInfo = null;
                AmazonLinkIssues link = GetCurrentRow() as AmazonLinkIssues;
                if (link != null)
                    BookInfo = link.ScrapeData();
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
        #endregion

        private void frmIssue_Shown(object sender, EventArgs e)
        {
            if (doSearchOnOpen)
            {
                //TODO: have better positional logic, test for DPI
                this.Location = new Point(this.Location.X + 190, this.Location.Y);
            }
        }
    }
}
