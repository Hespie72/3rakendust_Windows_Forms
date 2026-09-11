using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3rakendust_Windows_Forms
{
    public partial class mathquizz : Form
    {
        public mathquizz(string name, int width, int height)
        {
            this.Text = name;
            this.Width = width;
            this.Height = height;
        }
    }
}
