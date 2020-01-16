using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace VisualTraceRT
{
    /// <summary>
    /// 新字符串行委托
    /// </summary>
    /// <param name="line">cmd传回的字符串行</param>
    public delegate void NewCMDLine(string line);
    /// <summary>
    /// 命令运行结束委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void NoMoreCMD(EventArgs e);
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public event NewCMDLine ReadCMDLine;
        public event NoMoreCMD EndCMD;
        /// <summary>
        /// 绑定到DataGrid的数据列表
        /// </summary>
        private ObservableCollection<Hop> HopList = new ObservableCollection<Hop>();
        public MainWindow()
        {
            InitializeComponent();
            ReadCMDLine += new NewCMDLine(ReadCMDLineAction);
            EndCMD += new NoMoreCMD(EndCMDAction);
            traceMap.LoadCompleted += TraceMap_LoadCompleted;
            ShowMap();
        }
        /// <summary>
        /// 地图初始化后允许tracert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceMap_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Trace.IsEnabled = true;
        }
        /// <summary>
        /// 初始化显示地图
        /// </summary>
        private void ShowMap()
        {
            // 地图页面
            string str_MapInside = @"<!doctype html>
                                        <html lang='en'>
                                        <head>
                                            <meta charset='utf-8'>
                                            <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                                            <meta name='viewport' content='initial-scale=1.0, user-scalable=no, width=device-width'>
                                            <style>
                                                html,
                                                body,
                                                #container {
                                                    width: 100%;
                                                    height: 100%;
                                                }
                                            </style>
                                        </head>
                                        <body scroll='no'>
                                            <div id='container' class='map' tabindex='0'></div>
                                            <script src='https://webapi.amap.com/maps?v=1.4.15&key=259c97b0e4e74d590717244ccc7d8619'></script>
                                            <script type='text/javascript'>
                                                var map = new AMap.Map('container');
                                                var traceroute = [];
                                                // 清除地图
                                                function MapClear()
                                                {
                                                    traceroute = [];
                                                    map.clearMap();
                                                }
                                                // 添加新路由节点
                                                function AddNode(Longitude, Latitude)
                                                {
                                                    traceroute.push([Longitude, Latitude]);
                                                }
                                                // 显示路由路径
                                                function ShowRoute()
                                                {
                                                    for (var i = 0; i < traceroute.length; i++) {
                                                        new AMap.Marker({
                                                            map: map,
                                                            position: traceroute[i]
                                                        });
                                                    }
                                                    var polyline = new AMap.Polyline({
                                                        path: traceroute,
                                                        showDir: true,
                                                        strokeColor: '#FF0000',
                                                        strokeWeight: 8,
                                                        lineJoin: 'round',
                                                        lineCap: 'round'
                                                    });

                                                    map.add([polyline]);
                                                    map.setFitView();
                                                }
                                            </script>
                                        </body>
                                        </html>";
            // 显示地图
            this.traceMap.NavigateToString(str_MapInside);
        }
        /// <summary>
        /// 获取单跳的详细数据
        /// </summary>
        /// <param name="node">单跳节点</param>
        private void SetHopDetails(Hop node)
        {
            WebRequest request= WebRequest.Create("http://ip-api.com/line/" + node.IP + "?lang=zh-CN");
            WebResponse response = request.GetResponse();
            Stream s = response.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            // IP地址数据是否有效
            string str_status = sr.ReadLine();
            if (str_status.Equals("success") == true)
            {
                // 设置地理位置
                node.Geo = sr.ReadLine();
                // 国家编号
                sr.ReadLine();
                // 区域编号
                sr.ReadLine();
                // 设置地理位置(区域名称)
                node.Geo += " " + sr.ReadLine();
                // 设置地理位置(城市名称)
                node.Geo += " " + sr.ReadLine();
                // 邮政编码
                sr.ReadLine();
                // 设置纬度
                node.Lat = sr.ReadLine();
                // 设置经度
                node.Lon = sr.ReadLine();
                // 时区
                sr.ReadLine();
                // 设置互联网服务提供商
                node.ISP = sr.ReadLine();
                // 设置组织机构
                node.Org = sr.ReadLine();
                // 设置自治系统
                node.AS = sr.ReadLine();
            }
            sr.Dispose();
            sr.Close();
            s.Dispose();
            s.Close();
        }
        /// <summary>
        /// 命令行调用tracert
        /// </summary>
        /// <param name="IP"></param>
        private void TraceByCMD(string IP)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            // 使用windows自带tracert, 超时1s, 最大跃点30
            p.StartInfo.Arguments = "/c tracert -d -w 1000 -h 30 " + IP;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.OutputDataReceived += P_OutputDataReceived;
            p.Exited += P_Exited;
            p.EnableRaisingEvents = true;
            p.Start();
            p.BeginOutputReadLine();
        }
        /// <summary>
        /// 捕获到命令运行结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P_Exited(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(EndCMD, new object[] { e } );
        }
        /// <summary>
        /// 捕获到新的重定向输出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.Dispatcher.Invoke(ReadCMDLine, new object[] { e.Data });
            }
        }
        /// <summary>
        /// 获取新的字符串行动作
        /// </summary>
        /// <param name="result"></param>
        private void ReadCMDLineAction(string line)
        {
            // 跳数(距离)
            int dist = 0;
            string[] items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length >= 5 && Int32.TryParse(items[0], out dist) == true)
            {
                // 时延数量
                int num_delay = 0;
                // 实例化新节点
                Hop newHop = new Hop();
                newHop.Distance = items[0];
                int index_items = 1;
                for (; index_items < items.Length && num_delay < 3; index_items++)
                {
                    if (items[index_items].Equals("ms") == false && 
                        items[index_items].Equals("毫秒") == false)
                    {
                        newHop.Delay += items[index_items];
                        if (++num_delay != 3)
                        {
                            newHop.Delay += " / ";
                        }
                    }
                }
                while (items[index_items].Equals("ms") == true ||
                        items[index_items].Equals("毫秒") == true)
                {
                    ++index_items;
                }
                newHop.IP = items[index_items];
                SetHopDetails(newHop);
                HopList.Add(newHop);
                TraceGrid.ItemsSource = HopList;
                if (newHop.Lon != null &&
                    newHop.Lat != null)
                {
                    traceMap.InvokeScript("AddNode", new object[] { newHop.Lon, newHop.Lat });
                }
            }
        }
        /// <summary>
        /// 命令运行结束后动作
        /// </summary>
        /// <param name="e"></param>
        private void EndCMDAction(EventArgs e)
        {
            // 显示路由路径
            traceMap.InvokeScript("ShowRoute");
            Trace.Content = "TraceRT";
            Trace.IsEnabled = true;
        }
        /// <summary>
        /// 单击TraceRT按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Trace.IsEnabled = false;
            Trace.Content = "Tracing...";
            // 清除地图覆盖物
            traceMap.InvokeScript("MapClear");
            HopList.Clear();
            TraceByCMD(targetIP.Text);
        }
    }
}
