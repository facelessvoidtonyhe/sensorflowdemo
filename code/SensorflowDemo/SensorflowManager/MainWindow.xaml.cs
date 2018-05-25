using Fleck;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SensorflowManager
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        string websoketPort = ConfigurationManager.AppSettings["WebsoketPort"] ?? "8000";
        string websoketIp = ConfigurationManager.AppSettings["WebsoketIp"] ?? "127.0.0.1";
        string mqttServerIp = ConfigurationManager.AppSettings["MqttServerIp"] ?? "127.0.0.1";
        string mqttServerPort = ConfigurationManager.AppSettings["MqttServerPort"] ?? "1884";
        List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        WebSocketServer server = null;
        private IMqttClient mqttClient = null;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitWebSocket();
        }

        /// <summary>
        /// 初始化WebSocket
        /// </summary>
        private void InitWebSocket()
        {
            #region WebSocket部分
            allSockets = new List<IWebSocketConnection>();
            server = new WebSocketServer(string.Format("ws://{0}:{1}", websoketIp, websoketPort));
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    NLog.LogManager.GetCurrentClassLogger().Debug("Open!");
                    Dispatcher.Invoke((new Action(() =>
                    {
                        tb_message.AppendText("Open！" + Environment.NewLine);
                    })));
                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                    NLog.LogManager.GetCurrentClassLogger().Debug("Close!");
                    Dispatcher.Invoke((new Action(() =>
                    {
                        tb_message.AppendText("Close！" + Environment.NewLine);
                    })));
                };
                //接受网页发过来的数据
                socket.OnMessage = message =>
                {
                    try
                    {
                        Dispatcher.Invoke((new Action(() =>
                        {
                            string returnStr = "接收到了";
                            allSockets.ToList().ForEach(s => s.Send(returnStr));
                            tb_message.AppendText("接受到Message:" + message + Environment.NewLine);
                        })));
                    }
                    catch (Exception ex)
                    {

                    }
                };
            });
            #endregion
            #region MQTT客户端部分
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                 .WithTcpServer(mqttServerIp, int.Parse(mqttServerPort)) // Port is optional
                 .Build();
            mqttClient.ConnectAsync(options);
            mqttClient.Connected += MqttClient_Connected;
            mqttClient.Disconnected += MqttClient_Disconnected;
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived1;
            #endregion
        }

        private void MqttClient_ApplicationMessageReceived1(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Dispatcher.Invoke((new Action(() =>
            {
                tb_message.AppendText($">> {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}{Environment.NewLine}");
            })));
            //AddMessage(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        }

        private void MqttClient_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            AddMessage("已从MQTT服务器断开!");
        }

        private void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            AddMessage("已连接到MQTT服务器！");
            string topic = "sample-values/rack-1/tag-1/asset";
            mqttClient.SubscribeAsync(new List<TopicFilter> {
                new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
            });
            AddMessage(string.Format("已订阅[{0}]主题",topic));
        }

        private void AddMessage(string msg)
        {
            Dispatcher.Invoke((new Action(() =>
            {
                tb_message.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + msg + Environment.NewLine);
                if (tb_message.Text.Length >= 100000)
                {
                    tb_message.Text = "";
                }
            })));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

        }
    }
}
