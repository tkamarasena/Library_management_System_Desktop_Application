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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace Library_mgt_System_IMBS_
{
    public partial class UserRegistration : Form
    {
        Functions Con;
        public UserRegistration()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string gender = cmbGender.GetItemText(cmbGender.SelectedItem);
                string nic = txtNIC.Text;
                string address = txtAddress.Text;

                //insert new user details into UserTbl.
                string Query1 = "INSERT INTO  UserTbl VALUES('{0}','{1}','{2}','{3}')";
                Query1 = string.Format(Query1, name, gender, nic, address);
                Con.SetData(Query1);

                MessageBox.Show("User have been registered successfully!!");

                txtName.Text = "";
                cmbGender.SelectedIndex = -1;
                txtAddress.Text = "";
                txtNIC.Text = "";
                txtName.Focus();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message); 
            }
           


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            cmbGender.SelectedIndex = -1;
            txtAddress.Text = "";
            txtNIC.Text = "";
            txtName.Focus();
        }
    }
}
