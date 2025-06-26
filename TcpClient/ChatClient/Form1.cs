using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private TcpClient? client;
        private NetworkStream? stream;
        private Thread? receiveThread;
        private string usuarioNome = "Usuário";

        public Form1()
        {
            InitializeComponent();
            SolicitarNomeUsuario();
            ConectarAoServidor();
        }

        private void ConectarAoServidor()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000); // IP do servidor
                stream = client.GetStream();

                receiveThread = new Thread(ReceberMensagens);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar: " + ex.Message);
            }
        }

        private void ReceberMensagens()
        {
            byte[] buffer = new byte[1024];
            int byteCount;            
            while (stream != null && (byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string mensagem = Encoding.UTF8.GetString(buffer, 0, byteCount);
                Invoke(new Action(() =>
                {
                    txtChat.SelectionStart = txtChat.TextLength;
                    txtChat.SelectionLength = 0;
                    txtChat.SelectionColor = Color.Blue; // Mensagens recebidas em azul
                    txtChat.AppendText(mensagem + Environment.NewLine);
                    txtChat.SelectionColor = txtChat.ForeColor; // Reseta a cor
                }));
            }            
        }


        private void btnEnviar_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMensagem.Text) && stream != null)
            {
                string msg = $"{usuarioNome}: {txtMensagem.Text}";
                byte[] data = Encoding.UTF8.GetBytes(msg);
                stream.Write(data, 0, data.Length);
                txtChat.SelectionStart = txtChat.TextLength;
                txtChat.SelectionLength = 0;
                txtChat.SelectionColor = Color.Black;
                txtChat.AppendText( msg + Environment.NewLine);
                txtChat.SelectionColor = txtChat.ForeColor;
                txtMensagem.Clear();
            }
        }
        private void SolicitarNomeUsuario()
        {
            string? nome = Microsoft.VisualBasic.Interaction.InputBox(
                "Digite seu nome de usuário:", "Nome do Usuário", "Usuário");
            if (!string.IsNullOrWhiteSpace(nome))
                usuarioNome = nome.Trim();
        }
    }
}








