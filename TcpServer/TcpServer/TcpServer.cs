using System.Net;
using System.Net.Sockets;
using System.Text;

List<TcpClient> clients = new List<TcpClient>();
TcpListener server = new TcpListener(IPAddress.Any, 5000);
server.Start();
Console.WriteLine("Servidor iniciado na porta 5000...");

while (true)
{
    TcpClient client = server.AcceptTcpClient();
    clients.Add(client);
    Console.WriteLine("Novo cliente conectado.");
    ParameterizedThreadStart threadStart = new ParameterizedThreadStart(HandleClient);
    Thread thread = new Thread(threadStart);
    thread.Start(client);
}

void HandleClient(object? obj)
{
    if (obj is not TcpClient client) return;
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];

    try
    {
        while (true)
        {
            int byteCount = stream.Read(buffer, 0, buffer.Length);
            if (byteCount == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
            Console.WriteLine(message);

            // Envia para todos os outros clientes
            foreach (var c in clients)
            {
                if (c != client)
                {
                    NetworkStream s = c.GetStream();
                    s.Write(buffer, 0, byteCount);
                }
            }
        }
    }
    catch
    {
        Console.WriteLine("Cliente desconectado.");
    }
    finally
    {
        clients.Remove(client);
        client.Close();
    }
}
