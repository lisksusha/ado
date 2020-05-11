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
    public partial class Form9 : Form
    {
        int pageSize = 5; // размер страницы
        int pageNumber = 0; // текущая страница
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True";
        SqlDataAdapter adapter;
        DataSet ds;
        string GetSql()
        {
            return "SELECT * FROM autohor ORDER BY a_id OFFSET ((" + pageNumber + ") * " + pageSize + ") " +
                "ROWS FETCH NEXT " + pageSize + "ROWS ONLY";
        }
        public Form9()
        {
            InitializeComponent();
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form9_Load(object sender, EventArgs e)
        {
           
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                adapter= new SqlDataAdapter(GetSql(), connection);
                // Создаем объект Dataset
                 ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds,"autohor1");
                // Отображаем данные
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["a_id"].ReadOnly = true;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ds.Tables["autohor1"].Rows.Count < pageSize) return;
            pageNumber++;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(GetSql(), connection);
                ds.Tables["autohor1"].Rows.Clear();
                adapter.Fill(ds, "autohor1");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pageNumber==0) return;
            pageNumber--;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(GetSql(), connection);
                ds.Tables["autohor1"].Rows.Clear();
                adapter.Fill(ds, "autohor1");
            }

        }
    }
}
