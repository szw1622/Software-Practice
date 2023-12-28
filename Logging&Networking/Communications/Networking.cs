/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   Zhuowen Song
/// Date:      4/4/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file contains code for the Networking object, which can interact with the client and the server.
/// </summary>
/// 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Communications
{
    /// <summary>
    /// This class represent the Networking. 
    /// </summary>
    public class Networking
    {
        private ReportMessageArrived _handleMessage;
        private ReportDisconnect _handleDisconnect;
        private ReportConnectionEstablished _handleConnection;
        private CancellationTokenSource _WaitForCancellation;

        private readonly ILogger _logger;
        private readonly char _termCharacter;

        //public List<TcpClient> connectingClients = new();
        public TcpClient ConnectingClient = new();
        public TcpClient client;
        public TcpListener network_listener;
        public string serverName = string.Empty;
        public string clientName;

        /// <summary>
        /// This a constructor of the Networking, all of the deletes are used to call back to the client or the server.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="onConnect"></param>
        /// <param name="onDisconnect"></param>
        /// <param name="onMessage"></param>
        /// <param name="terminationCharacter"></param>
        public Networking(ILogger logger,
            ReportConnectionEstablished onConnect, ReportDisconnect onDisconnect, ReportMessageArrived onMessage,
            char terminationCharacter)
        {
            _logger = logger;
            _handleConnection = onConnect;
            _handleDisconnect = onDisconnect;
            _termCharacter = terminationCharacter;
            _handleMessage = onMessage;
            client = new TcpClient();
            clientName = string.Empty;
        }

        /// <summary>
        /// Get of set the name of client.
        /// </summary>
        public string ID
        {
            get
            {
                if (serverName.Length == 0)
                {
                    serverName = client.Client.RemoteEndPoint.ToString();
                }

                return serverName;
            }
            set
            {
                serverName = value;
            }
        }

        /// <summary>
        /// This method is used for clients to connect to the server.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Connect(string host, int port)
        {
            try
            {
                if (!client.Connected)
                {
                    //Create a tcp client object and connect it to the given host/port
                    client = new TcpClient(host, port);
                    ID = Environment.MachineName;
                    _logger.LogInformation($"Client {port} connected to server successfully");
                    _handleConnection(this);
                }
            }
            //Handle the exceptional cases of where the host/port is not available
            catch (Exception ex)
            {
                _logger.LogInformation($"Client {port} can not connected to server");
                _logger.LogInformation("Exception: " + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// This method is used for clients to wait the message from the server.
        /// </summary>
        /// <param name="infinite"></param>
        public async void ClientAwaitMessagesAsync(bool infinite = true)
        {
            try
            {
                StringBuilder dataBacklog = new StringBuilder();
                byte[] buffer = new byte[4096];
                NetworkStream stream = client.GetStream();

                if (stream == null) return;

                while (infinite)
                {
                    int total = await stream.ReadAsync(buffer, 0, buffer.Length);

                    string current_data = Encoding.UTF8.GetString(buffer, 0, total);

                    dataBacklog.Append(current_data);

                    CheckForMessage(dataBacklog);

                    if (client.Connected == false)
                    {
                        infinite = false; break;
                    }
                }
            }catch (Exception ex)
            {
                _handleDisconnect(this);
            }        
            
        }

        /// <summary>
        /// This is a helper method check the received messsag by using the _termCharacter
        /// </summary>
        /// <param name="data"></param>
        private void CheckForMessage(StringBuilder data)
        {
            string allData = data.ToString();
            int terminator_position = allData.IndexOf(_termCharacter);
            bool foundOneMessage = false;

            while (terminator_position >= 0)
            {
                foundOneMessage = true;
                string message = allData.Substring(0, terminator_position + 1);
                data.Remove(0, terminator_position + 1);

                _handleMessage(this, message);

                allData = data.ToString();
                terminator_position = allData.IndexOf(_termCharacter);
            }

            if (!foundOneMessage)
            {
                _logger.LogDebug("Cannot find message");
            }
            else
            {
                _logger.LogDebug($"{data.Length} bytes unprocessed.");
            }
        }

        /// <summary>
        /// This method is used for server to wait connecting client.
        /// </summary>
        /// <param name="infinite"></param>
        public async void WaitForClients(int port, bool infinite)
        {
            network_listener = new TcpListener(IPAddress.Any, port); //not sure bout IPAdress.Any
            network_listener.Start();
            try
            {
                _WaitForCancellation = new();
                while (true)
                {
                    TcpClient connection = await network_listener.AcceptTcpClientAsync(_WaitForCancellation.Token);
                    Networking connectingNet = new Networking(NullLogger.Instance, _handleConnection, _handleDisconnect, netOfServerReceivedMessage, _termCharacter );//TODO: Define the networking. Not sure bout Networking parameters
                    //connection = clientNet.client;
                    connectingNet.client = connection;
                    //connectingNet.setClientName(connection.Client.LocalEndPoint.ToString());
                    this.ConnectingClient = connection;
                    Thread t = new Thread(() => connectingNet.ClientAwaitMessagesAsync(infinite)); //not sure
                    t.Start();
                    _handleConnection(this);
                }
            }
            catch (Exception ex)
            {
                network_listener.Stop();
            }
        }

        /// <summary>
        /// Cancel the previous method
        /// </summary>
        public void StopWaitingForClients()
        {
            _handleDisconnect(this);
            _WaitForCancellation.Cancel();
        }

        /// <summary>
        /// Close the connection to the remote host
        /// </summary>
        public void Disconnect()
        {
            _handleDisconnect(this);//TODO: in the client GUI, sent the disconnect message to server
            client.Close();
        }

        /// <summary>
        /// Send the text across the network using the (already connected) tcp client.
        /// </summary>
        /// <param name="text"></param>
        public async void Send(string text)
        {
                byte[] messageBytes = Encoding.UTF8.GetBytes(text);

                try
                {
                    await client.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
                }
                catch(Exception ex)
                {
                    
                }
        }

        /// <summary>
        /// Call back again by using the new Networking
        /// </summary>
        /// <param name="sendingChannel"></param>
        /// <param name="message"></param>
        public void netOfServerReceivedMessage(Networking sendingChannel, string message)
        {
            _handleMessage(sendingChannel, message);
        }
    }

    public delegate void ReportMessageArrived(Networking channel, string message);
    public delegate void ReportDisconnect(Networking channel);
    public delegate void ReportConnectionEstablished(Networking channel);

}