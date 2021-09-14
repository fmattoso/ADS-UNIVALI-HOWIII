using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace HOW
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void passo1_Click(object sender, EventArgs e)
        {
            Form3 _f3;
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                _f3 = new Form3(Convert.ToInt32(r.Cells[0].Value));
                _f3.Show();
                break;
            }

            //            _f3 = new Form3();

            Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            MySqlConnectionStringBuilder conexaoBD = form1.conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "select * from tipoProduto";
                MySqlDataReader reader = comandoMySql.ExecuteReader();
  
                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32("idTipoProduto");
                    row.Cells[1].Value = reader.GetString("nmTipoProduto") + " (" + reader.GetString("dsTipoProduto") + ")";
                    dataGridView1.Rows.Add(row);
                }

                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro recuperando dados do servidor ! " + ex.Message);
                Console.WriteLine(ex.Message);
            }

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

        }


    }
}
