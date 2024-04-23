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
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace MidtermExam
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-VC57OT1\\SQL2004; Initial Catalog=DBMidterm;" +
            "Integrated Security=true");
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }
        private void getData()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From tblBook", con);
            dt.Clear();
            da.Fill(dt);
            dgvBook.DataSource = dt;
            dgvBook.Columns[0].HeaderText = "Code";
            dgvBook.Columns[1].HeaderText = "BookTitle";
            dgvBook.Columns[2].HeaderText = "Quantity";
            dgvBook.Columns[3].HeaderText = "UnitPrice";
            dgvBook.Columns[4].HeaderText = "Autor";
            dgvBook.Columns[5].HeaderText = "YearPublish";
            dgvBook.Columns["Code"].Width = 140;
            dgvBook.Columns["Title"].Width = 140;
            dgvBook.Columns["Quantity"].Width = 140;
            dgvBook.Columns["UnitPrice"].Width = 140;
            dgvBook.Columns["Author"].Width = 140;
            dgvBook.Columns["YearPublish"].Width = 139;
            dgvBook.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBook.ColumnHeadersDefaultCellStyle.Font = new Font("Tahama", 14, FontStyle.Bold);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con.Open();
            getData();
        }

        private void dgvBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int Index = e.RowIndex;
                DataGridViewRow SelectRow = dgvBook.Rows[Index];
                txtCode.Text = SelectRow.Cells["Code"].Value.ToString();
                txtTitle.Text = SelectRow.Cells["Title"].Value.ToString();
                txtQuantity.Text = SelectRow.Cells["Quantity"].Value.ToString();
                txtUnitPrice.Text = SelectRow.Cells["UnitPrice"].Value.ToString();
                txtAuthor.Text = SelectRow.Cells["Author"].Value.ToString();
                txtYearPublish.Text = SelectRow.Cells["YearPublish"].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //no need Code colums because it's auto ingremant
                SqlCommand cmd = new SqlCommand(" INSERT INTO tblBook(Title,Quantity,UnitPrice,Author,YearPublish)VALUES('" + txtTitle.Text + "','" + int.Parse(txtQuantity.Text) + "','" + Double.Parse(txtUnitPrice.Text) + "','" + txtAuthor.Text + "','" + int.Parse(txtYearPublish.Text) + "'); ", con);
                cmd.ExecuteNonQuery();
                getData();
                btnSave.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult iDelete = DialogResult.OK;
            iDelete = MessageBox.Show("Confirm if you want to delete this record", "Delete Record...!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (iDelete == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete from tblBook where Code= '" + int.Parse(txtCode.Text) + "' ", con);
                    cmd.ExecuteNonQuery();
                    getData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update tblBook Set " + " Title = ' " + txtTitle.Text + "', " + " Quantity = ' " + int.Parse(txtQuantity.Text) + "'," +
            " UnitPrice ='  " + double.Parse(txtUnitPrice.Text) + "', " +
            " Author ='  " + txtAuthor.Text + "', " +
             " YearPublish ='  " + int.Parse(txtYearPublish.Text) + "' " +
            " Where Code =  ' " + int.Parse(txtCode.Text) + "' ", con);


            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Update data successfull...!", "Update Data...!");
                getData();
            }
            catch (Exception)
            {
                MessageBox.Show("Update data unsuccessfull...!", "Update Data...!");
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From tblBook" + " Where Title= '" + txtFind.Text + "' ", con);
            dt.Clear();
            da.Fill(dt);
            dgvBook.DataSource = dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            iExit = MessageBox.Show("Confirm if you want to exit...!", "Exit Programm",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
                btnSave.Enabled = true;
                txtTitle.Clear();
                txtQuantity.Clear();
                txtUnitPrice.Clear();
                txtAuthor.Clear();
                txtYearPublish.Clear();
                txtCode.Text ="auto number";
                txtFind.Clear();

        }
    }
}