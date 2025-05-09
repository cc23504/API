using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormGetAPI
{
    //herda Form
    public partial class FormGetAPI : Form
    {
        public FormGetAPI()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //funcao close veio do Form
            this.Close();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            // Criar o RestClient
            RestClient restClient = new RestClient();

            // Setar o endPoint dele para a URI desejada
            restClient.endPoint = txtURI.Text;

            // Fazer a chamada do método que executa o request
            string respJSON = restClient.makeRequest();

            // atualizar a tela com a resposta do usuário
            txtResponse.Text = respJSON;
        }
    }
}
