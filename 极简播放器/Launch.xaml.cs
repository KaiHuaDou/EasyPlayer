using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace 极简播放器;

/// <summary>
/// Launch.xaml 的交互逻辑
/// </summary>
public partial class Launch : Window
{
    public Launch( )
    {
        InitializeComponent( );
    }

    private void Close_Click(object sender, RoutedEventArgs e)
        => Close( );

    private void MiniButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;
    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed) DragMove( );
    }

    private bool IsThereVideo(string fileName) => false;

    private void Open_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new( )
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Multiselect = false,
            Title = TitleLabel.Content.ToString( ),
            Filter = "音视频文件|*.wma;*.wmv;*.wm;*.wax;*.wvx;*.wmx;*.avi;.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.wav;*.mov;*.m4a;*.mp4;*.m4v;*.mp4v;*.3g2;*.3gp2;*.3gp;*.3gpp;*.aac;*.adt;*.adts"
        };
        if (ofd.ShowDialog( ) == true)
        {
            if (IsThereVideo(ofd.FileName) == false)
            {
                new PlayVideo(ofd.FileName).Show( );
            }
            else
            {
                new PlayVideo(ofd.FileName).Show( );
            }
        }
    }
}
