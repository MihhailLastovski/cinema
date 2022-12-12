using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cinema
{
    public partial class Form2 : Form
    {
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        TextBox textBox;
        Label label, label2;
        SqlCommand cmd;
        DataGridView DataGrid;
        public Form2()
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox = new TextBox 
            {
                Location = new Point(5, 150),
                Size = new Size(135, 50),
                Font = new Font("Arial", 16),
            };
            Button button = new Button
            {
                Text = "OTSI",
                Size = new Size(100, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(145, 140)
            };
            label = new Label
            {
                BackColor = Color.White,
                Size = new Size(300, 30),
                Font = new Font("Arial", 16),
                Location = new Point(400, 150)
            };
            label2 = new Label
            {
                BackColor = formparam._backcolorform,
                Text = "Sisestage id: ",
                Font = new Font("Arial", 16),
                Location = new Point(0, 100),
                Size = new Size(200,30),
                ForeColor = Color.White
            };
            PictureBox pictureBox = new PictureBox 
            {
                Image = Image.FromFile(@"../../arrow.png"),
                Size = new Size(100, 30),
                Location = new Point(250,150),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            Button button1 = new Button 
            {
                Text = "Kustuta!",
                Size = new Size(100, 50),
                Font = new Font("Arial", 16),
                ForeColor = Color.White,
                Location = new Point(5, 220),
            };
            textBox.Text = "Ainult numbrid";
            textBox.KeyPress += TextBox_KeyPress;
            textBox.GotFocus += RemoveText;
            button.Click += Button_Click;
            button1.Click += Button1_Click;
            Naita_Andmed();
            this.Controls.Add(button);
            this.Controls.Add(pictureBox);
            this.Controls.Add(textBox);
            this.Controls.Add(label);
            this.Controls.Add(label2);
            this.Controls.Add(button1);


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            connenction.Open();
            if (DataGrid.SelectedRows.Count == 0)
            {
                connenction.Close();
                return;
            }
            string sql = "DELETE FROM Filmid WHERE Id = @rowID";
            int selectedIndex;
            int rowID;
            using (SqlCommand deleteRecord = new SqlCommand(sql, connenction))
            {
                selectedIndex = DataGrid.SelectedRows[0].Index;
                rowID = Convert.ToInt32(DataGrid[0, selectedIndex].Value);

                deleteRecord.Parameters.AddWithValue("@rowID", rowID);
                deleteRecord.ExecuteNonQuery();

                DataGrid.Rows.RemoveAt(selectedIndex);
            }
            connenction.Close();

        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        public void RemoveText(object sender, EventArgs e)
        {
            if (textBox.Text == "Ainult numbrid")
            {
                textBox.Text = "";
            }
        }
        public void Naita_Andmed()
        {
            DataGrid = new DataGridView();
            DataGrid.Location = new Point(0, 300);
            DataGrid.Size = new Size(1200, 370);
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Update_Table();
            this.Controls.Add(DataGrid);
        }
        private void Update_Table()
        {
            connenction.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM filmid", connenction);
            DataSet ds = new DataSet();
            da.Fill(ds, "filmid");
            DataGrid.DataSource = ds.Tables["filmid"].DefaultView;
            connenction.Close();
        }
        RESTAPI.MyObject film;
        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox.Text != "")
            {
                film = new RESTAPI.MyObject();
                film = RESTAPI.rest(Int32.Parse(textBox.Text));
                if (film != null)
                {
                    label.Text = film.Name;
                    Button button = new Button
                    {
                        Text = "LISA",
                        Location = new Point(720, 140),
                        Size = new Size(100, 50),
                        Font = new Font("Arial", 16),
                        ForeColor = Color.White

                    };
                    button.Click += Button_Click1;
                    this.Controls.Add(button);
                }
                else 
                {
                    MessageBox.Show("Sellist filmi pole olemas!", "Error");
                }
                
            }
            
        }

        private void Button_Click1(object sender, EventArgs e)
        {
            connenction.Open();
            cmd = new SqlCommand("SELECT poster FROM filmid WHERE poster = '"+film.Poster+"'", connenction);
            object result = cmd.ExecuteScalar();
            if (result == null)
            {
                cmd = new SqlCommand("INSERT INTO filmid(nimetus, poster, zanr, rating, movieLength, description, year) VALUES(@nimetus, @poster, @zanr, @rating, @movieLength, @description, @year)", connenction);
                cmd.Parameters.AddWithValue("@nimetus", film.Name);
                cmd.Parameters.AddWithValue("@poster", film.Poster);
                cmd.Parameters.AddWithValue("@zanr", film.Zanr);
                cmd.Parameters.AddWithValue("@rating", film.Rating);
                cmd.Parameters.AddWithValue("@movieLength", film.MovieLength);
                cmd.Parameters.AddWithValue("@description", film.Description);
                cmd.Parameters.AddWithValue("@year", film.Year);
                cmd.ExecuteNonQuery();
            }
            else 
            {
                MessageBox.Show("Такой фильм уже существует в БД!");
            }
            connenction.Close();
            Update_Table();
        }
    }
}
