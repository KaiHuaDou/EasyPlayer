using System;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 极简播放器;

/// <summary>
/// PlayAudio.xaml 的交互逻辑
/// </summary>
public partial class PlayAudio : Window
{
    private MediaElement player = new( );
    public static bool isPlaying = false;
    public static Timer timer = new( );
    public static string TotalDur;
    public static string fileName;
    public PlayAudio(string file)
    {
        InitializeComponent( );
        player = new MediaElement
        {
            Width = 0,
            Height = 0
        };
        MainGrid.Children.Add(player);
        player.Source = new Uri(file);
        player.LoadedBehavior = MediaState.Manual;
        fileName = file;
        Title = StdApi.SubName(Path.GetFileName(player.Source.ToString( )), 13) + " - 极简播放器";
        NowFileLabel.Content = Title;
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
        if (e.LeftButton == MouseButtonState.Pressed) DragMove( );
    }

    private void Close_Click(object sender, RoutedEventArgs e) => Close( );

    private void MiniButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

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

    private void Progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        => player.Position = TimeSpan.FromSeconds(ProgressSlider.Value);
}
