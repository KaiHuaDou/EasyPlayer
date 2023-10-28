using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace 极简播放器;

/// <summary>
/// App.xaml 的交互逻辑
/// </summary>
public partial class App : Application
{
    public static class Program
    {
        public static string AppPath = Path.GetDirectoryName(Process.GetCurrentProcess( ).MainModule.FileName);
        public static string FFmpegPath = Path.Combine(AppPath, "ffmpeg.exe");
        [STAThread]
        public static void Main( )
        {
            App app = new( );
            app.InitializeComponent( );
            app.StartupUri = new Uri("Launch.xaml", UriKind.Relative);
            app.Run( );
        }
    }
}
