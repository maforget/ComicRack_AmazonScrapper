using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AmazonScrapper.Settings;

namespace AmazonScrapper.Dialog
{
    public partial class frmConfig : Form
    {
        Fields user = null;
        Dictionary<string, UserConfig> dictionary = new Dictionary<string, UserConfig>();

        public frmConfig()
        {
            InitializeComponent();
            AddCheckboxFromUser();
        }

        private void AddCheckboxFromUser()
        {
            user = Config.ReadUserFromFile();
            dictionary = user.ToDictionary();
            if (dictionary == null)
                return;

            int locX = 12, stepX = 120;
            int locY = 12, stepY = 24, numItems = 6;
            int maxY = locY, limitY = locY + (numItems * stepY), startY = locY;
            foreach (var item in dictionary)
            {
                CheckBox chk = new CheckBox();
                chk.Name = item.Key;
                chk.Text = item.Value.Text;
                chk.Checked = item.Value.Enabled;
                chk.AutoSize = true;
                chk.Location = new Point(locX, locY);
                this.Controls.Add(chk);
                this.Size = new Size(locX + stepX, maxY + 100);
                locX = locY >= limitY ? locX + stepX : locX;
                maxY = locY > maxY ? locY : maxY;
                locY = locY >= limitY ? startY : locY + stepY;
            }
        }

        private void GetUserFromCheckbox()
        {
            dictionary = new Dictionary<string, UserConfig>();
            foreach (var item in this.Controls)
            {
                if (item.GetType() != typeof(CheckBox))
                    continue;

                CheckBox chk = (CheckBox)item;
                dictionary.Add(chk.Name,  new UserConfig(chk.Name, chk.Text, chk.Checked));
            }

            user = dictionary.ToConfig<Fields>();
            user.WriteUserToFile();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            GetUserFromCheckbox();
            this.Close();
        }

        private void frmConfig_Shown(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X - 100, this.Location.Y);
        }
    }
}
