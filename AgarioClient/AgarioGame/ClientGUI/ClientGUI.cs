using AgarioModels;
using Communications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text.Json;

/// <summary> 
/// Author:    Jiawen Wang
/// Partner:   Zhuowen Song
/// Date:      4/14/2022
/// Course:    CS 3500, University of Utah, School of Computing 
/// Copyright: CS 3500 and Jiawen Wang - This work may not be copied for use in Academic Coursework. 
/// 
/// I, Jiawen Wang, certify that I wrote this code from scratch and did not copy it in part or whole from  
/// another source.  All references used in the completion of the assignment are cited in my README file. 
/// 
/// File Contents 
/// This file contains code for the Game Client GUI, allowing a user to connect to a server and play this game with other players.
/// </summary>
///
namespace ClientGUI
{
    /// <summary>
    /// This class represent the game client. 
    /// </summary>
    public partial class ClientGUI : Form
    {
        // Private game world object, which is used to store the game objects of the game.
        private World world = new();


        // The player of this client.
        private Player client_player = new();

        // The net working object which is used to connect to the server, recieving and sending messages.
        private Networking channel;

        // The following
        private float clientPlayer_X_world;
        private float clientPlayer_Y_world;
        private double clientPlayer_Radius;

        // scale factor from client player size to zoom in screen size
        private int rate_constant = 0;

        // zoom in screen size
        private int ZoomRect_Width = 0;
        private int ZoomRect_Height = 0;

        // zoom in screen coordinate
        private int ZoomRect_X_left = 0;
        private int ZoomRect_X_right = 0;
        private int ZoomRect_Y_up = 0;
        private int ZoomRect_Y_down = 0;

        // values of the screen
        private const int screen_width = 1050;
        private const int screen_height = 1050;

        private int die_times = 0;

        private readonly ILogger<ClientGUI> _logger;


        /// <summary>
        /// The constructor of this GUI.
        /// </summary>
        public ClientGUI()
        {
            // Create a new net working .
            channel = new Networking(NullLogger.Instance, onConnect, onDisconnect, onMessage, '\n');
            InitializeComponent();

            // Max FPS
            DoubleBuffered = !DoubleBuffered;

            // Get local ip address, and set the text of server box.
            IPAddress localAddr = IPAddress.Parse(GetLocalIPAddress());
            ServerBox.Text = localAddr.ToString();
            
        }


        /// <summary>
        /// Resolve the event when the player click the play button and start playing the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            Invoke(() => ErrorBox.Text = "Connecting.....");
            try
            {
                channel.ID = PlayerNameBox.Text;
                channel.Connect(ServerBox.Text, 11000);
                channel.ClientAwaitMessagesAsync();
            }
            catch (Exception ex)
            {
                Invoke(() => ErrorBox.Text = "Wrong Server Address.");
            }
        }

        /// <summary>
        /// Draw the world on the GUI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Draw_Scene(object? sender, PaintEventArgs e)
        {
            // Draw the basic scene
            SolidBrush brush = new(Color.Gray);
            Pen pen = new(new SolidBrush(Color.Black));
            e.Graphics.DrawRectangle(pen, 0, 0, screen_width, screen_height);
            e.Graphics.FillRectangle(brush, 0, 0, screen_width, screen_height);

            if (this.world.Players.Keys.Contains(client_player.ID))
            {
                move_client();
            }

            Invoke(() => PositionBox.Text = $"{client_player.X}, {client_player.Y}");

            Invoke(() => MassBox.Text = $"{client_player.Mass}");
            Invoke(() => FoodBox.Text = this.world.Foods.Count.ToString());
            Invoke(() => PlayersBox.Text = this.world.Players.Count.ToString());


            //draw fixed position players and foods into the map of screen size
            Draw_Foods(e);
            Draw_Players(e);

        }


        /// <summary>
        /// Move client player as mouse moves, send the direction message to server to update 
        /// client player's location
        /// </summary>
        private void move_client()
        {
            // calculate the zoom in screen size before moving client player
            SetZoomPortolInfo();

            // destinatiion of client player as mouse move
            int direction_X = (int)client_player.X + (MousePosition.X - 550 - this.Location.X);
            int direction_Y = (int)client_player.Y + (MousePosition.Y - 550 - this.Location.Y);

            Invoke(() => MouseBox.Text = $"{MousePosition.X - this.Location.X}, {MousePosition.Y - this.Location.Y}");
            Invoke(() => DirectionBox.Text = $"{client_player.X - direction_X}, {client_player.Y - direction_Y}");

            channel.Send(string.Format(Protocols.CMD_Move, direction_X, direction_Y) + "\n");
        }

        /// <summary>
        /// Compute zoom in screen size and position based on the client player's position
        /// </summary>
        private void SetZoomPortolInfo()
        {
            clientPlayer_X_world = world.Players[client_player.ID].X;
            clientPlayer_Y_world = world.Players[client_player.ID].Y;
            clientPlayer_Radius = Math.Sqrt(world.Players[client_player.ID].Mass / Math.PI);

            rate_constant = 10;
            ZoomRect_Width = Convert.ToInt32(2 * clientPlayer_Radius * rate_constant);
            ZoomRect_Height = Convert.ToInt32(2 * clientPlayer_Radius * rate_constant);

            ZoomRect_X_left = Convert.ToInt32(clientPlayer_X_world - (ZoomRect_Width / 2));
            ZoomRect_X_right = Convert.ToInt32(clientPlayer_X_world + (ZoomRect_Width / 2));
            ZoomRect_Y_up = Convert.ToInt32(clientPlayer_Y_world - (ZoomRect_Height / 2));
            ZoomRect_Y_down = Convert.ToInt32(clientPlayer_Y_world + (ZoomRect_Height / 2));
        }


        /// <summary>
        /// A private helper method to draw all of the foods from the Json list of foods into the world.
        /// </summary>
        /// <param name="e"></param>
        private void Draw_Foods(PaintEventArgs e)
        {
            foreach (Food food in world.Foods.Values)
            {
                if (food.X < ZoomRect_X_right && food.X > ZoomRect_X_left && food.Y > ZoomRect_Y_up && food.Y < ZoomRect_Y_down)
                {
                    float X_rate = (food.X - (clientPlayer_X_world - ZoomRect_Width / 2)) / ZoomRect_Width;
                    float Y_rate = (food.Y - (clientPlayer_Y_world - ZoomRect_Height / 2)) / ZoomRect_Height;

                    float food_x = 1050 * X_rate;
                    float food_y = 1050 * Y_rate;

                    int food_radius = (int)Math.Sqrt(food.Mass / Math.PI);
                    Color food_color = Color.FromArgb((int)food.ARGBColor);
                    SolidBrush food_brush = new(food_color);

                    e.Graphics.FillEllipse(food_brush, new Rectangle((int)food_x - food_radius, (int)food_y - food_radius, 5 * food_radius, 5 * food_radius));
                }
            }
        }

        /// <summary>
        /// A private helper method to draw all of the players from the Json list of foods into the world.
        /// </summary>
        /// <param name="e"></param>
        private void Draw_Players(PaintEventArgs e)
        {
            foreach (Player player in world.Players.Values)
            {
                if (player.X < ZoomRect_X_right && player.X > ZoomRect_X_left && player.Y > ZoomRect_Y_up && player.Y < ZoomRect_Y_down)
                {
                    float X_rate = (player.X - (clientPlayer_X_world - ZoomRect_Width / 2)) / ZoomRect_Width;
                    float Y_rate = (player.Y - (clientPlayer_Y_world - ZoomRect_Height / 2)) / ZoomRect_Height;

                    float player_x = screen_width * X_rate;
                    float player_y = screen_height * Y_rate;

                    int player_radius = (int)Math.Sqrt(player.Mass / Math.PI);
                    Color player_color = Color.FromArgb((int)player.ARGBColor);
                    SolidBrush player_brush = new(player_color);

                    e.Graphics.FillEllipse(player_brush, new Rectangle((int)player_x - player_radius, (int)player_y - player_radius, 5 * player_radius, 5 * player_radius));

                    // Show the name of the player
                    Draw_Player_Name(e, player, player_x, player_y);

                    if (this.world.Players.Keys.Contains(client_player.ID))
                    {
                        this.client_player = world.Players[this.client_player.ID];

                    }
                }
            }
        }

        /// <summary>
        /// A private helper method to draw the name of every player on the screen.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="player"> a player object </param>
        /// <param name="player_x"> the X coordinate of the player in screen size </param>
        /// <param name="player_y"></param>
        private static void Draw_Player_Name(PaintEventArgs e, Player player, float player_x, float player_y)
        {
            string drawString = player.Name;
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            float x = (int)player_x;
            float y = (int)player_y;
            StringFormat drawFormat = new StringFormat();
            e.Graphics.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        /// <summary>
        /// Send message about client player's name to server using Protocols.
        /// Will tranform from Welcome page to game page.
        /// Adding timer to draw scene 30 times a second.
        /// </summary>
        /// <param name="channel"> Networking object of this client </param>
        public void onConnect(Networking channel)
        {
            Invoke(() => ErrorBox.Text = "Welcome to the game!");
            string start_message = String.Format(Protocols.CMD_Start_Game, PlayerNameBox.Text);
            channel.Send(start_message + '\n');

            this.Controls.Remove(PlayerNameLabel);
            this.Controls.Remove(PlayerNameBox);
            this.Controls.Remove(ServerLabel);
            this.Controls.Remove(ServerBox);
            this.Controls.Remove(ConnectButton);
            this.Controls.Remove(WelcomeLabel);
            this.GameOverLabel.Visible = false;

            this.Paint += Draw_Scene;
            this.KeyDown += Split_KeyDown;


            // Use a timer to redraw the world 30 times a second.
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000 / 30;  // 1000 milliseconds in a second divided by 30 frames per second
            timer.Tick += (a, b) => this.Invalidate();
            timer.Start();
        }


        /// <summary>
        /// Use different private helper method to handle the different kind of 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public void onMessage(Networking channel, string message)
        {
            // Handle player object command message.
            if (message.Contains(Protocols.CMD_Player_Object))
            {
                // Set the ID of player of this client when connect to the server.
                client_player.ID = long.Parse(message[Protocols.CMD_Player_Object.Length..]!);
                return;
            }
            // Handle food command message.
            else if (message.Contains(Protocols.CMD_Food))
            {
                Add_New_Foods(message);
                return;
            }
            // Handle eaten food command message.
            else if (message.Contains(Protocols.CMD_Eaten_Food))
            {
                Remove_Eaten_Foods(message);
                return;
            }
            // Handle heart beat command message.
            else if (message.Contains(Protocols.CMD_HeartBeat))
            {
                UpdateHeartBeat(message);
                return;
            }
            // Handle update command message.
            else if (message.Contains(Protocols.CMD_Update_Players))
            {
                Update_Players(message);
                return;
            }
            // Handle dead players command message.
            else if (message.Contains(Protocols.CMD_Dead_Players))
            {
                Remove_Dead_Players(message);
                return;
            }
            Invoke(() => ErrorBox.Text = "Can not recognize : " + message);

        }

        /// <summary>
        /// Add ClientAwaitMessagesAsync based on our own Networking class. Other than that,
        /// don't do anything when client disconnect.
        /// </summary>
        /// <param name="channel">this.channel</param>
        public void onDisconnect(Networking channel)
        {
            channel.ClientAwaitMessagesAsync();
        }

        /// <summary>
        /// This method will be called when received CMD_Food command message.
        /// 
        /// This method is used to add the new foods into the world object.
        /// </summary>
        /// <param name="message"> A message about CMD_Food from server </param>
        private void Add_New_Foods(string message)
        {
            // 1. get the foods list from Json message.
            string foods_Json = message[Protocols.CMD_Food.Length..]!;
            List<Food> foodList = JsonSerializer.Deserialize<List<Food>>(foods_Json);

            // 2. add the new foods into the world
            foreach (Food checking_food in foodList)
            {
                world.Foods[checking_food.ID] = checking_food;
            }
            if (foodList.Count > 0)
                world.world_logger?.LogInformation($"Added {foodList.Count} foods into the world");
        }

        /// <summary>
        /// This method will be called when received CMD_Eaten_Food command message.
        /// 
        /// This method is used to remove the eaten foods from the world object.
        /// </summary>
        /// <param name="message"> a message about CMD_Eaten_Food from server </param>
        private void Remove_Eaten_Foods(string message)
        {
            string eatens_Json = message[Protocols.CMD_Eaten_Food.Length..]!;
            List<long> eatenIDs = JsonSerializer.Deserialize<List<long>>(eatens_Json);

            foreach (long checking_eaten_ID in eatenIDs)
            {
                if (world.Foods.Keys.Contains(checking_eaten_ID))
                {
                    world.Foods.Remove(checking_eaten_ID);
                }
            }

            if (eatenIDs.Count > 0)
                world.world_logger?.LogInformation($"Remove {eatenIDs.Count} foods from the world");
        }


        /// <summary>
        /// Receive dead players ID from server and remove them from players list
        /// </summary>
        /// <param name="message"></param>
        private void Remove_Dead_Players(string message)
        {
            string dead_Json = message[Protocols.CMD_Dead_Players.Length..]!;
            List<long> deadIDs = JsonSerializer.Deserialize<List<long>>(dead_Json);
            world.world_logger?.LogDebug($"Try to remove dead players: {dead_Json}.");

            foreach (long checking_dead_ID in deadIDs)
            {
                if (checking_dead_ID == client_player.ID)
                {
                    world.Players.Remove(checking_dead_ID);
                    client_player = new Player();
                    GameOverLabel.Visible = true;
                    ReplayNameLabel.Visible = true;
                    ReplayBox.Visible = true;
                    ReplayButton.Visible = true;
                    ReplayButton.Enabled = true;
                    die_times++;
                    world.world_logger?.LogInformation("This player died.");
                    DieBox.Text = die_times.ToString();
                    this.Paint -= Draw_Scene;
                }
                else
                {
                    world.Players.Remove(checking_dead_ID);
                }
                if (deadIDs.Count > 0)
                    world.world_logger?.LogInformation($"Remove {deadIDs.Count} dead players.");
            }
        }


        /// <summary>
        /// This method will be called when received CMD_Eaten_Food command message.
        /// 
        /// This method is used to remove the eaten foods from the world object.
        /// </summary>
        /// <param name="message"> a message about CMD_Update_Players from server </param>
        private void Update_Players(string message)
        {
            string players_Json = message[Protocols.CMD_Update_Players.Length..]!;
            List<Player> playerList = JsonSerializer.Deserialize<List<Player>>(players_Json);

            foreach (Player checking_player in playerList)
            {
                world.Players[checking_player.ID] = checking_player;
            }
            world.world_logger?.LogInformation($"{playerList.Count} players in the world.");
        }

        /// <summary>
        /// A helper method to update HeartBeat of this game on screen when receive a message
        /// from server
        /// </summary>
        /// <param name="message"> a message about CMD_HeartBeat from server </param>
        private void UpdateHeartBeat(string message)
        {
            string heartBeat = message[Protocols.CMD_HeartBeat.Length..]!;
            int heartBeatValue = JsonSerializer.Deserialize<int>(heartBeat);
            Invoke(() => HeartBeatBox.Text = heartBeatValue.ToString());
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

        /// <summary>
        /// When RePlay button clicked, client will not be disconnected from server, but will provide
        /// a textbox to enter a name to restart the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReplayButton_Click_1(object sender, EventArgs e)
        {
            world.world_logger?.LogDebug($"Try to restart the game");
            string start_message = String.Format(Protocols.CMD_Start_Game, ReplayBox.Text);
            channel.Send(start_message + "\n");

            this.Paint += Draw_Scene;
            this.KeyDown += Split_KeyDown;

            GameOverLabel.Visible = false;
            ReplayNameLabel.Visible = false;
            ReplayBox.Visible = false;
            ReplayBox.Visible = false;
            ReplayButton.Visible = false;
            ReplayButton.Enabled = false;
        }

        /// <summary>
        /// Send deatition of one of the splitted client circle to server when spacebar key press detected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Split_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                string split_message = String.Format(Protocols.CMD_Split, (int)client_player.X, (int)client_player.Y);
                channel.Send(split_message + "\n");
            }
        }

    }
}

