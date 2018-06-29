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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        SqlConnection conn = null;
        private string strSql = string.Empty;
        private string strConn = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=mat;Data Source=DESKTOP-BQJKA5R";
        

        

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConn);
            strSql = "INSERT INTO produtos (Nome, Fabricante, PrecoUnitario, DataFabricacao, DataValidade, Estoque) VALUES (@Nome, @Fabricante, @PrecoUnitario, @DataFabricacao, @DataValidade, @Estoque); ";
            SqlCommand cmd = new SqlCommand(strSql, conn);

            
            cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            cmd.Parameters.Add("@Fabricante", SqlDbType.VarChar).Value = txtFabricante.Text;
            cmd.Parameters.Add("@PrecoUnitario", SqlDbType.Float).Value = float.Parse(txtPrecoUnitario.Text);
            cmd.Parameters.Add("@DataFabricacao", SqlDbType.Date).Value = DateTime.Parse(mskDataFabricacao.Text);
            cmd.Parameters.Add("@DataValidade", SqlDbType.Date).Value = DateTime.Parse(mskDataValidade.Text);
            cmd.Parameters.Add("@Estoque", SqlDbType.Int).Value = int.Parse(txtEstoque.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produto cadastrado com sucesso!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = false;
            txtFabricante.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtEstoque.Enabled = false;
            mskDataFabricacao.Enabled = false;
            mskDataValidade.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtFabricante.Text = "";
            txtPrecoUnitario.Text = "";
            txtEstoque.Text = "";
            mskDataFabricacao.Text = "";
            mskDataValidade.Text = "";

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = false;
            txtFabricante.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtEstoque.Enabled = false;
            mskDataFabricacao.Enabled = false;
            mskDataValidade.Enabled = false;

            txtId.Focus();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 fm1 = new Form1();
            fm1.Show();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            txtNome.Enabled = true;
            txtFabricante.Enabled = true;
            txtEstoque.Enabled = true;
            txtPrecoUnitario.Enabled = true;
            mskDataFabricacao.Enabled = true;
            mskDataValidade.Enabled = true;

            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = false;
            txtFabricante.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtEstoque.Enabled = false;
            mskDataFabricacao.Enabled = false;
            mskDataValidade.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtFabricante.Text = "";
            txtPrecoUnitario.Text = "";
            txtEstoque.Text = "";
            mskDataFabricacao.Text = "";
            mskDataValidade.Text = "";
        }

        private void txtId_KeyUp(object sender, KeyEventArgs e)
        {
            conn = new SqlConnection(strConn);            
            strSql = "SELECT * FROM produtos WHERE Id=@Id;";
            SqlCommand cmd = new SqlCommand(strSql, conn);

            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(txtId.Text);

            try
            {
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();                
                dr.Read();

                if (dr.HasRows == false)
                {
                    MessageBox.Show("Id não cadastrado!");
                    
                    txtNome.Text = "";
                    txtFabricante.Text = "";
                    txtPrecoUnitario.Text = "";
                    txtEstoque.Text = "";
                    mskDataFabricacao.Text = "";
                    mskDataValidade.Text = "";
                }
                else
                {
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    txtEstoque.Text = Convert.ToString(dr["Estoque"]);
                    txtFabricante.Text = Convert.ToString(dr["Fabricante"]);
                    txtPrecoUnitario.Text = Convert.ToString(dr["PrecoUnitario"]);
                    mskDataFabricacao.Text = Convert.ToString(dr["DataFabricacao"]);
                    mskDataValidade.Text = Convert.ToString(dr["DataValidade"]);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            
            btnNovo.Enabled = false;
            btnSalvar.Enabled = false;
            btnVoltar.Enabled = false;
            btnAtualizar.Enabled = true;
            btnExcluir.Enabled = true;
            btnCancelar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = true;
            txtFabricante.Enabled = true;
            txtPrecoUnitario.Enabled = true;
            txtEstoque.Enabled = true;
            mskDataFabricacao.Enabled = true;
            mskDataValidade.Enabled = true;

            


        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConn);
            strSql = "UPDATE produtos SET Nome=@Nome, Fabricante=@Fabricante, PrecoUnitario=@PrecoUnitario, DataFabricacao=@DataFabricacao, DataValidade=@DataValidade, Estoque=@Estoque WHERE Id=@Id; ";

            SqlCommand cmd = new SqlCommand(strSql, conn);

            cmd.Parameters.Add("@id", SqlDbType.Int).Value = int.Parse(txtId.Text);

            cmd.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            cmd.Parameters.Add("@Fabricante", SqlDbType.VarChar).Value = txtFabricante.Text;
            cmd.Parameters.Add("@PrecoUnitario", SqlDbType.Float).Value = float.Parse(txtPrecoUnitario.Text);
            cmd.Parameters.Add("@DataFabricacao", SqlDbType.Date).Value = DateTime.Parse(mskDataFabricacao.Text);
            cmd.Parameters.Add("@DataValidade", SqlDbType.Date).Value = DateTime.Parse(mskDataValidade.Text);
            cmd.Parameters.Add("@Estoque", SqlDbType.Int).Value = int.Parse(txtEstoque.Text);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produto atualizado com sucesso!");                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {                
                conn.Close();                
            }

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = false;
            txtFabricante.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtEstoque.Enabled = false;
            mskDataFabricacao.Enabled = false;
            mskDataValidade.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtFabricante.Text = "";
            txtPrecoUnitario.Text = "";
            txtEstoque.Text = "";
            mskDataFabricacao.Text = "";
            mskDataValidade.Text = "";

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {                        
            if(MessageBox.Show("Deseja Excluir esse produto? ", "Cuidado!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                MessageBox.Show("Operação Cancelada!");
            }
            else
            {
                conn = new SqlConnection(strConn);
                strSql = "DELETE produtos WHERE Id=@Id;";
                SqlCommand cmd = new SqlCommand(strSql, conn);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = int.Parse(txtId.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produto Deletado!");
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

            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnExcluir.Enabled = false;
            btnVoltar.Enabled = true;

            txtId.Enabled = true;
            txtNome.Enabled = false;
            txtFabricante.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtEstoque.Enabled = false;
            mskDataFabricacao.Enabled = false;
            mskDataValidade.Enabled = false;

            txtId.Text = "";
            txtNome.Text = "";
            txtFabricante.Text = "";
            txtPrecoUnitario.Text = "";
            txtEstoque.Text = "";
            mskDataFabricacao.Text = "";
            mskDataValidade.Text = "";



        }
    }
}
