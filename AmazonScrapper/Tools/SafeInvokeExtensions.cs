using System;
using System.Windows.Forms;

namespace AmazonScrapper.Tools
{
    public static class SafeInvokeExtensions
    {
        public static void SafeInvoke<T>(this T c, Action<T> action) where T : Control
        {
            if (c.InvokeRequired)
                c.Invoke(new Action<T, Action<T>>(SafeInvoke), new object[] { c, action });
            else
                action(c);
        }

        public static void SetBindingSourceDataSource(this BindingSource bs, object newDataSource, Control control)
        {
            if (control.InvokeRequired)
                control.Invoke(new Action<BindingSource, object, Control>(SetBindingSourceDataSource),
                       new object[] { bs, newDataSource, control });
            else
                bs.DataSource = newDataSource;
        }
    }
}
