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

namespace WindowsFormsApp20
{
    public partial class Form5 : Form
    {
        int i = 0;
        DataSet ds5;
        DataSet ds6;
        SqlDataAdapter adapter5;
        SqlDataAdapter adapter6;
        SqlCommandBuilder commandBuilder;
        //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True";
        string quare = "select * from autohor";
        string q = "select * from books";
        string q2 = "select * from m2m_books_authors";
        string q3 = "select b_name,firstname from m2m_books_authors join books on m2m_books_authors.b_id=books.b_id join autohor on m2m_books_authors.a_id= autohor.a_id";
        public Form5()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {


                conn.Open();
                adapter5 = new SqlDataAdapter(q3, conn);
                ds5 = new DataSet();
                adapter5.Fill(ds5, "a");
                dataGridView1.DataSource = ds5.Tables["a"];

                adapter6 = new SqlDataAdapter(q2, conn);
                ds6 = new DataSet();
                adapter6.Fill(ds6, "m2m_books_authors");
                dataGridView2.DataSource = ds6.Tables["m2m_books_authors"];

                // делаем недоступным столбец id для изменения
                
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(quare, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "autohor");
                comboBox1.DisplayMember = "firstname";
                comboBox1.ValueMember = "a_id";
                comboBox1.DataSource = ds.Tables["autohor"];
                SqlDataAdapter adapter1 = new SqlDataAdapter(q, conn);
                DataSet ds2 = new DataSet();
                adapter1.Fill(ds2, "books");
                comboBox2.DisplayMember = "b_name";
                comboBox2.ValueMember = "b_id";
                comboBox2.DataSource = ds2.Tables["books"];
                SqlDataAdapter adapter2= new SqlDataAdapter(q2, conn);

            }
            update();

        }
        public void update()
        {
            dataGridView2.Rows.Count.ToString();
            textBox1.Text = dataGridView2.Rows[i].Cells["a_id"].Value.ToString();
            textBox2.Text = dataGridView2.Rows[i].Cells["b_id"].Value.ToString();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                adapter5 = new SqlDataAdapter(q2, conn);
                ds5 = new DataSet();
                adapter5.Fill(ds5, "m2m_books_authors");
                DataTable dt = ds5.Tables["m2m_books_authors"];
                // добавим новую строку
                DataRow newRow = dt.NewRow();
                //MessageBox.Show(comboBox1.SelectedValue.ToString());
                //MessageBox.Show(comboBox2.SelectedValue.ToString());
                newRow["a_id"] = (comboBox1.SelectedValue);
                newRow["b_id"] = (comboBox2.SelectedValue);
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter5);
                adapter5.Update(ds5, "m2m_books_authors");
                //ds5.Clear();
                //adapter5 = new SqlDataAdapter(q3, conn);
                //ds5 = new DataSet();
                //adapter5.Fill(ds5);
                //dataGridView2.DataSource = ds5.Tables[0];
                //// перезагружаем данные
               // update();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                update();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (i < (dataGridView2.Rows.Count - 2))
            {
                i++; update();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            if (i < (dataGridView1.Rows.Count - 2))
            {
                i++; update();
            }
            else
            {
                i = 0; update();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                adapter5 = new SqlDataAdapter(q2, conn);
                commandBuilder = new SqlCommandBuilder(adapter5);
                adapter5.Update(ds6, "m2m_books_authors");
            }
        }
    }
}
