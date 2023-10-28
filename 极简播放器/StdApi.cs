using System.Diagnostics;
using System.IO;
using System.Windows;

namespace 极简播放器;

public struct WindowStatus
{
    public WindowState windowState;
    public WindowStyle windowStyle;
    public ResizeMode resizeMode;
    public bool topMost;
    public double left;
    public double top;
    public double width;
    public double height;
}
public class StdApi
{
    public static string GetVideoDuration(string sourceFile)
    {
        using Process ffmpeg = new( );
        string duration;
        string result;
        StreamReader errorreader;
        ffmpeg.StartInfo.UseShellExecute = false;
        ffmpeg.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        ffmpeg.StartInfo.RedirectStandardError = true;
        ffmpeg.StartInfo.FileName = App.Program.FFmpegPath;
        ffmpeg.StartInfo.Arguments = "-i " + sourceFile;
        ffmpeg.StartInfo.CreateNoWindow = true;
        ffmpeg.Start( );
        errorreader = ffmpeg.StandardError;
        ffmpeg.WaitForExit( );
        result = errorreader.ReadToEnd( );
        duration = result.Substring(result.IndexOf("Duration: ") + "Duration: ".Length, "00:00:00".Length);
        return duration;
    }
    public static string SubName(string str, int length)
        => str.Length <= length ? str : str.Substring(0, length - 3) + "..." + str.Substring(str.Length - 3, 3);
}
