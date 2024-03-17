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
    public partial class Book_Return : Form
    {
        Functions Con;
        int userId;
        public Book_Return()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
                try
                {
                    userId = Convert.ToInt32(txtUserId.Text);

                    if (txtUserId.Text == "")
                    {
                        MessageBox.Show("Missing Data!!");
                    }
                    else
                    {
                        string Query3 = "SELECT * FROM UserTbl WHERE User_Id={0}"; // check whether 
                        Query3 = string.Format(Query3, userId);
                        DataTable tb1 = Con.GetData(Query3);
                        if (tb1.Rows.Count > 0)
                        {
                            string Query2 = "SELECT * FROM Copy_BorrowingsTbl WHERE User_Id={0}";
                            Query2 = string.Format(Query2, userId);
                            DataTable tb2 = Con.GetData(Query2); /*temporary store data row inside the DataTable,
                                                                *we can change and update date table again*/

                            if (tb2.Rows.Count > 0)
                            {
                                
                                grbIssueBook.Enabled = Enabled;
                                txtBookId.Focus();
                        }
                            else
                            {
                                MessageBox.Show("This user has not borrow books!!");

                        }


                        }
                        else
                        {
                            MessageBox.Show("Not registered User!!");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        private void Book_Return_Load(object sender, EventArgs e)
        {
            grbIssueBook.Enabled = false;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string copy = txtBookId.Text;
            int availableCopies = 0;


             //Update BookBorrowing table 
             string Query1 = "UPDATE Copy_BorrowingsTbl SET  User_Id = NULL, Borrowing_Date = NULL, Return_Date = NULL WHERE Copy_Id = '{0}'";
             Query1 = string.Format(Query1, copy);
             Con.SetData(Query1);


            var str = copy; //split copyId and filter book id
            char[] seperator = {' '};
            string[] strarr = str.Split(seperator);

            string bookId = strarr[1];

            string Query5 = "SELECT Available_Copy FROM BookTbl WHERE Book_Id='" + bookId + "'";

            DataTable t = Con.GetData(Query5);

            foreach (DataRow row in t.Rows)
            {
                availableCopies = Convert.ToInt32(row["Available_copy"].ToString());

            }

            MessageBox.Show(availableCopies+" , "+ copy);

            //Update(increase) BookTbl availble copies 
            string Query4 = "UPDATE BookTbl SET  Available_copy = {0} WHERE Book_Id = {1}";
            Query4 = string.Format(Query4, availableCopies += 1, bookId);
            Con.SetData(Query4);



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBookId.Text = "";
        }
    }
}
