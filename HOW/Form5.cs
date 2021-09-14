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
    public partial class Form5 : Form
    {
        private Int32 idPedido;

        public Form5(Int32 idPedido)
        {
            this.idPedido = idPedido;
            InitializeComponent();
        }

        public Form5()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Form4 _f4;
            _f4 = new Form4();
            _f4.Show();
            Hide();
        }

        private void lblDadosPedido_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            Form6 _f6;
            _f6 = new Form6();
            _f6.Show();
            Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            MySqlConnectionStringBuilder conexaoBD = form1.conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();
                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT pedido.idPedido, pedido.dtPedido, pedido.previsaoPreparo, pedido.previsaoEntrega, pedido.totalPedido, " +
                    "cliente.nmCliente, " +
                    "tipoProduto.nmTipoProduto, produto.nmProduto,  " +
                    "pedidoItens.valorUnit, pedidoItens.quantidade, (pedidoItens.valorUnit * pedidoItens.quantidade) AS `totalItem` FROM pedido " +
                    "INNER JOIN cliente ON cliente.idCliente = pedido.idCliente " +
                    "INNER JOIN pedidoitens ON pedidoitens.idPedido = pedido.idPedido " +
                    "INNER JOIN produto ON produto.idProduto = pedidoitens.idProduto " +
                    "INNER JOIN tipoProduto ON tipoProduto.idTipoProduto = produto.idTipoProduto " +
                    "WHERE Pedido.idPedido = " + Convert.ToString(idPedido);
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                reader.Read();
                richTextBox1.Clear();
                richTextBox1.AppendText("\r\n         PEDIDO Nº " + reader.GetString("idPedido"));
                richTextBox1.AppendText("\r\n             DATA: " + reader.GetString("dtPedido"));
                richTextBox1.AppendText("\r\n PREVISÃO PREPARO: " + reader.GetString("previsaoPreparo"));
                richTextBox1.AppendText("\r\n PREVISÃO ENTREGA: " + reader.GetString("previsaoEntrega"));
                richTextBox1.AppendText("\r\n");
                richTextBox1.AppendText("\r\n          Cliente: " + reader.GetString("nmCliente"));
                richTextBox1.AppendText("\r\n");
                richTextBox1.AppendText("\r\n ITENS DO PEDIDO");

                do
                {
                    richTextBox1.AppendText("\r\n" + reader.GetString("nmProduto") + " - " +
                        reader.GetString("nmTipoProduto") + " " +
                        reader.GetString("valorUnit") + " X " +
                        reader.GetString("quantidade") + " = " +
                        reader.GetString("totalItem"));
                }
                while (reader.Read());

                richTextBox1.AppendText("\r\n");
                richTextBox1.AppendText("\r\n TOTAL DO PEDIDO: " + reader.GetString("totalPedido"));
                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
