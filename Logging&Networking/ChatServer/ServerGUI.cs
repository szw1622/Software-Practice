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
/// This file contains code for the Chat Client GUI, allowing a user to connect to a server, send and receive messages.
/// </summary>
/// 
using Communications;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    /// <summary>
    /// This class represent the chat server. 
    /// </summary>
    public partial class ServerGUI : Form
    {
        private Dictionary<TcpClient, Networking> channels = new();
        private Networking channel;
        private ILogger logger;

        /// <summary>
        /// This is a constructor of the server gui.
        /// When the program start running, the client will create a new Networking object,
        /// and set the name of this server as local mechine name
        /// The termination char of this client is '\n'.
        /// 
        /// This server will start trying to connect the clients when this program start.
        /// </summary>
        public ServerGUI()
        {
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');
            channel.ID = Environment.MachineName;

            InitializeComponent();

            IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());
            ServerBox.Text = localAddr.ToString();
            try
            {
                channel.WaitForClients(11000, true);
                logger?.LogDebug($"Starting server with port 11000");
            }
            catch
            {
                ServerBox.Text = "Wrong Address.";
            }
        }

        /// <summary>
        /// When the server shutdown button clicked, the channel will stop waiting for clients.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShutdownServerButton_Click(object sender, EventArgs e)
        {
            this.channel.StopWaitingForClients();
            this.Close();
        }

        /// <summary>
        /// When the GUI is closing or the channel stop waiting for clients,
        /// send the last notice message to the clients.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            logger?.LogDebug("Server shut down.");
            SendMessage("Server shut down!\n");
        }

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when connected successfully.
        /// </summary>
        /// <param name="channel"></param> This.channel
        private void onConnect(Networking channel)
        {
            logger?.LogInformation($"{channel.ID} connected.");
            refreshConnections(channel);
        }

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when client disconnected.
        /// </summary>
        /// <param name="channel"></param> This.channel
        public void onDisconnect(Networking channel)
        {
            logger?.LogInformation($"{channel.ID} disconnected.");
            channels.Remove(channel.client);
        }

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when received message.
        /// </summary>
        /// <param name="channel"></param> This.channel
        public void onMessage(Networking channel, string message)
        {
            logger?.LogInformation($"Received a message {message} from {channel.ID}.");
            // Get the name of the sending client
            TcpClient sendingClient = channel.client;
            Networking sendingNet = channels[sendingClient];
            string connectingName = channels[sendingClient].ID;

            // Show the message on the message box.
            BoxOfMessages.Invoke(new MethodInvoker(delegate { BoxOfMessages.Text += connectingName + "-" + message + Environment.NewLine; }));

            // Change the name.
            if (message.Contains("Command Name"))
            {
                TcpClient changingNameClient = channel.client;
                string newName = message.Substring(13, message.IndexOf("\n") - 13);
                logger?.LogDebug($"{channel.ID} change the name into {newName}.");
                channels[changingNameClient].ID = newName;

                // Refresh the participants
                ParticipantsBox.Invoke(new MethodInvoker(delegate { ParticipantsBox.Clear(); }));
                foreach (TcpClient client in channels.Keys)
                {
                    string refreshingName = channels[client].ID;
                    ParticipantsBox.Invoke(new MethodInvoker(delegate { ParticipantsBox.Text += refreshingName + Environment.NewLine; }));
                }
            }

            // Deal with the request command participants
            if (message.Contains("Command Participants"))
            {
                logger?.LogDebug($"{channel.ID} request the list of participants.");
                string partList = "Command Participants";
                // Get the names of connecting clients
                foreach (Networking par in channels.Values)
                {
                    partList += $",{par.ID}";
                }
                partList += "\n";
                // Send back to the requesting client.
                channel.Send(partList);
            }

            // Send the message to the connecting clients.
            SendMessage(connectingName + "-" + message);
        }


        /// <summary>
        /// This is a helper method to send message to all connecting clients.
        /// </summary>
        /// <param name="message"></param>
        private async void SendMessage(string message)
        {
            List<Networking> toSendTo = new();
            lock (channels)
            {
                foreach (Networking toSendNet in channels.Values)
                {
                    toSendTo.Add(toSendNet);
                }
            }

            foreach (Networking toSendNet in toSendTo)
            {
                toSendNet.Send(message);
            }
        }

        /// <summary>
        /// This is a helper method to refresh the participants list.
        /// </summary>
        /// <param name="message"></param>
        private void refreshConnections(Networking channel)
        {
            TcpClient connectingClient = channel.ConnectingClient;
            // Using the all of the endpoints to create the Networkings
            if (!channels.Keys.Contains(connectingClient))
            {
                // Create a new Networking object to store the connecting client.
                Networking connectingNet = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');
                connectingNet.client = connectingClient;
                channels.Keys.Append(connectingClient);
                channels[connectingClient] = connectingNet;
                // Refresh the name of the connecting clients
                string connectingName = channels[connectingClient].ID;
                ParticipantsBox.Text += $"{connectingName}\n";
            }

        }

        /// <summary>
        /// This is a helper method to get the lcoal ip address
        /// Reference: https://stackoverflow.com/questions/6803073/get-local-ip-address
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

    }
}