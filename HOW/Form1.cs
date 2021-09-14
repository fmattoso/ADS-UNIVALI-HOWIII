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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        public MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "howsdog";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "ffm#7276";
            conexaoBD.SslMode = 0;
            return conexaoBD;
        }

        private void start_Click(object sender, EventArgs e)
        {
            Form2 _f2;
            _f2 = new Form2();
            if (_f2.ShowDialog() != DialogResult.OK) 
                Show();
            else
                Hide();
        }
    }
}
