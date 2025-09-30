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

namespace AmazonScrapper.Dialog
{
    public partial class frmSearch : BaseForm
    {
        public string SearchText { get; set; }
        public bool SortByDate { get; set; }
        public bool StrictSearch { get; set; }

        public frmSearch(string searchTerm = "", bool sortByDate = false, bool strictSearch = true)
        {
            InitializeComponent();

            txtSearchTerm.Text = searchTerm;
            chkSortDate.Checked = sortByDate;
            chkStrictSearch.Checked = strictSearch;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SearchText = txtSearchTerm.Text;
            SortByDate = chkSortDate.Checked;
            StrictSearch = chkStrictSearch.Checked;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSearch_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 100, this.Location.Y);
        }
    }
}
