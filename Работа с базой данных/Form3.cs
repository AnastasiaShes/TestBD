using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Работа_с_базой_данных
{
    public partial class Form3 : Form
    {
        SqlConnection sqlConnection;

        public Form3()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {

            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDBDataSet.Department". При необходимости она может быть перемещена или удалена.
            this.departmentTableAdapter.Fill(this.testDBDataSet.Department);
            try
            {
                this.departmentTableAdapter.FillBy2(this.testDBDataSet.Department);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            String connectionString = @"Data Source=LAPTOP-GJVK2LVJ\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlCommand command2 = new SqlCommand("SELECT max(id) FROM [Empoyee]", sqlConnection);

            int numberID = Convert.ToInt32(command2.ExecuteScalar()) + 1;

            textBox1.Text = numberID.ToString();

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            String connectionString = @"Data Source=LAPTOP-GJVK2LVJ\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            try
            {
                if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                    !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                    !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                    !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text) &&
                    !string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                    !string.IsNullOrEmpty(comboBox1.Text) && !string.IsNullOrWhiteSpace(comboBox1.Text))
                {

                    DateTime data = Convert.ToDateTime(textBox5.Text);
                    Guid g = new Guid (comboBox1.SelectedValue.ToString());
                    SqlCommand command = new SqlCommand("SET IDENTITY_INSERT[Empoyee] ON INSERT INTO [Empoyee] (Empoyee.ID, DepartmentID, SurName, FirstName, Patronymic, DateOfBirth, DocSeries, DocNumber, Position) VALUES (@ID, @DepartmentID, @SurName, @FirstName, @Patronymic, @DateOfBirth, @DocSeries, @DocNumber, @Position)", sqlConnection);
                   

                    command.Parameters.AddWithValue("ID", textBox1.Text);
                    command.Parameters.AddWithValue("DepartmentID", g);
                    command.Parameters.AddWithValue("SurName", textBox2.Text);
                    command.Parameters.AddWithValue("FirstName", textBox3.Text);
                    command.Parameters.AddWithValue("Patronymic", textBox4.Text);
                    command.Parameters.AddWithValue("DateOfBirth", data);
                    command.Parameters.AddWithValue("DocSeries", textBox7.Text);
                    command.Parameters.AddWithValue("DocNumber", textBox6.Text);
                    command.Parameters.AddWithValue("Position", textBox8.Text);

                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Сотрудник зарегистрирован", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
