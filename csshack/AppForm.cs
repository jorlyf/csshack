using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace csshack
{
    internal partial class AppForm : Form
    {
        #region TransparentWindow
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion
        Graphics Graphics { get; set; }
        public AppForm()
        {
            InitializeComponent();

            this.BackColor = Color.LimeGreen;
            this.TransparencyKey = Color.LimeGreen;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            Graphics = this.CreateGraphics();
            Hack hack = new Hack(this);
            Thread hackThread  = new Thread(hack.Run);
            hackThread.Start();
        }

        private void AppForm_Paint(object sender, PaintEventArgs e)
        {

        }

        public void DrawBox(Rectangle rectangle, Color color)
        {
            Graphics.DrawRectangle(new Pen(color, 3.5f), rectangle);
        }
        public void Clear()
        {
            Graphics.Clear(Color.LimeGreen);
        }
    }
}
