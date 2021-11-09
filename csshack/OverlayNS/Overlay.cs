using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csshack.OverlayNS
{
    internal class Overlay
    {
        private AppForm AppForm { get; }
        private Graphics Graphics { get; }
        public Overlay(AppForm form)
        {
            AppForm = form;
            Graphics = AppForm.CreateGraphics();
        }


    }
}
