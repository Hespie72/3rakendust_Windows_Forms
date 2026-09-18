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
            nuppFoto.Location = new Point(300, 30);
            nuppFoto.Size = new Size(280, 40);
            nuppFoto.Click += NuppFoto_Click;
            nuppFoto.BackColor = Color.IndianRed;

            nuppMat = new Button();
            nuppMat.Text = "Ava matematika ülessaned aken";
            nuppMat.Location = new Point(300, 80);
            nuppMat.Size = new Size(280, 40);
            nuppMat.Click += NuppMat_Click;
            nuppMat.BackColor = Color.GreenYellow;

            nuppMang= new Button();
            nuppMang.Text = "Ava mängu aken";
            nuppMang.Location = new Point(300, 130);
            nuppMang.Size = new Size(280, 40);
            nuppMang.Click += NuppMang_Click;
            nuppMang.BackColor = Color.LightSeaGreen;

            Controls.Add(nuppFoto);
            Controls.Add(nuppMat);
            Controls.Add(nuppMang);
        }

        private void NuppMang_Click(object sender, EventArgs e)
        {
            matchgame mgame = new matchgame("Sobitamismäng", 1000, 600);
            mgame.Show();
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
