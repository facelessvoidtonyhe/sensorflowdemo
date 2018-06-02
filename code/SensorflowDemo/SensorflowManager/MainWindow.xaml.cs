using Fleck;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
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
        string dbPath = ConfigurationManager.AppSettings["DBPath"];
        List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        WebSocketServer server = null;
        private IMqttClient mqttClient = null;
        private IMqttServer mqttServer = null;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitWebSocket();
            InitMQTT();
            RefreshDataGrid();
        }

        /// <summary>
        /// 初始化MQTT
        /// </summary>
        private async void InitMQTT()
        {
            #region 服务端
            var optionsBuilder = new MqttServerOptionsBuilder()
            .WithConnectionBacklog(100)
            .WithDefaultEndpointPort(1883);
            var mqttServer = new MqttFactory().CreateMqttServer();
            await mqttServer.StartAsync(optionsBuilder.Build());
            mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
            mqttServer.ClientConnected += MqttServer_ClientConnected;
            mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
            #endregion
            #region 客户端
            ClientConnect();
            #endregion
        }

        private void ClientConnect()
        {
            var factory = new MqttFactory();
            mqttClient = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                 .WithTcpServer(mqttServerIp, int.Parse(mqttServerPort)) // Port is optional
                 .Build();
            mqttClient.ConnectAsync(options);
            mqttClient.Connected += MqttClient_Connected1;
            mqttClient.Disconnected += MqttClient_Disconnected1;
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived1;
        }

        private void MqttClient_Disconnected1(object sender, MQTTnet.Client.MqttClientDisconnectedEventArgs e)
        {
            AddMessage2("已从MQTT服务器断开!");
            ClientConnect();
        }

        private void MqttClient_Connected1(object sender, MQTTnet.Client.MqttClientConnectedEventArgs e)
        {
            AddMessage2("已连接到MQTT服务器！");
            //连接成功之后，订阅机柜每一层的主题
            TopicEachLayer();
        }

        private void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            AddMessage($"客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 负荷：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
        }

        private void MqttServer_ClientConnected(object sender, MQTTnet.Server.MqttClientConnectedEventArgs e)
        {
            AddMessage($"客户端[{e.Client.ClientId}]已连接，协议版本：{e.Client.ProtocolVersion}");
        }

        private void MqttServer_ClientDisconnected(object sender, MQTTnet.Server.MqttClientDisconnectedEventArgs e)
        {
            AddMessage($"客户端[{e.Client.ClientId}]已断开连接！");
        }

        /// <summary>
        /// 初始化WebSocket
        /// </summary>
        private void InitWebSocket()
        {
            allSockets = new List<IWebSocketConnection>();
            server = new WebSocketServer(string.Format("ws://{0}:{1}", websoketIp, websoketPort));
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    allSockets.Add(socket);
                    NLog.LogManager.GetCurrentClassLogger().Debug("Open!");
                    AddMessage3("WebSocket已打开");
                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                    NLog.LogManager.GetCurrentClassLogger().Debug("Close!");
                    AddMessage3("WebSocket已关闭");
                };
                //接受网页发过来的数据
                socket.OnMessage = message =>
                {
                    try
                    {
                        AddMessage3(string.Format("接收到客户端信息：" + message));
                        Dispatcher.Invoke((new Action(() =>
                        {
                            LocationAsset(message);
                        })));
                    }
                    catch (Exception ex)
                    {

                    }
                };
            });
        }

        /// <summary>
        /// 定位资产
        /// </summary>
        /// <param name="orderNo"></param>
        private void LocationAsset(string orderNo)
        {
            SqliteHelper.BLL.Asset assetBll = new SqliteHelper.BLL.Asset(dbPath);
            var model = assetBll.GetModel(int.Parse(orderNo));
            //获取到该资产的层位为
            AddMessage3(string.Format("获取到资产层位为{0}", model.StartLayer));
            //发送控制命令
            string topic = string.Format("command/mu-test/u-{0}/led", model.StartLayer);
            string inputString = JsonConvert.SerializeObject(new SensorFlowCommond()
            {
                monitoringUnit = "mu-test",
                sampleUnit = string.Format("u-{0}", model.StartLayer),
                channel = "led",
                parameters = new parameters()
                {
                    r = 0,
                    g = 1,
                    b = 0
                },
                phase = "executing",
                timeout = 100000,
                operator1 = "admin",
                startTime = "2020-06-01T18:57:30Z"
            });
            inputString = inputString.Replace("operator1", "operator").Trim();
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            mqttClient.PublishAsync(appMsg);
            AddMessage3(string.Format("发布主题[{0}],值为：[{1}]", topic, inputString));
        }

        private void MqttClient_ApplicationMessageReceived1(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Dispatcher.Invoke((new Action(() =>
            {
                string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                tb_message2.AppendText($">> {msg}{Environment.NewLine}");
                UpdateAssetInfo(msg);
            })));
        }

        /// <summary>
        /// 更新资产信息
        /// </summary>
        /// <param name="msg"></param>
        private void UpdateAssetInfo(string msg)
        {
            SqliteHelper.BLL.RealTimeData realTimeBll = new SqliteHelper.BLL.RealTimeData(dbPath);
            var assetModel = JsonConvert.DeserializeObject<SensorFlowAsset>(msg);
            if (assetModel != null && assetModel.channelId == "asset")
            {
                //资产Id是固定的位数，位数不足会补齐*号，所以要去掉
                assetModel.value = assetModel.value.Replace("*", "");
                if (string.IsNullOrEmpty(assetModel.value))
                {
                    return;
                }
                //根据monitoringUnitId获取机柜CabinetNo，因为是Demo，所以写死是1

                var list = realTimeBll.GetModelList(string.Format("RFID='{0}'", assetModel.value));
                if (list.Count > 0)
                {
                    //更新资产实时位置
                    list[0].CabinetNo = 1;
                    list[0].LayerIndex = int.Parse(assetModel.sampleUnitId.Replace("tag-", ""));
                    list[0].UpdateTime = DateTime.Now;
                    realTimeBll.Update(list[0]);
                }
                else
                {
                    //添加资产实时位置
                    SqliteHelper.Model.RealTimeData realModel = new SqliteHelper.Model.RealTimeData();
                    realModel.CabinetNo = 1;
                    realModel.LayerIndex = int.Parse(assetModel.sampleUnitId.Replace("tag-", ""));
                    realModel.RFID = assetModel.value;
                    realModel.UpdateTime = DateTime.Now;
                    realTimeBll.Add(realModel);
                }
            }
            if (assetModel != null && assetModel.channelId == "tag")
            {
                if (assetModel.value == "0")
                {
                    //表示该U位的资产被拔掉了，删除RealTimeData记录
                    var layerIndex = int.Parse(assetModel.sampleUnitId.Replace("u-", ""));
                    var modellist = realTimeBll.GetModelList(string.Format("CabinetNo='1' and LayerIndex='{0}'", layerIndex));
                    if (modellist.Count > 0)
                    {
                        realTimeBll.Delete(modellist[0].OrderNo);
                    }
                }
            }
            if (assetModel != null && assetModel.channelId == "temperature")
            {
                SqliteHelper.BLL.Cabinet cabinetBll = new SqliteHelper.BLL.Cabinet(dbPath);
                var model = cabinetBll.GetModel(1);
                if (model != null)
                {
                    model.ColdTemp1 = assetModel.value;
                    cabinetBll.Update(model);
                }
            }

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

        private void AddMessage2(string msg)
        {
            Dispatcher.Invoke((new Action(() =>
            {
                tb_message2.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + msg + Environment.NewLine);
                if (tb_message2.Text.Length >= 100000)
                {
                    tb_message2.Text = "";
                }
            })));
        }

        private void AddMessage3(string msg)
        {
            Dispatcher.Invoke((new Action(() =>
            {
                tb_message3.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + msg + Environment.NewLine);
                if (tb_message3.Text.Length >= 100000)
                {
                    tb_message3.Text = "";
                }
            })));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            #region 客户端

            #endregion
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string topic = "sample-values/mu-test/#";
            mqttClient.SubscribeAsync(new List<TopicFilter> {
                    new TopicFilter(topic, MqttQualityOfServiceLevel.AtMostOnce)
                });
            AddMessage(string.Format("已订阅[{0}]主题", topic));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (mqttServer != null)
            {
                mqttServer.StopAsync();
            }
        }

        /// <summary>
        /// 订阅每一层的层位
        /// </summary>
        private void TopicEachLayer()
        {
            List<TopicFilter> layerFilterList = new List<TopicFilter>();
            for (int i = 1; i <= 42; i++)
            {
                string topic = string.Format("sample-values/mu-test/tag-{0}/asset", i);
                layerFilterList.Add(new TopicFilter(topic, MqttQualityOfServiceLevel.AtLeastOnce));
                string topic2 = string.Format("sample-values/mu-test/u-{0}/tag", i);
                layerFilterList.Add(new TopicFilter(topic2, MqttQualityOfServiceLevel.AtLeastOnce));
                string topic3 = string.Format("sample-values/mu-test/tag-{0}/temperature", i);
                layerFilterList.Add(new TopicFilter(topic3, MqttQualityOfServiceLevel.AtLeastOnce));
            }
            mqttClient.SubscribeAsync(layerFilterList);
            AddMessage2("已订阅U位、Tag、温度主题");
        }

        /// <summary>
        ///  刷新RealTimeData表
        /// </summary>
        private void RefreshDataGrid()
        {
            SqliteHelper.BLL.RealTimeData realTimeBll = new SqliteHelper.BLL.RealTimeData(dbPath);
            var allList = realTimeBll.GetModelList("cabinetNo='1'");
            dglist.ItemsSource = allList;
        }

        /// <summary>
        /// 删除RealTime记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                var orderno = btn.Tag.ToString();
                SqliteHelper.BLL.RealTimeData realTimeBll = new SqliteHelper.BLL.RealTimeData(dbPath);
                realTimeBll.Delete(int.Parse(orderno));
                RefreshDataGrid();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = cmb_layer.SelectedItem as ComboBoxItem;
            SqliteHelper.BLL.RealTimeData realTimeBll = new SqliteHelper.BLL.RealTimeData(dbPath);
            var list = realTimeBll.GetModelList(string.Format("LayerIndex='{0}'", int.Parse(item.Content.ToString())));
            if (list != null && list.Count > 0)
            {
                list[0].RFID = tb_rfid.Text;
                list[0].UpdateTime = DateTime.Now;
                realTimeBll.Update(list[0]);
            }
            else
            {
                var model = new SqliteHelper.Model.RealTimeData();
                model.CabinetNo = 1;
                model.LayerIndex = int.Parse(item.Content.ToString());
                model.RFID = tb_rfid.Text;
                model.UpdateTime = DateTime.Now;
                realTimeBll.Add(model);
            }
            RefreshDataGrid();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            SqliteHelper.BLL.Asset assetBll = new SqliteHelper.BLL.Asset(dbPath);
            assetBll.Update("update asset set state='0'");
            MessageBox.Show("清空成功");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            SqliteHelper.BLL.ChangeLog changeLogBll = new SqliteHelper.BLL.ChangeLog(dbPath);
            changeLogBll.Delete("delete from Changelog");
            MessageBox.Show("清空成功");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            SqliteHelper.BLL.RealTimeData realTimeBll = new SqliteHelper.BLL.RealTimeData(dbPath);
            realTimeBll.Delete("delete from RealTimeData");
            MessageBox.Show("清空成功");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            var layer = cmb_layer2.SelectedIndex + 1;
            string topic = string.Format("command/mu-test/u-{0}/led", layer);
            string inputString = JsonConvert.SerializeObject(new SensorFlowCommond()
            {
                monitoringUnit = "mu-test",
                sampleUnit = string.Format("u-{0}", layer),
                channel = "led",
                parameters = new parameters()
                {
                    r = 0,
                    g = 0,
                    b = 0
                },
                phase = "executing",
                timeout = 100000,
                operator1 = "admin",
                startTime = "2020-06-01T18:57:30Z"
            });
            inputString = inputString.Replace("operator1", "operator").Trim();
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            mqttClient.PublishAsync(appMsg);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string topic = txtPubTopic.Text.Trim();

            if (string.IsNullOrEmpty(topic))
            {
                MessageBox.Show("发布主题不能为空！");
                return;
            }

            string inputString = txtSendMessage.Text.Trim();
            var appMsg = new MqttApplicationMessage(topic, Encoding.UTF8.GetBytes(inputString), MqttQualityOfServiceLevel.AtMostOnce, false);
            mqttClient.PublishAsync(appMsg);
        }
    }
}
