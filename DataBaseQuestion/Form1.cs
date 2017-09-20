using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace DataBaseQuestion
{
    public partial class Form1 : Form
    {
        DAO dao = new DAO();
        SqlDataAdapter da;
        BindingSource bs;
        DataTable dt;
       

        public Form1()
        {
            InitializeComponent();
        }

        private void DGVSelection(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                DataGridViewRow selected = dgv.SelectedRows[0];
                txtFN.Text = selected.Cells["StuFn"].Value.ToString();
                txtSN.Text = selected.Cells["StuLN"].Value.ToString();
                txtStuID.Text = selected.Cells["StuID"].Value.ToString();

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            da = new SqlDataAdapter();
            dt = new DataTable();
            bs = new BindingSource();
            string update = "SELECT * FROM STUDENT";
            SqlCommand cmd = new SqlCommand(update, dao.OpenCon());
            da.SelectCommand = cmd;
            da.Fill(dt);
            bs.DataSource = dt;
            dgv.DataSource = dt;
            dao.CloseCon();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string fn = txtFN.Text;
            string ln = txtSN.Text;
            string insert = "INSERT INTO Student(StuFN,StuLN) VALUES (@fn,@ln)";
            SqlCommand cmd = new SqlCommand(insert, dao.OpenCon());
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@ln", ln);
            cmd.ExecuteNonQuery();
            dao.CloseCon();
            UpdateGrid();
        
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string del = "DELETE FROM STUDENT WHERE StuID=@stuid";

            if (dgv.SelectedRows.Count >0)
            {
                SqlCommand cmd = new SqlCommand(del, dao.OpenCon());
                cmd.Parameters.AddWithValue("stuid", dgv.SelectedRows[0].Cells[0].Value);
                cmd.ExecuteNonQuery();
                dao.CloseCon();
                UpdateGrid();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string fn = txtFN.Text;
            string ln = txtSN.Text;
            string id = txtStuID.Text;

            string edit = "UPDATE Student SET StuFN=@fn,StuLN=@ln WHERE StuID=@id";
            SqlCommand cmd = new SqlCommand(edit, dao.OpenCon());
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@ln", ln);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            dao.CloseCon();
            UpdateGrid();
        }
    }
}
