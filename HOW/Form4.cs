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
    public partial class Form4: Form
    {
        private List<ItemPedido> itensPed = new List<ItemPedido>();
 
        public Form4()
        {
            InitializeComponent();
        }

        public void AddItemPed(Int32 idProduto)
        {
            itensPed.Add(new ItemPedido() { idProduto = idProduto });
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblbairro_Click(object sender, EventArgs e)
        {

        }

        private void tbcep_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Form3 _f3;
            _f3 = new Form3();
            _f3.Show();
            Hide();
        }

        private void passo1_Click(object sender, EventArgs e)
        {
            // Insere pedido
            Int32 idPedido;
            Int32 idProduto;
            string SQLValues;

            Form1 form1 = new Form1();
            MySqlConnectionStringBuilder conexaoBD = form1.conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "INSERT INTO pedido (dtPedido, previsaoPreparo, previsaoEntrega, idCliente) " +
                    "VALUES( now(), date_add(now(), INTERVAL 30 minute), date_add(now(), INTERVAL 1 hour), 1 )" ;
                comandoMySql.ExecuteNonQuery();

                comandoMySql.CommandText = "SELECT MAX(idPedido) FROM Pedido ";
                MySqlDataReader reader = comandoMySql.ExecuteReader();
                reader.Read();
                idPedido = reader.GetInt32(0);
                reader.Close();

                comandoMySql.CommandText = "INSERT INTO pedidoitens (valorUnit, quantidade, idPedido, idProduto) VALUES ";
                SQLValues = "";

                foreach (ItemPedido aPart in itensPed)
                {
                    idProduto = aPart.GetHashCode();

                    if (SQLValues == "")
                    {
                        SQLValues = " ((SELECT valorVenda FROM produto WHERE idProduto = " + Convert.ToString(idProduto) + "), " +
                            "1, " + Convert.ToString(idPedido) + ", " + Convert.ToString(idProduto) + ")";
                    }
                    else
                    {
                        SQLValues = SQLValues + ", " +
                            " ((SELECT valorVenda FROM produto WHERE idProduto = " + Convert.ToString(idProduto) + "), " +
                            "1, " + Convert.ToString(idPedido) + ", " + Convert.ToString(idProduto) + ")";

                    }                    
                }

                comandoMySql.CommandText = comandoMySql.CommandText + SQLValues;
                comandoMySql.ExecuteNonQuery();

                comandoMySql.CommandText = "UPDATE pedido SET totalPedido = (SELECT SUM(valorUnit * quantidade) FROM pedidoItens WHERE idPedido = " + Convert.ToString(idPedido) + ") " +
                    " WHERE idPedido = " + Convert.ToString(idPedido);
                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close();
                MessageBox.Show("Inserido com sucesso");

                Form5 _f5;
                _f5 = new Form5(idPedido);

                _f5.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Hide();
        }
    }

    public class ItemPedido : IEquatable<ItemPedido>
    {
        public Int32 idProduto { get; set; }

        public override int GetHashCode()
        {
            return idProduto;
        }

        bool IEquatable<ItemPedido>.Equals(ItemPedido other)
        {
            throw new NotImplementedException();
        }
    }

}
