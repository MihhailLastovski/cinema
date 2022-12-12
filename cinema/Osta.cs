using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    public partial class Osta : Form
    {
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        SqlCommand cmd;
        List<int> row, seat;
        int seanssID;
        List<string> pilet = new List<string>();
        decimal kokkuhind = 0;
        string nimetus_hall;
        public Osta(List<int> row, List<int> seat, int seanssID, string time, string nimetus, string nimetus_hall)
        {
            this.nimetus_hall = nimetus_hall;
            this.row = row;
            this.seat = seat;
            this.seanssID = seanssID;
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            Label lbl = new Label 
            {
                Text = nimetus,
                AutoSize = true,
                Location = new Point(350, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White
            };
            Label lbl2 = new Label
            {
                Text = time,
                AutoSize = true,
                Location = new Point(650, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White
            };
            this.Controls.Add(lbl);
            this.Controls.Add(lbl2);
            string[] pilettuup = new string[] { "Tavapilet", "Õpilase / Tudengipilet", "Lastepilet", "Pensionär" };
            double[] price = new double[] { 8.10, 5.88, 5.29, 5.29 };
            int y = 200;
            for (int i = 0; i < pilettuup.Length; i++)
            {
                Label lbl3 = new Label
                {
                    Text = pilettuup[i],
                    AutoSize = true,
                    Location = new Point(350, y),
                    Font = new Font("Arial", 16),
                    ForeColor = Color.White
                };
                NumericUpDown numericUpDown = new NumericUpDown 
                {
                    Name = pilettuup[i],
                    Tag = price[i],
                    Font = new Font("Arial", 16),
                    ForeColor = Color.White,
                    BackColor = formparam._backcolorform,
                    AutoSize = true,
                    Location = new Point(650, y),
                    Maximum = row.Count,
                };
                Label lbl4 = new Label
                {
                    Text = price[i].ToString() + "€",
                    AutoSize = true,
                    Location = new Point(850, y),
                    Font = new Font("Arial", 16),
                    ForeColor = Color.White
                };
                numericUpDown.ValueChanged += NumericUpDown_ValueChanged;
                this.Controls.Add(lbl3);
                this.Controls.Add(lbl4);
                this.Controls.Add(numericUpDown);
                y += 50;
            }
            Button button = new Button
            {
                Text = "OSTA",
                Size = new Size(180, 40),
                Location = new Point(650, 500),
                Font = new Font("Arial", 16, FontStyle.Bold),
                ForeColor = Color.White
            };
            button.Click += Button_Click;
            this.Controls.Add(button);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            pilet.Add(numericUpDown.Name + " - " + numericUpDown.Value.ToString() + " - " + (numericUpDown.Value*Decimal.Parse(numericUpDown.Tag.ToString()) + "€" + "\n"));
            kokkuhind += numericUpDown.Value * Decimal.Parse(numericUpDown.Tag.ToString());
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (pilet.Count != 0)
            {
                connenction.Open();
                for (int i = 0; i < row.Count; i++)
                {
                    cmd = new SqlCommand("INSERT INTO piletid(row, seat, seanssID) VALUES(@row, @seat, @seanssID)", connenction);
                    cmd.Parameters.AddWithValue("@row", row[i]);
                    cmd.Parameters.AddWithValue("@seat", seat[i]);
                    cmd.Parameters.AddWithValue("@seanssID", seanssID);
                    cmd.ExecuteNonQuery();
                }
                connenction.Close();
                SendMail sendMail = new SendMail(pilet, kokkuhind, nimetus_hall, seat, row);
                MessageBox.Show("Pileteid ostetakse");
            }
            
        }
    }
}
