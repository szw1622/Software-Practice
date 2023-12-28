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
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Communications;
using Microsoft.Extensions.Logging.Abstractions;
using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ChatClient
{
    /// <summary>
    /// This class represent the chat client. 
    /// </summary>
    public partial class clientGUI : Form
    {
        private Networking channel;

        private ILogger logger;

        /// <summary>
        /// This is a constructor of the chat gui.
        /// When the program start running, the client will create a new Networking object,
        /// and set the name of this client as local mechine name
        /// The termination char of this client is '\n'.
        /// The default address of target server is the local ip address.
        /// </summary>
        public clientGUI()
        {
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');

            InitializeComponent();

            // Get local ip address, and set the text of server box.
            IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());
            ServerBox.Text = localAddr.ToString();

            // Set the name of this client as local machine name
            ClientNameBox.Text = Environment.MachineName;
            channel.ID = ClientNameBox.Text;
        }

        /// <summary>
        /// When the connect button click, this.channel will start trying to connect to the target server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                channel.Connect(ServerBox.Text, 11000);
            }
            catch (Exception ex)
            {
                ServerBox.Text = "Wrong Address.";
            }
        }

        /// <summary>
        /// When the send button click, this.channel will start sending message to connecting server.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            logger?.LogDebug($"Send message: [{TypeBox.Text}] to the server");

            // Add the termination char to the message
            string message = TypeBox.Text + "\n";
            channel.Send(message);
            TypeBox.Clear();
        }

        /// <summary>
        /// When the GUI closed, the clannel will be disconnected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clientGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            channel.Disconnect();
        }

        // Call back and helper methods    ----------------------------------------------------------------------------------------------

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when connected successfully.
        /// </summary>
        /// <param name="channel"></param> This.channel
        public void onConnect(Networking channel)
        {
            // Log this event.
            logger?.LogInformation($"Client {channel.ID} connected to server successfully.");

            // Send command message to change the name of this client, and request for the participants.
            channel.Send($"Command Name {ClientNameBox.Text}\n");
            channel.Send("Command Participants\n");

            // Let the user can not call connect again and show connected successfully.
            ConnectButton.Enabled = false;
            ServerBox.Enabled = false;
            ClientNameBox.Enabled = false;
            ConnectButton.Text = "Connected!";
            BoxOfMessages.Text += $"Connected to the server {channel.client.Client.RemoteEndPoint.ToString()} {Environment.NewLine}";

            // Start waiting for message.
            channel.ClientAwaitMessagesAsync();
        }

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when this client disconnected from the server.
        /// </summary>
        /// <param name="channel"></param> This.channel
        public void onDisconnect(Networking channel)
        {
            logger?.LogInformation($"Client {channel.ID} disconnected from the server.");
            channel.Send(channel.ID + " disconnect!\n");
        }

        /// <summary>
        /// This is a call back method for this.channel.
        /// This method will be called when this client recieved message.
        /// </summary>
        /// <param name="channel"></param> This.channel
        public void onMessage(Networking channel, string message)
        {
            logger?.LogDebug($"Recieved message: [{message}] from the server");

            string backupMessage = BoxOfMessages.Text;

            // Show the message on the GUI
            BoxOfMessages.Text += message + Environment.NewLine;

            // Check the command participants message
            if (message.Contains("Command Participants") && !message.Contains("-Command Participants"))
            {
                // Refresh the participant box
                ParticipantBox.Clear();
                string rawPart = message.Substring(21, message.Length - 21);
                // Split the participants, and show them on the participant box
                string[] participants = rawPart.Split(",");
                foreach (string participant in participants)
                {
                    ParticipantBox.Text += participant + Environment.NewLine;
                }
                // Use the backup Message to avoid show command message
                BoxOfMessages.Text = backupMessage;
            }
        }

        /// <summary>
        /// This is a helper method to get the lcoal ip address, using as the defualt server address.
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