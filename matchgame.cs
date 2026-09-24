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
    public partial class matchgame : Form
    {
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N",
            "c", "c", ",", ",",
            "b", "b", "f", "f",
            "k", "k", "v", "v"
        };

        Random random = new Random();

        Label firstClicked = null;
        Label secondClicked = null;

        Timer timer = new Timer();

        public matchgame(string Name, int width, int height)
        {
            this.Text = Name;
            this.Width = width;
            this.Height = height;

            InitializeComponent();

            Text = "Sobitamismäng";
            Size = new Size(550, 550);

            CreateGameBoard();

            timer.Interval = 750;
            timer.Tick += Timer_Tick;
        }

        private void CreateGameBoard()
        {
            TableLayoutPanel table = new TableLayoutPanel();

            table.Dock = DockStyle.Fill;
            table.BackColor = Color.CornflowerBlue;
            table.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            table.ColumnCount = 4;
            table.RowCount = 4;

            for (int i = 0; i < 4; i++)
            {
                table.ColumnStyles.Add(
                    new ColumnStyle(SizeType.Percent, 25F)
                );

                table.RowStyles.Add(
                    new RowStyle(SizeType.Percent, 25F)
                );
            }

            ShuffleIcons();

            for (int i = 0; i < 16; i++)
            {
                Label label = new Label();

                label.Dock = DockStyle.Fill;
                label.BackColor = Color.CornflowerBlue;
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.MiddleCenter;

                label.Font = new Font(
                    "Webdings",
                    48,
                    FontStyle.Bold
                );

                label.Text = "";

                label.Tag = icons[i];

                label.Click += Label_Click;

                table.Controls.Add(label);
            }

            Controls.Add(table);
        }

        private void ShuffleIcons()
        {
            for (int i = icons.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                string temp = icons[i];
                icons[i] = icons[j];
                icons[j] = temp;
            }
        }

        private void Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (timer.Enabled)
                return;

            if (clickedLabel.Text != "")
                return;

            if (firstClicked != null && secondClicked != null)
                return;

            clickedLabel.Text = clickedLabel.Tag.ToString();

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                return;
            }

            secondClicked = clickedLabel;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (firstClicked.Tag.ToString() ==
                secondClicked.Tag.ToString())
            {
                firstClicked = null;
                secondClicked = null;

                CheckGameFinished();
            }
            else
            {
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            firstClicked.Text = "";
            secondClicked.Text = "";

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckGameFinished()
        {
            foreach (Control control in Controls[0].Controls)
            {
                Label label = control as Label;

                if (label != null && label.Text == "")
                    return;
            }

            MessageBox.Show(
                "Sa sobitasid kõik ikoonid!",
                "Palju õnne!"
            );

        }
    }
}
