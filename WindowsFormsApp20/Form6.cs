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
    public partial class Form6 : Form
    {
        int i = 0;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True";
        string sql = "SELECT * FROM subscribers";
        string GetSql()
        {
            return "SELECT * FROM subscribers";
        }
        public Form6()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);

                ds = new DataSet();
                adapter.Fill(ds, "subscribers");
                dataGridView1.DataSource = ds.Tables[0];
                // делаем недоступным столбец id для изменения
                dataGridView1.Columns["s_id"].ReadOnly = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(GetSql(), connection);
                DataTable dt = ds.Tables["subscribers"];
                // добавим новую строку
                DataRow newRow = dt.NewRow();
                newRow["firstname"] = textBox4.Text;
                newRow["secondname"] = textBox5.Text;
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds, "subscribers");
                ds.Clear();
                // перезагружаем данные
                adapter.Fill(ds, "subscribers");
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];

            }
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("sp_subscribers", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "firstname"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@secondn", SqlDbType.NVarChar, 50, "secondname"));


                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "s_id");
                parameter.Direction = ParameterDirection.Output;

                adapter.Update(ds);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            update();
        }
        public void update()
        {
            dataGridView1.Rows.Count.ToString();
            textBox1.Text = dataGridView1.Rows[i].Cells["s_id"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells["firstname"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells["secondname"].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                update();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (i < (dataGridView1.Rows.Count - 2))
            {
                i++; update();
            }
        }
    }
}
