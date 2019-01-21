using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Работа_с_базой_данных
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDBDataSet.Empoyee". При необходимости она может быть перемещена или удалена.
            this.empoyeeTableAdapter1.Fill(this.testDBDataSet.Empoyee);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDBDataSet.Empoyee". При необходимости она может быть перемещена или удалена.
            this.empoyeeTableAdapter1.Fill(this.testDBDataSet.Empoyee);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDBDataSet.Department". При необходимости она может быть перемещена или удалена.
            this.departmentTableAdapter1.Fill(this.testDBDataSet.Department);

            //Создаем колонки
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("SurName", "Фамилия");
            dataGridView1.Columns.Add("FirstName", "Имя");
            dataGridView1.Columns.Add("Patronymic", "Отчество");
            dataGridView1.Columns.Add("Position", "Должность");

            dataGridView1.Columns[0].Width = 45;
            dataGridView1.Columns[1].Width = 130;
            dataGridView1.Columns[2].Width = 130;
            dataGridView1.Columns[3].Width = 130;
            dataGridView1.Columns[4].Width = 200;

        }


        //При выборе необходимого отдела, формируем таблицу сотрудников
        private async void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            button1.Visible = true;

            dataGridView1.Rows.Clear();

            String connectionString = @"Data Source=LAPTOP-GJVK2LVJ\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //выделение строчки полностью в таблице

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            string ComboSelectedValue = comboBox1.SelectedValue.ToString();


            SqlCommand command = new SqlCommand("SELECT * FROM [Empoyee] WHERE DepartmentID = @ID", sqlConnection);
            command.Parameters.AddWithValue("ID", comboBox1.SelectedValue.ToString());

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                {
                    while (await sqlReader.ReadAsync())
                    {
                        int rowNumber = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowNumber].Cells["ID"].Value = sqlReader["ID"];
                        dataGridView1.Rows[rowNumber].Cells["SurName"].Value = sqlReader["SurName"];
                        dataGridView1.Rows[rowNumber].Cells["FirstName"].Value = sqlReader["FirstName"];
                        dataGridView1.Rows[rowNumber].Cells["Patronymic"].Value = sqlReader["Patronymic"];
                        dataGridView1.Rows[rowNumber].Cells["Position"].Value = sqlReader["Position"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        //Кнопка с дополнительной информацией
        private void button1_Click(object sender, EventArgs e)
        {
            string identifier = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            Form2 form2 = new Form2(identifier);
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.departmentTableAdapter1.FillBy1(this.testDBDataSet.Department);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.departmentTableAdapter1.FillBy1(this.testDBDataSet.Department);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.departmentTableAdapter1.FillBy(this.testDBDataSet.Department);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
