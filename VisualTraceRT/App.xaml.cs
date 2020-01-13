using System.Windows;

namespace VisualTraceRT
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            int Updated = 0, PID = 0;
            // 更新后第一次启动参数
            if (e.Args.Length >= 3 && 
                int.TryParse(e.Args[0], out Updated) == true &&
                int.TryParse(e.Args[1], out PID) == true)
            {
                AutoUpdate.CheckVersion(Updated, PID, e.Args[2]);
            }
            // 正常更新检测
            else
            {
                AutoUpdate.CheckVersion();
            }
        }
    }
}
