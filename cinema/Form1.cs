using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using Timer = System.Windows.Forms.Timer;

namespace cinema
{
    public partial class Form1 : Form
    {
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        Button button, button2;
        SqlCommand cmd;
        SqlDataReader reader;
        Timer timer1 = new Timer();
        PictureBox pictureBox = new PictureBox();
        public Form1()
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            button = new Button
            {
                Text = "Pilt",

            };
            button2 = new Button
            {
                Text = "admin" 
            };
            button.Click += Button_Click;
            button2.Click += Button2_Click;
            button2.Location = new Point(400, 400);
            this.Controls.Add(button);
            this.Controls.Add(button2);


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            this.Hide();
            form2.ShowDialog();

        }

        bool playing = false;
        int counter = 0;
        List<string> namepilt;
        private void Button_Click(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            timer1.Tick += Timer1_Tick; 
            this.Controls.Clear();
            connenction.Open();
            cmd = new SqlCommand("SELECT poster FROM filmid", connenction);
            reader = cmd.ExecuteReader();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Size = new Size(400, 600);
            pictureBox.Location = new Point(260, 30);
            this.Controls.Add(pictureBox);
            namepilt = new List<string>();
            while (reader.Read()) 
            {
                namepilt.Add(reader["poster"].ToString());

            }
            if (!playing)
            {
                timer1.Start();
                playing = true;
            }
            else
            {
                playing = false;
                timer1.Stop();
            }
            connenction.Close();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (counter > namepilt.Count - 1)
            {
                counter = 0;
            }

            pictureBox.Image = Image.FromFile(@"../../poster/"+namepilt[counter]);
            counter++;
        }
    }
}
