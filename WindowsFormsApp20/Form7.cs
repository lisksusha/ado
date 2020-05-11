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
    public partial class Form7 : Form
    {
        int i = 0;
        DataSet ds6;
        SqlDataAdapter adapter6;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=books_epam;Integrated Security=True";
        string sql1 = "select * from books";
        string sql2 = "select * from subscribers";
        string sql3 = "select firstname,secondname,b_name from subscriptions join subscribers on subscriptions.sb_subscriber=subscribers.s_id join books on subscriptions.sb_book= books.b_id";
        string sql = "SELECT * FROM subscriptions";
        public Form7()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {


                connection.Open();
                adapter = new SqlDataAdapter(sql3, connection);
                ds = new DataSet();
                adapter.Fill(ds,"aaaa");
                dataGridView1.DataSource = ds.Tables["aaaa"];
                // делаем недоступным столбец id для изменения
                //dataGridView1.Columns["sb_id"].ReadOnly = true;
                adapter6 = new SqlDataAdapter(sql, connection);
                ds6 = new DataSet();
                adapter6.Fill(ds6, "subscriptions");
                dataGridView2.DataSource = ds6.Tables["subscriptions"];
            }
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    MessageBox.Show(row.Cells[0].Value.ToString());
            //    MessageBox.Show(row.Cells[1].Value.ToString());
            //    MessageBox.Show(row.Cells[2].Value.ToString());
            //    //dataGridView1.Rows.Remove(row);
            //}
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
                //connection.Open();
                //adapter = new SqlDataAdapter(sql, connection);
                //commandBuilder = new SqlCommandBuilder(adapter);
                //adapter.InsertCommand = new SqlCommand("sp_subscribers", connection);
                //adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                //adapter.InsertCommand.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50, "sb_subscriber"));
                //adapter.InsertCommand.Parameters.Add(new SqlParameter("@secondn", SqlDbType.NVarChar, 50, "sb_book"));
                //adapter.InsertCommand.Parameters.Add(new SqlParameter("@name1", SqlDbType.NVarChar, 50, "sb_start"));
                //adapter.InsertCommand.Parameters.Add(new SqlParameter("@second", SqlDbType.NVarChar, 50, "sb_is_active"));


                //SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "s_id");
                //parameter.Direction = ParameterDirection.Output;

                //adapter.Update(ds);
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "books_epamDataSet.books". При необходимости она может быть перемещена или удалена.
            //this.booksTableAdapter.Fill(this.books_epamDataSet.books);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connection);
                DataSet ds1 = new DataSet();
                adapter1.Fill(ds1, "books");
                comboBox1.DisplayMember = "b_name";
                comboBox1.ValueMember = "b_id";
                comboBox1.DataSource = ds1.Tables["books"];

                SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, connection);
                DataSet ds2 = new DataSet();
                adapter2.Fill(ds2, "subscribers");
                comboBox2.DisplayMember = "firstname";
                comboBox2.ValueMember = "s_id";
                comboBox2.DataSource = ds2.Tables["subscribers"];
            }

            update();
            }
        public void update()
        {
            dataGridView2.Rows.Count.ToString();
            textBox1.Text = dataGridView2.Rows[i].Cells["sb_subscriber"].Value.ToString();
            textBox2.Text = dataGridView2.Rows[i].Cells["sb_book"].Value.ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(sql, connection);
                ds = new DataSet();
                adapter.Fill(ds, "subscriptions");
                DataTable dt = ds.Tables["subscriptions"];
                // добавим новую строку
                DataRow newRow = dt.NewRow();
                //MessageBox.Show(comboBox1.SelectedValue.ToString());
                //MessageBox.Show(comboBox2.SelectedValue.ToString());
                //MessageBox.Show(comboBox1.Text);
                newRow["sb_book"] = (comboBox1.SelectedValue);
                newRow["sb_subscriber"] = (comboBox2.SelectedValue);
                dt.Rows.Add(newRow);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds, "subscriptions");
                ds.Clear();
                adapter = new SqlDataAdapter(sql3, connection);
                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                // перезагружаем данные

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (i > 0)
            {
                i--;
                update();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (i < (dataGridView2.Rows.Count - 2))
            {
                i++; update();
            }
        }
    }
}
