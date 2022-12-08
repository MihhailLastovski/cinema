using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    class formparam
    {
        private Color backcolorform = Color.FromArgb(114, 114, 114);
        private int width = 1200;
        private int height = 700;
        public Color _backcolorform{ get {  return backcolorform; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
    }
}
