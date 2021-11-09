using System;
using System.Windows.Forms;

namespace csshack
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppForm app = new AppForm();
            Application.Run(app);
        }
    }
}
