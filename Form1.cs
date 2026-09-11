using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _3rakendust_Windows_Forms
{
    public partial class Form1 : Form
    {
        TreeView tree;
        TableLayoutPanel tbllp;
        private Button nuppFoto;
        private Button nuppMat;
        private Button nuppMang;
        
        public Form1()
        {
            Height = 600;
            Width = 1000;
            Text = "3 rakendust";


            nuppFoto = new Button();
            nuppFoto.Text = "Ava pildi näita aken";
            nuppFoto.Location = new Point(50, 30);
            nuppFoto.Size = new Size(280, 40);
            nuppFoto.Click += NuppFoto_Click;

            nuppMat = new Button();
            nuppMat.Text = "Ava matematika ülessaned aken";
            nuppMat.Location = new Point(50, 80);
            nuppMat.Size = new Size(280, 40);
            nuppMat.Click += NuppMat_Click;

            nuppMang= new Button();
            nuppMang.Text = "Ava mängu aken";
            nuppMang.Location = new Point(50, 130);
            nuppMang.Size = new Size(280, 40);
            nuppMang.Click += NuppMang_Click;

            Controls.Add(nuppFoto);
            Controls.Add(nuppMat);
            Controls.Add(nuppMang);
        }

        private void NuppMang_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NuppMat_Click(object sender, EventArgs e)
        {
            mathquizz mquiz = new mathquizz("Matematika ülesaned", 1000, 600);
            mquiz.Show();
        }

        private void NuppFoto_Click(object sender, EventArgs e)
        {
            FotoViewer fotoVorm = new FotoViewer ("Pildi näita", 1000, 600);
            fotoVorm.Show();
        }
    }
}
