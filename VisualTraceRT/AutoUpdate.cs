using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;

namespace VisualTraceRT
{
    /// <summary>
    /// 自动升级
    /// </summary>
    public static class AutoUpdate
    {
        /// <summary>
        /// 当前版本号
        /// </summary>
        private const double nowVersion = 1.1;
        /// <summary>
        /// 当前程序绝对路径
        /// </summary>
        private static string nowPath = System.Windows.Application.Current.GetType().Assembly.Location;
        /// <summary>
        /// 新程序绝对路径
        /// </summary>
        private static string newPath = string.Empty;
        /// <summary>
        /// 检查新版本
        /// </summary>
        /// <param name="Updated">升级后第一次启动</param>
        /// <param name="oldPID">旧版本进程PID</param>
        /// <param name="oldPath">旧版本程序路径</param>
        public static void CheckVersion(int Updated = 0, int oldPID = 0, string oldPath = "")
        {
            try
            {
                WebRequest request = WebRequest.Create("https://injectrl.github.io/VisualTraceRT/CheckVersion.html");
                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s);
                // 最新版本号
                double latestVersion = double.Parse(sr.ReadLine());
                // 有新版本
                if (latestVersion > nowVersion)
                {
                    // 是否强制升级
                    string Force = sr.ReadLine();
                    // 新版本下载地址
                    string DownloadURL = sr.ReadLine();
                    // 升级公告
                    string TIP = sr.ReadToEnd();
                    // 强制升级
                    if (Force.Equals("no") == false)
                    {
                        TIP += "\n\n升级过程无需操作, 结束后将自动启动新版本 !\n点击\"是\"进行升级, \"否\"取消升级并退出";
                        // 确认升级
                        if (MessageBox.Show(TIP, "检测到新版本", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            DoUpdate(DownloadURL);
                        }
                        // 程序退出
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    // 建议升级
                    else
                    {
                        TIP += "\n\n升级过程无需操作, 结束后将自动启动新版本 !\n点击\"是\"进行升级, \"否\"本次不升级";
                        // 确认升级
                        if (MessageBox.Show(TIP, "检测到新版本", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            DoUpdate(DownloadURL);
                        }
                    }
                }
                // 更新完成后第一次启动新版本程序, 执行旧版本清理
                else if (latestVersion == nowVersion && Updated == 1)
                {
                    if (Process.GetProcessById(oldPID).HasExited == false)
                        Process.GetProcessById(oldPID).Kill();
                    File.Delete(oldPath);
                    MessageBox.Show("已经更新到最新版本", "自动升级");
                }
                sr.Dispose();
                sr.Close();
                s.Dispose();
                s.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("无法检测版本状态", "自动升级");
            }
        }
        /// <summary>
        /// 重命名当前程序并设置当前程序与新程序绝对路径
        /// </summary>
        private static void RenameNow()
        {
            if (nowPath.Equals(string.Empty) == false)
            {
                // 程序所在目录的绝对路径
                string dir = Path.GetDirectoryName(nowPath);
                // 当前程序新的绝对路径
                string renamePath = dir + "/VisualTraceRT_" + Guid.NewGuid().ToString();
                FileInfo nowFile = new FileInfo(nowPath);
                // 文件改名
                nowFile.MoveTo(renamePath);
                // 更新当前程序与新程序路径
                newPath = nowPath;
                nowPath = renamePath;
            }
        }
        /// <summary>
        /// 升级过程
        /// </summary>
        /// <param name="DownloadURL">新版本下载地址</param>
        private static void DoUpdate(string DownloadURL)
        {
            RenameNow();
            WebClient client = new WebClient();
            // 下载新版本程序
            client.DownloadFile(DownloadURL, newPath);
            // 启动新版本程序
            Process p = new Process();
            p.StartInfo.FileName = newPath;
            // 设置更新后第一次启动参数
            p.StartInfo.Arguments = "1 " + Process.GetCurrentProcess().Id.ToString() + " " + nowPath;
            p.StartInfo.UseShellExecute = false;
            Environment.Exit(0);
        }
    }
}
