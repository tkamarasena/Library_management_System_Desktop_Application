using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Library_mgt_System_IMBS_
{
    public partial class BookRegistration : Form
    {
        Functions Con; //link with Function.cs class
        public BookRegistration()
        {
            InitializeComponent();
            Con = new Functions();
            GetCatergories();
        }

        private void GetCatergories() // get book catergories from CatergoryTbl
        {
            string Query = "SELECT * FROM Book_CatergoryTbl";
            cmbCatergory.DisplayMember = Con.GetData(Query).Columns["C_Name"].ToString();
            cmbCatergory.ValueMember = Con.GetData(Query).Columns["C_Id"].ToString();
            cmbCatergory.DataSource = Con.GetData(Query);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            grbCatergory.Enabled = true;   
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookRegistration_Load(object sender, EventArgs e)
        {
            grbCatergory.Enabled=false;
        }

        int copies = 0, availableCopies = 0;
        int book_id;
        string cat_name, status;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBookName.Text == "" || txtAuther.Text == "" || txtISBN.Text == "" || cmbCatergory.SelectedIndex == -1 
                    || txtPublisher.Text == "" || status == "") // check radio btn is checked or not
                {
                    //From Copy_browinng data insrtion
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string name = txtBookName.Text;
                    string auther = txtAuther.Text;
                    string isbn = txtISBN.Text;
                    int catergory = Convert.ToInt32(cmbCatergory.SelectedValue.ToString());
                    string cat_name = cmbCatergory.GetItemText(cmbCatergory.SelectedItem);
                    string title = txtBookName.Text + ", " + txtAuther.Text + ", " + txtISBN.Text;                    
                    string publisher = txtPublisher.Text;
                    
                   

                    //check whether the book already registered or not
                    string Query4 = "SELECT Name, No_of_copy, Available_copy FROM BookTbl WHERE Name='{0}'";
                    Query4 = string.Format(Query4, name);
                    DataTable t=  Con.GetData(Query4); /*temporary store data row inside the DataTable,
                                                        * we can change and update date table again*/ 

                    if (t.Rows.Count>0)
                    {
                        MessageBox.Show("Already exist Book");

                        /*if entered book is available in the library, retrieve that data and
                        update number of copies in the data base BookTbl*/

                        foreach (DataRow row in t.Rows)
                        {
                            copies = Convert.ToInt32(row["No_of_copy"].ToString());
                            availableCopies = Convert.ToInt32(row["Available_copy"].ToString());

                        }

                       // copies = Convert.ToInt32(Con.GetData(Query4).Columns["No_of_copy"].ToString());
                        //availableCopies = Convert.ToInt32(Con.GetData(Query4).Columns["Available_copy"].ToString());
                       

                        //update BookTbl
                        string Query5 = "UPDATE BookTbl SET No_of_copy = {0}, Available_copy = {1} WHERE Name='{2}'";
                        MessageBox.Show(name);
                        Query5 = string.Format(Query5, copies += 1, availableCopies += 1, name);
                        Con.SetData(Query5);

                        // cmbCatergory.DataSource = Con.GetData(Query);
                        //retrieve Book Id from Book details table
                        string Query2 = "SELECT Book_Id FROM BookTbl WHERE Name='{0}'";
                        Query2 = string.Format(Query2, name);

                        //book_id = Convert.ToInt32(Con.GetData(Query2).Columns["Book_Id"].ToString());

                        
                        foreach (DataRow dr in Con.GetData(Query2).Rows)
                        {
                            book_id = Convert.ToInt32(dr["Book_Id"].ToString());
                        }


                        //retrieve Book Catergory from category table
                        string Query6 = "SELECT C_Name FROM Book_CatergoryTbl WHERE C_Id={0}";
                        Query6 = string.Format(Query6, Convert.ToInt32(cmbCatergory.SelectedValue.ToString()));

                        //cat_name = (Con.GetData(Query6).Columns["C_Name"].ToString()).Substring(0, 1); //taking first letter of category

                        

                        foreach (DataRow dr in Con.GetData(Query6).Rows)
                        {
                            cat_name = (dr["C_Name"].ToString()).Substring(0, 1); //taking first letter of category
                        }


                        string clasiffication = cat_name + " " + book_id + " " + copies;
                        MessageBox.Show(clasiffication);

                        //string date = DateTime.Now.ToString("dd-mm-yyyy");

                        //insert book copy details.
                        string Query3 = "INSERT INTO  Copy_BorrowingsTbl VALUES('{0}',NULL, NULL, NULL)";
                        Query3 = string.Format(Query3, clasiffication);
                        Con.SetData(Query3);

                        

                        MessageBox.Show("Book copy has been registered succesfully!!");


                    }
                    else
                    {                        
                        //insert new book details into BookTbl.
                        string Query1 = "INSERT INTO  BookTbl VALUES({0},'{1}','{2}','{3}',{4},{5},'{6}')";
                        Query1 = string.Format(Query1, catergory, name, title, publisher, copies += 1, availableCopies += 1, status);
                        Con.SetData(Query1);

                        //retrieve Book Id from Book details table
                        string Query2 = "SELECT Book_Id FROM BookTbl WHERE Name='{0}'";
                        Query2 = string.Format(Query2, name);

                        //book_id = Convert.ToInt32(Con.GetData(Query2).Columns["Book_Id"].ToString());

                        //MessageBox.Show("Book Id "+book_id);
                        foreach (DataRow dr in Con.GetData(Query2).Rows)
                        {
                            book_id = Convert.ToInt32(dr["Book_Id"].ToString());
                        }


                        //retrieve Book Catergory from category table
                        string Query6 = "SELECT C_Name FROM Book_CatergoryTbl WHERE C_Id={0}";
                        Query6 = string.Format(Query6, Convert.ToInt32(cmbCatergory.SelectedValue.ToString()));

                        //cat_name = (Con.GetData(Query6).Columns["C_Name"].ToString()).Substring(0, 1); //taking first letter of category

                       // MessageBox.Show(cat_name);

                        foreach (DataRow dr in Con.GetData(Query6).Rows)
                        {
                            cat_name = (dr["C_Name"].ToString()).Substring(0, 1); //taking first letter of category
                        }


                        string clasiffication = cat_name + " " + book_id + " " + copies;
                        MessageBox.Show(clasiffication);

                        //string date = DateTime.Now.ToString("dd-mm-yyyy");

                        //insert book copy details.
                        string Query3 = "INSERT INTO  Copy_BorrowingsTbl VALUES('{0}',NULL, NULL, NULL)";
                        Query3 = string.Format(Query3, clasiffication);
                        Con.SetData(Query3);

                        MessageBox.Show("Book have been registered succesfully!!");


                    }


                    txtBookName.Text = "";
                    txtAuther.Text = "";
                    txtISBN.Text = "";
                    txtPublisher.Text = "";
                    cmbCatergory.SelectedIndex = -1;
                    
                    txtBookName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rdbBorrowable_CheckedChanged(object sender, EventArgs e)
        {
            status = "Borrowable";
        }

        private void rdbRefference_CheckedChanged(object sender, EventArgs e)
        {
            status = "Refference";
        }

        private void btnCatergoryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCatergory.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string category = txtCatergory.Text;
                    cmbCatergory.SelectedValue = category;

                    //insert book category 
                    string Query = "INSERT INTO  Book_CatergoryTbl VALUES('{0}')";
                    Query = string.Format(Query, category);
                    Con.SetData(Query);

                    txtCatergory.Text = "";
                    grbCatergory.Enabled = false;
                                       

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
