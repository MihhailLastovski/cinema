using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        Button button;
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
            button.Click += Button_Click;
            this.Controls.Add(button);
        }
        bool playing = false;
        int counter = 1;
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
            counter++;


            if (counter > namepilt.Count - 1)
            {
                counter = 0;
            }

            pictureBox.Image = Image.FromFile(@"../../poster/"+namepilt[counter]);
        }
    }
}
