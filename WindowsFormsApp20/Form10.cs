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
    public partial class Form10 : Form
    {
        int i = 0;
        DataSet dsmain;
        DataSet ds6;
        DataSet dstext;
        SqlDataAdapter textadapter;
        SqlDataAdapter adapter6;
        SqlDataAdapter mainadapter;
        SqlCommandBuilder commandBuilder;
        string quare = "select * from genres";
        string q = "select * from books";
        string q2 = "select * from m2m_books_genres";
        string q3 = "select b_name,g_name from m2m_books_genres join books on m2m_books_genres.b_id=books.b_id join genres on m2m_books_genres.g_id= genres.g_id";
        public Form10()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.AllowUserToAddRows = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {


                conn.Open();
                mainadapter = new SqlDataAdapter(q3, conn);
                dsmain = new DataSet();
                mainadapter.Fill(dsmain, "aa");
                dataGridView1.DataSource = dsmain.Tables["aa"];
                // делаем недоступным столбец id для изменения
                adapter6 = new SqlDataAdapter(q2, conn);
                ds6 = new DataSet();
                adapter6.Fill(ds6, "m2m_books_genres");
                dataGridView2.DataSource = ds6.Tables["m2m_books_genres"];

                textadapter = new SqlDataAdapter(q3, conn);
                dstext = new DataSet();
                textadapter.Fill(dstext, "aa");
                dataGridView3.DataSource = dstext.Tables["aa"];

            }
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(quare, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "genres");
                comboBox1.DisplayMember = "g_name";
                comboBox1.ValueMember = "g_id";
                comboBox1.DataSource = ds.Tables["genres"];
                SqlDataAdapter adapter1 = new SqlDataAdapter(q, conn);
                DataSet ds2 = new DataSet();
                adapter1.Fill(ds2, "books");
                comboBox2.DisplayMember = "b_name";
                comboBox2.ValueMember = "b_id";
                comboBox2.DataSource = ds2.Tables["books"];
                SqlDataAdapter adapter2 = new SqlDataAdapter(q2, conn);
                textadapter = new SqlDataAdapter(q3, conn);
            }
           
            update1();
        }
        
        public void update1()
        {

            dataGridView3.Rows.Count.ToString();

            textBox3.Text = dataGridView3.Rows[i].Cells["g_name"].Value.ToString();
            textBox4.Text = dataGridView3.Rows[i].Cells["b_name"].Value.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                mainadapter = new SqlDataAdapter(q2, conn);
                dsmain = new DataSet();
                mainadapter.Fill(dsmain, "m2m_books_genres");
                DataTable dt = dsmain.Tables["m2m_books_genres"];
                // добавим новую строку
                DataRow newRow = dt.NewRow();
                //MessageBox.Show(comboBox1.SelectedValue.ToString());
                //MessageBox.Show(comboBox2.SelectedValue.ToString());
                newRow["g_id"] = (comboBox1.SelectedValue);
                newRow["b_id"] = (comboBox2.SelectedValue);
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(mainadapter);
                mainadapter.Update(dsmain, "m2m_books_genres");
                dsmain.Clear();
                textadapter = new SqlDataAdapter(q3, conn);
                dstext = new DataSet();
                textadapter.Fill(dstext,"aa");
                dataGridView3.DataSource = dstext.Tables["aa"];



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                update1();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (i < (dataGridView3.Rows.Count - 2))
            {
                i++; update1();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.Rows[i]);
            if (i < (dataGridView1.Rows.Count - 2))
            {
                i++; update1();
            }
            else
            {
                i = 0; update1();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                mainadapter = new SqlDataAdapter(q2, conn);
                commandBuilder = new SqlCommandBuilder(mainadapter);
                adapter6.Update(ds6, "m2m_books_genres");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {
                conn.Open();
                textadapter = new SqlDataAdapter(q3, conn);
                dstext = new DataSet();
                mainadapter.Fill(dstext);
                textadapter.Update(dstext, "aa");

            }
        }
    }
}
