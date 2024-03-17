using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_mgt_System_IMBS_
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (true)
            {
                
            }
        }

        private void lblLoan_Click(object sender, EventArgs e)
        {
            Borrowings obj= new Borrowings();
            obj.Show();
        }

        private void lblReservation_Click(object sender, EventArgs e)
        {
            Reservation obj = new Reservation();
            obj.Show();
        }

        private void lblReturn_Click(object sender, EventArgs e)
        {
            Book_Return obj = new Book_Return();
            obj.Show();
        }

        private void lblUserReg_Click(object sender, EventArgs e)
        {
            UserRegistration obj= new UserRegistration();
            obj.Show();
        }

        private void lblBookReg_Click(object sender, EventArgs e)
        {
            BookRegistration obj= new BookRegistration();
            obj.Show();
        }
    }
}
