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

namespace MercadoCRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = null;
        private string strSql = string.Empty;
        private string strConn = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=mat;Data Source=DESKTOP-BQJKA5R";
        SqlDataAdapter da = null;
        DataTable datatable = null;

        private void CARREGAR(string caixa)
        {
            try
            {
                conn = new SqlConnection(strConn);
                da = new SqlDataAdapter(caixa, conn);
                datatable = new DataTable();
                da.Fill(datatable);
                datatable.Columns[0].ColumnName = "Id";
                datatable.Columns[1].ColumnName = "Nome";
                datatable.Columns[2].ColumnName = "Fabricante";
                datatable.Columns[3].ColumnName = "PrecoUnitario";
                datatable.Columns[4].ColumnName = "DataFabricacao";
                datatable.Columns[5].ColumnName = "DataValidade";
                datatable.Columns[6].ColumnName = "Estoque";
                

                dataGridView1.DataSource = datatable;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ATUALIZAR()
        {
            strSql = "SELECT * FROM produtos ORDER BY Id ASC";
            SqlCommand cmd = new SqlCommand(strSql, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable datatable = new DataTable();
            datatable.Load(dr);
            dataGridView1.DataSource = datatable;
        }

        private void btnCadastrar(object sender, EventArgs e)
        {
            this.Hide();
            Form2 cadastro = new Form2();            
            cadastro.Show();            
        }

        decimal total, troco;

        private void txtIdBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            strSql = "SELECT * FROM produtos WHERE Id = '"+txtIdBuscar.Text+"'";
            CARREGAR(strSql);

            
        }

        private void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            strSql = "UPDATE produtos SET Estoque = Estoque - '"+txtQuantidade.Text+"' WHERE Id=@Id;";

            conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strSql, conn);

            cmd.Parameters.Add("Id", SqlDbType.Int).Value = int.Parse(txtIdBuscar.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produto vendido com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                ATUALIZAR();
                total = 0;

            }

            lblProduto.Text = "";
            lblFabricante.Text = "";
            lblPrecoUnitario.Text = "";
            lblTotal.Text = "";
            txtDinheiro.Text = "";
            lblTroco.Text = "";
            txtIdBuscar.Text = "";
            txtQuantidade.Text = "";

        }

        private void txtDinheiro_KeyUp(object sender, KeyEventArgs e)
        {
            decimal dinheiro = decimal.Parse(txtDinheiro.Text);
            troco = dinheiro - total;
            lblTroco.Text = troco.ToString();
        }

        private void btnAdicionarCarrinho_Click(object sender, EventArgs e)
        {
            if(txtQuantidade.Text == string.Empty)
            {
                throw new Exception("Digite uma quantidade: ");
            }
            else
            {
                int id = Convert.ToInt32(txtIdBuscar.Text);
                strSql = "SELECT Id, Nome, Fabricante, PrecoUnitario FROM produtos WHERE Id=@Id;";
                conn = new SqlConnection(strConn);
                SqlCommand cmd = new SqlCommand(strSql, conn);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(txtIdBuscar.Text);

                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    dr.Read();

                    lblProduto.Text = Convert.ToString(dr["Nome"]);
                    lblFabricante.Text = Convert.ToString(dr["Fabricante"]);
                    lblPrecoUnitario.Text = Convert.ToString(dr["PrecoUnitario"]);

                    int quantidade = int.Parse(txtQuantidade.Text);
                    decimal preco = decimal.Parse(lblPrecoUnitario.Text);
                    total = total + (preco * quantidade);

                    lblTotal.Text = total.ToString();

                    txtIdBuscar.Focus();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
