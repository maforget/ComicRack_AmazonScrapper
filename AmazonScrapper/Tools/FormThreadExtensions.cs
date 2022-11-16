using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmazonScrapper.Tools
{
    public static class FormThreadExtensions
    {
        public static void RunOnNewThread(this Form frm)
        {
            try
            {
                var t = new Thread(new ThreadStart(() => Application.Run(frm)));
                t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                //t.Join();
            }
            catch (Exception)
            {
                frm.Close();
                frm.Dispose();
            }
        }

        public static void ShowDialogOnNewThread(this Form frm)
        {
            try
            {
                var t = new Thread(new ThreadStart(() => frm.ShowDialog()));
                //t.IsBackground = true;
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                //t.Join();
            }
            catch (Exception)
            {
                frm.Close();
                frm.Dispose();
            }
        }
    }
}
