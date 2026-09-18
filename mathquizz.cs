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
        Label tlabel;
        Label pllabel;
        Label prlabel;
        Label mllabel;
        Label mrlabel;
        Label tllabel;
        Label trlabel;
        Label dllabel;
        Label drlabel;
        NumericUpDown sum;
        NumericUpDown difference;
        NumericUpDown product;
        NumericUpDown quotient;
        Button startbutton;
        Timer quizztime;
        

        int addend1;
        int addend2;
        int minuend;
        int subtrahend;
        int multiplicand;
        int multiplier;
        int dividend;
        int divisor;
        int timeLeft = 30;
        Random randomizer = new Random();
        public mathquizz(string name, int width, int height)
        {
            this.Text = name;
            this.Width = width;
            this.Height = height;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label timeTextLabel = new Label();
            timeTextLabel.Text = "Time Left";
            timeTextLabel.Font = new Font("Arial", 15);
            timeTextLabel.AutoSize = true;
            timeTextLabel.Location = new Point(230, 20);
            this.Controls.Add(timeTextLabel);

            tlabel = new Label();
            tlabel.Text = "30 seconds";
            tlabel.Font = new Font("Arial", 15);
            tlabel.AutoSize = true;
            tlabel.Location = new Point(330, 20);
            this.Controls.Add(tlabel);

            quizztime = new Timer();
            quizztime.Interval = 1000;
            quizztime.Tick += timer1_Tick;

            // plus label
            pllabel = new Label();
            pllabel.Text = "?";
            pllabel.AutoSize = false;
            pllabel.Size = new Size(60, 50);
            pllabel.Font = new Font("Arial", 18);
            pllabel.TextAlign = ContentAlignment.MiddleCenter;
            pllabel.Location = new Point(50, 75);
            this.Controls.Add(pllabel);
            Label plussign = new Label();
            plussign.Text = "+";
            plussign.AutoSize = false;
            plussign.Size = new Size(60, 50);
            plussign.Font = new Font("Arial", 18);
            plussign.TextAlign = ContentAlignment.MiddleCenter;
            plussign.Location = new Point(100, 75);
            this.Controls.Add(plussign);
            prlabel = new Label();
            prlabel.Text = "?";
            prlabel.AutoSize = false;
            prlabel.Size = new Size(60, 50);
            prlabel.Font = new Font("Arial", 18);
            prlabel.TextAlign = ContentAlignment.MiddleCenter;
            prlabel.Location = new Point(150, 75);
            this.Controls.Add(prlabel);
            Label pequals = new Label();
            pequals.Text = "=";
            pequals.AutoSize = false;
            pequals.Size = new Size(60, 50);
            pequals.Font = new Font("Arial", 18);
            pequals.TextAlign = ContentAlignment.MiddleCenter;
            pequals.Location = new Point(200, 75);
            this.Controls.Add(pequals);
            sum = new NumericUpDown();
            sum.Font = new Font("Arial", 18);
            sum.Location = new Point(275, 75);
            sum.Size = new Size(60, 50);
            sum.MaximumSize = new Size(100, 0);
            sum.MouseClick += Sum_MouseClick;
            sum.TabIndex = 1;
            Controls.Add(sum);

            // Minus label
            mllabel = new Label();
            mllabel.Text = "?";
            mllabel.AutoSize = false;
            mllabel.Size = new Size(60, 50);
            mllabel.Font = new Font("Arial", 18);
            mllabel.TextAlign = ContentAlignment.MiddleCenter;
            mllabel.Location = new Point(50, 150);
            this.Controls.Add(mllabel);
            Label minussign = new Label();
            minussign.Text = "-";
            minussign.AutoSize = false;
            minussign.Size = new Size(60, 50);
            minussign.Font = new Font("Arial", 18);
            minussign.TextAlign = ContentAlignment.MiddleCenter;
            minussign.Location = new Point(100, 150);
            this.Controls.Add(minussign);
            mrlabel = new Label();
            mrlabel.Text = "?";
            mrlabel.AutoSize = false;
            mrlabel.Size = new Size(60, 50);
            mrlabel.Font = new Font("Arial", 18);
            mrlabel.TextAlign = ContentAlignment.MiddleCenter;
            mrlabel.Location = new Point(150, 150);
            this.Controls.Add(mrlabel);
            Label mequals = new Label();
            mequals.Text = "=";
            mequals.AutoSize = false;
            mequals.Size = new Size(60, 50);
            mequals.Font = new Font("Arial", 18);
            mequals.TextAlign = ContentAlignment.MiddleCenter;
            mequals.Location = new Point(200, 150);
            this.Controls.Add(mequals);
            difference = new NumericUpDown();
            difference.Font = new Font("Arial", 18);
            difference.Location = new Point(275, 150);
            difference.Size = new Size(60, 50);
            difference.MaximumSize = new Size(100, 0);
            difference.MouseClick += Difference_MouseClick;
            difference.TabIndex = 2;
            Controls.Add(difference);

            //times label
            tllabel = new Label();
            tllabel.Text = "?";
            tllabel.AutoSize = false;
            tllabel.Size = new Size(60, 50);
            tllabel.Font = new Font("Arial", 18);
            tllabel.TextAlign = ContentAlignment.MiddleCenter;
            tllabel.Location = new Point(50, 225);
            this.Controls.Add(tllabel);
            Label timessign = new Label();
            timessign.Text = "×";
            timessign.AutoSize = false;
            timessign.Size = new Size(60, 50);
            timessign.Font = new Font("Arial", 18);
            timessign.TextAlign = ContentAlignment.MiddleCenter;
            timessign.Location = new Point(100, 225);
            this.Controls.Add(timessign);
            trlabel = new Label();
            trlabel.Text = "?";
            trlabel.AutoSize = false;
            trlabel.Size = new Size(60, 50);
            trlabel.Font = new Font("Arial", 18);
            trlabel.TextAlign = ContentAlignment.MiddleCenter;
            trlabel.Location = new Point(150, 225);
            this.Controls.Add(trlabel);
            Label tequals = new Label();
            tequals.Text = "=";
            tequals.AutoSize = false;
            tequals.Size = new Size(60, 50);
            tequals.Font = new Font("Arial", 18);
            tequals.TextAlign = ContentAlignment.MiddleCenter;
            tequals.Location = new Point(200, 225);
            this.Controls.Add(tequals);
            product = new NumericUpDown();
            product.Font = new Font("Arial", 18);
            product.Location = new Point(275, 225);
            product.Size = new Size(60, 50);
            product.MaximumSize = new Size(100, 0);
            product.MouseClick += Product_MouseClick;
            product.TabIndex = 3;
            Controls.Add(product);

            // divided label
            dllabel = new Label();
            dllabel.Text = "?";
            dllabel.AutoSize = false;
            dllabel.Size = new Size(60, 50);
            dllabel.Font = new Font("Arial", 18);
            dllabel.TextAlign = ContentAlignment.MiddleCenter;
            dllabel.Location = new Point(50, 300);
            this.Controls.Add(dllabel);
            Label dividesign = new Label();
            dividesign.Text = "÷";
            dividesign.AutoSize = false;
            dividesign.Size = new Size(60, 50);
            dividesign.Font = new Font("Arial", 18);
            dividesign.TextAlign = ContentAlignment.MiddleCenter;
            dividesign.Location = new Point(100, 300);
            this.Controls.Add(dividesign);
            drlabel = new Label();
            drlabel.Text = "?";
            drlabel.AutoSize = false;
            drlabel.Size = new Size(60, 50);
            drlabel.Font = new Font("Arial", 18);
            drlabel.TextAlign = ContentAlignment.MiddleCenter;
            drlabel.Location = new Point(150, 300);
            this.Controls.Add(drlabel);
            Label dequals = new Label();
            dequals.Text = "=";
            dequals.AutoSize = false;
            dequals.Size = new Size(60, 50);
            dequals.Font = new Font("Arial", 18);
            dequals.TextAlign = ContentAlignment.MiddleCenter;
            dequals.Location = new Point(200, 300);
            this.Controls.Add(dequals);
            quotient = new NumericUpDown();
            quotient.Font = new Font("Arial", 18);
            quotient.Location = new Point(275, 300);
            quotient.Size = new Size(60, 50);
            quotient.MaximumSize = new Size(100, 0);
            quotient.MouseClick += Quotient_MouseClick;
            quotient.TabIndex = 4;
            Controls.Add(quotient);


            startbutton = new Button();
            startbutton.Text = "Start the quizz";
            startbutton.Font = new Font("Arial", 14);
            startbutton.AutoSize = true;
            startbutton.TabIndex = 0;
            startbutton.Location = new Point(500, 400);
            startbutton.MouseClick += Startbutton_MouseClick;
            Controls.Add(startbutton);

            int d = 0;
        }

        private void Sum_MouseClick(object sender, MouseEventArgs e)
        {
            if (sum.Value == 0)
            {
                sum.Text = "";
            }

        }

        private void Difference_MouseClick(object sender, MouseEventArgs e)
        {
            if (difference.Value == 0)
            {
                difference.Text = "";
            }

        }

        private void Product_MouseClick(object sender, MouseEventArgs e)
        {
            if (product.Value == 0)
            {
                product.Text = "";
            }

        }

        private void Quotient_MouseClick(object sender, MouseEventArgs e)
        {
            if (quotient.Value == 0)
            {
                quotient.Text = "";
            }

        }

        private void Startbutton_MouseClick(object sender, MouseEventArgs e)
        {
            StartTheQuiz();
            startbutton.Enabled = false;
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                quizztime.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startbutton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer() returns false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft = timeLeft - 1;
                tlabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show
                // a MessageBox, and fill in the answers.
                quizztime.Stop();
                tlabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startbutton.Enabled = true;
            }
        }

        //start quizz
        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            pllabel.Text = addend1.ToString();
            prlabel.Text = addend2.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // This step makes sure its value is zero before
            // adding any values to it.
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            mllabel.Text = minuend.ToString();
            mrlabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            tllabel.Text = multiplicand.ToString();
            trlabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dllabel.Text = dividend.ToString();
            drlabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Start the timer.
            timeLeft = 30;
            tlabel.Text = "30 seconds";
            quizztime.Start();
        }

    }
}

