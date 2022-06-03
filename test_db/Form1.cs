using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test_db
{
    public partial class Form1 : Form
    {
        database db = new database();
        public Form1()
        {
            InitializeComponent();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '⚫';
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"insert into register(login_user, password_user) values ('{loginUser}', '{passUser}')";
            SqlCommand command = new SqlCommand(querystring, db.GetConnection());
            db.OpenConnection();
            checkUser();
            
        }
        private Boolean checkUser()
        {
            var loginUser = textBox1.Text;
            var pasUser = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{pasUser}'";
            SqlCommand command = new SqlCommand(querystring, db.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже зарегистрирован!");
                return true;
            }
            else
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form3 frm3 = new Form3();
                    this.Hide();
                    frm3.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Аккаунт не создан!", "Аккаунт не создан!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                db.CloseConnection();
                return false;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 newfrm = new Form2();
            newfrm.Show();
            Form1 oldfrm = new Form1();
            oldfrm.Close();
        }

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        
    }
}
