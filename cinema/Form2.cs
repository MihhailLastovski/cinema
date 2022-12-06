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
    public partial class Form2 : Form
    {
        SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\opilane.TTHK\\source\\repos\\Lastovski_TARpv21\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        //SqlConnection connenction = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lasto\\source\\repos\\cinema\\cinema\\DB\\cinemaDB.mdf;Integrated Security=True");
        TextBox textBox;
        Label label;
        public Form2()
        {
            formparam formparam = new formparam();
            BackColor = formparam._backcolorform;
            Width = formparam.Width;
            Height = formparam.Height;
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox = new TextBox();
            textBox.Text = "ID";
            Button button = new Button
            {
                Text = "OTSI",

            };
            label = new Label 
            {
                BackColor = Color.White
            };
            button.Location = new Point(500, 400);
            textBox.Location = new Point(400, 400);
            label.Location = new Point(400, 500);

            button.Click += Button_Click; 
            this.Controls.Add(button);
            this.Controls.Add(textBox);
            this.Controls.Add(label);

        }

        private void Button_Click(object sender, EventArgs e)
        {
            RESTAPI.MyObject asd = new RESTAPI.MyObject();
            asd = RESTAPI.rest(Int32.Parse(textBox.Text));
            label.Text = asd.Name;
        }
    }
}
