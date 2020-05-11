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
    public partial class Form11 : Form
    {
        int i = 0;
        DataSet ds5;
        SqlDataAdapter mainadapter;
        SqlCommandBuilder commandBuilder;
        string quare = "select * from genres";
        string q = "select * from books";
        string q2 = "select * from m2m_books_genres";
        string q3 = "select b_name,g_name from m2m_books_genres join books on m2m_books_genres.b_id=books.b_id join genres on m2m_books_genres.g_id= genres.g_id";
        public Form11()
        {
           
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            //dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView2.AllowUserToAddRows = false;
            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True"))
            {


                conn.Open();
                mainadapter = new SqlDataAdapter(q3, conn);
                ds5 = new DataSet();
                mainadapter.Fill(ds5, "m2m_books_genres");
                dataGridView1.DataSource = ds5.Tables["m2m_books_genres"];
                // делаем недоступным столбец id для изменения
               

               

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }
    }
}
