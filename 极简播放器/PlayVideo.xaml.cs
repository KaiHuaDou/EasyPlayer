using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace 极简播放器;

/// <summary>
/// PlayVideo.xaml 的交互逻辑
/// </summary>
public partial class PlayVideo : Window
{
    public PlayVideo(string file)
    {
        InitializeComponent( );
        player.Source = new Uri(file);
        player.LoadedBehavior = MediaState.Manual;
        fileName = file;
        Title = StdApi.SubName(Path.GetFileName(player.Source.ToString( )), 23) + " - 极简播放器";
        TitleLabel.Content = Title;
        timer.Interval = 1000;
        timer.Elapsed += Timer_Elapsed;
        timer.Start( );
        try
        {
            TotalDur = StdApi.GetVideoDuration(fileName);
            TotalDurLabel.Content = TotalDur;
            ProgressSlider.Maximum = TimeSpan.Parse(TotalDur).TotalSeconds;
        }
        catch (Exception) { }
    }
    public static bool isPlaying = false;
    public static Timer timer = new( );
    public static string TotalDur;
    public static string fileName;
    public static bool isMaxed = false;
    public WindowStatus status = new( );
    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Dispatcher.BeginInvoke(( ) =>
        {
            ProgressSlider.Value = player.Position.TotalSeconds;
            PlayedDurLabel.Content = player.Position.ToString(@"hh\:mm\:ss");
        });
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed && isMaxed == false) DragMove( );
    }

    private void Close_Click(object sender, RoutedEventArgs e)
        => Close( );

    private void MiniButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    private void Play_Click(object sender, RoutedEventArgs e)
    {
        if (isPlaying)
        {
            player.Pause( );
            PlayButton.Content = "\uF5B0";
            isPlaying = false;
        }
        else
        {
            player.Play( );
            PlayButton.Content = "\uF8AE";
            isPlaying = true;
        }
    }

    private void Progress_ValueChanged(object sender, MouseButtonEventArgs e)
        => player.Position = TimeSpan.FromSeconds(ProgressSlider.Value);

    private void SetStatus( )
    {
        status = new WindowStatus
        {
            windowState = WindowState,
            windowStyle = WindowStyle,
            resizeMode = ResizeMode,
            topMost = Topmost,
            left = Left,
            top = Top,
            height = Height,
            width = Width
        };
    }

    private void WindowsMaxiClick(object sender, RoutedEventArgs e)
    {
        if (isMaxed == false)
        {
            SetStatus( );
            MainBorder.CornerRadius = new CornerRadius(0);
            button.Content = "\uE73F";
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;
            Left = 0.0;
            Top = 0.0;
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
        }
        else
        {
            WindowState = status.windowState;
            WindowStyle = status.windowStyle;
            ResizeMode = status.resizeMode;
            Topmost = status.topMost;
            Left = status.left;
            Top = status.top;
            Width = status.width;
            Height = status.height;
            MainBorder.CornerRadius = new CornerRadius(20);
            button.Content = "\uE740";
        }
        isMaxed = !isMaxed;
    }

    private void SpeedRadioChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            string ratio = ((ComboBoxItem) sppedRadio.SelectedItem).Content.ToString( );
            player.SpeedRatio = Convert.ToDouble(ratio.TrimEnd('x'));
        }
        catch (Exception) { }
    }
}
