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
    public partial class Form3 : Form
    {
        private Int32 idTipoProduto;

        public Form3(Int32 idTipoProduto)
        {
            this.idTipoProduto = idTipoProduto;
            InitializeComponent();
        }

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            
            MySqlConnectionStringBuilder conexaoBD = form1.conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "select * from Produto where idTipoProduto = " + Convert.ToString(this.idTipoProduto);
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32("idProduto");
                    row.Cells[1].Value = reader.GetString("nmProduto") + " (" + reader.GetString("dsProduto") + ")";
                    row.Cells[2].Value = reader.GetDecimal("valorVenda");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void passo1_Click(object sender, EventArgs e)
        {
            //            Hide();
            Form4 _f4;
            _f4 = new Form4();
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                _f4.AddItemPed(Convert.ToInt32(r.Cells[0].Value));
            }
            _f4.Show();
            Close();
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            Form2 _f2;
            _f2 = new Form2();
            _f2.Show();
            Hide();
//            Close();
        }
    }
}
