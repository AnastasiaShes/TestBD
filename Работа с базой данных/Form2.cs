using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Работа_с_базой_данных
{
    public partial class Form2 : Form
    {
        SqlConnection sqlConnection;

        public string identifier;
        public Form2(string Identifier)
        {
            InitializeComponent();
            identifier = Identifier;
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            String connectionString = @"Data Source=LAPTOP-GJVK2LVJ\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT *  FROM [Empoyee], [Department] WHERE Empoyee.ID = @identifier", sqlConnection);
            command.Parameters.AddWithValue("identifier", identifier);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                {
                    while (await sqlReader.ReadAsync())
                    {
                        textBox1.Text = sqlReader["ID"].ToString();
                        textBox2.Text = sqlReader["SurName"].ToString();
                        textBox3.Text = sqlReader["FirstName"].ToString();
                        textBox4.Text = sqlReader["Patronymic"].ToString();
                        textBox5.Text = sqlReader["DateOfBirth"].ToString();
                        textBox6.Text = sqlReader["Position"].ToString();
                        textBox8.Text = sqlReader["Name"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = textBox3.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = textBox6.ReadOnly = false;
            button2.Visible = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [Empoyee ] SET [SurName] = @SurName, [FirstName] = @FirstName, [Patronymic] = @Patronymic, [DateOfBirth]=@DateOfBirth, [Position] = @Position WHERE [Id] = @Id", sqlConnection);

                command.Parameters.AddWithValue("ID", textBox1.Text);
                command.Parameters.AddWithValue("SurName", textBox2.Text);
                command.Parameters.AddWithValue("FirstName", textBox3.Text);
                command.Parameters.AddWithValue("Patronymic", textBox4.Text);
                command.Parameters.AddWithValue("DateOfBirth", textBox5.Text);
                command.Parameters.AddWithValue("Position", textBox6.Text);

                await command.ExecuteNonQueryAsync();

            }
            else if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Необходимо заполнить обязательные поял", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            textBox2.ReadOnly = textBox3.ReadOnly = textBox4.ReadOnly = textBox5.ReadOnly = textBox6.ReadOnly = true;
            button2.Visible = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}
