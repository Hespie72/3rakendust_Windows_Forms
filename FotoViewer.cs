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
    public partial class FotoViewer : Form
    {
        private PictureBox pctbox;
        private TableLayoutPanel tblp;
        private CheckBox cbox;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button showButton;
        private Button clearButton;
        private Button backgroundButton;
        private Button closeButton;
        private CheckBox stretchCheckBox;
        private OpenFileDialog openFileDialog1;
        private ColorDialog colorDialog1;
        public FotoViewer(string name, int width, int height)
        {
            InitializeComponent();

            this.Text = name;
            this.Width = width;
            this.Height = height;


            tblp = new TableLayoutPanel();

            tblp.Dock = DockStyle.Fill;
            tblp.ColumnCount = 2;
            tblp.RowCount = 2;

            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tblp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85F));
            tblp.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tblp.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            Controls.Add(tblp);


            pctbox = new PictureBox();
            pctbox.Dock = DockStyle.Fill;
            pctbox.BorderStyle = BorderStyle.Fixed3D;
            pctbox.BackColor = Color.White;
            pctbox.SizeMode = PictureBoxSizeMode.Zoom;

            stretchCheckBox = new CheckBox();
            stretchCheckBox.Text = "Venitama";
            stretchCheckBox.AutoSize = true;
            stretchCheckBox.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            stretchCheckBox.Margin = new Padding(5);
            stretchCheckBox.CheckedChanged += StretchCheckBox_CheckedChanged;
            tblp.Controls.Add(stretchCheckBox, 0, 1);




            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.AutoSize = false;
            tblp.Controls.Add(flowLayoutPanel1, 1, 1);



            showButton = new Button();
            showButton.Name = "kuvaNupp";
            showButton.Text = "näita pilti";
            showButton.AutoSize = true;
            showButton.Click += ShowButton_Click;

            clearButton = new Button();
            clearButton.Name = "Tühjendusnupp";
            clearButton.Text = "tee pilt selgeks";
            clearButton.AutoSize = true;
            clearButton.Click += ClearButton_Click;

            backgroundButton = new Button();
            backgroundButton.Name = "taustanupp";
            backgroundButton.Text = "Määra taustavärv";
            backgroundButton.AutoSize = true;
            backgroundButton.Click += BackgroundButton_Click;

            closeButton = new Button();
            closeButton.Name = "sulgemisnupp";
            closeButton.Text = "sulge";
            closeButton.AutoSize = true;
            closeButton.Click += CloseButton_Click;

            flowLayoutPanel1.Controls.Add(closeButton);
            flowLayoutPanel1.Controls.Add(backgroundButton);
            flowLayoutPanel1.Controls.Add(clearButton);
            flowLayoutPanel1.Controls.Add(showButton);
            tblp.Controls.Add(pctbox, 1, 0);

            colorDialog1 = new ColorDialog();

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;

            openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|" + "PNG Files (*.png)|*.png|" + "BMP Files (*.bmp)|*.bmp|" +"All files (*.*)|*.*";
        }

            private void StretchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (stretchCheckBox.Checked)
            {
                pctbox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pctbox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pctbox.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (pctbox.Image != null)
            {
                pctbox.Image.Dispose();
                pctbox.Image = null;
            }
        }


        private void BackgroundButton_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pctbox.BackColor = colorDialog1.Color;
            }
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Controls.Add(tblp);

        }
    }
}
