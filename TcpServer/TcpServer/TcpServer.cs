﻿using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Concurrent;

ConcurrentBag<TcpClient> clients = new ConcurrentBag<TcpClient>();
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
socket.Bind(new IPEndPoint(IPAddress.Any, 5000));
socket.Listen(100);
Console.WriteLine("Servidor iniciado na porta 5000...");

await AcceptClientsAsync();

async Task AcceptClientsAsync()
{
    while (true)
    {
        Socket clientSocket = await socket.AcceptAsync();
        TcpClient client = new TcpClient { Client = clientSocket };
        clients.Add(client);
        Console.WriteLine("Novo cliente conectado.");
        _ = HandleClientAsync(client);
    }
}

async Task HandleClientAsync(TcpClient client)
{
    NetworkStream stream = client.GetStream();
    byte[] buffer = new byte[1024];

    try
    {
        while (true)
        {
            int byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (byteCount == 0) break;

            string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
            Console.WriteLine(message);

            // Envia para todos os outros clientes
            foreach (var c in clients)
            {
                if (c != client && c.Connected)
                {
                    try
                    {
                        NetworkStream s = c.GetStream();
                        await s.WriteAsync(buffer, 0, byteCount);
                    }
                    catch { /* Ignora falhas de envio */ }
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
        // Remove cliente da lista
        clients = new ConcurrentBag<TcpClient>(clients.Where(c => c != client));
        client.Close();
    }
}
