using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_mgt_System_IMBS_
{
    public partial class Reservation : Form
    {
        Functions Con;
        public Reservation()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserId.Text == "" || txtBookId.Text == "") // check radio btn is checked or not
                {
                    //From Copy_browinng data insrtion
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string user=txtUserId.Text;
                    string bookId=txtBookId.Text;

                    string date = DateTime.Now.ToString("dd-mm-yyyy");

                    MessageBox.Show(date);

                    string Query1 = "INSERT INTO  ReservationTbl VALUES({0},{1},'{2}')";
                    Query1 = string.Format(Query1, user,bookId, date);
                    Con.SetData(Query1);

                    MessageBox.Show("Reservation is successfull");

                    txtUserId.Text = "";
                    txtBookId.Text = "";
                    txtUserId.Focus();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBookId.Text = "";
            txtUserId.Text = "";

        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            txtUserId.Focus();
        }
    }
}
