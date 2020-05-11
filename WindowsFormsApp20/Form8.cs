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
    public partial class Form8 : Form
    {
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True";
        SqlDataAdapter adapter;
        DataSet ds;
       
        string GetSql()
        {
            return "SELECT * FROM autohor";
                
        }
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // Создаем объект DataAdapter
                adapter = new SqlDataAdapter(GetSql(), connection);
                // Создаем объект Dataset
                ds = new DataSet();
                // Заполняем Dataset
                adapter.Fill(ds, "autohor1");
                // Отображаем данные
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns["a_id"].ReadOnly = true;
                adapter.InsertCommand = new SqlCommand("sp_CreateUser", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "firstname"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@secondname", SqlDbType.NVarChar, 50, "secondname"));

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
              adapter = new SqlDataAdapter(GetSql(), connection);
                DataTable dt = ds.Tables["autohor1"];
                // добавим новую строку
                DataRow newRow = dt.NewRow();
                newRow["firstname"] = textBox1.Text;
                newRow["secondname"] = textBox2.Text;
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds, "autohor1");
                ds.Clear();
                // перезагружаем данные
                adapter.Fill(ds, "autohor1");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
