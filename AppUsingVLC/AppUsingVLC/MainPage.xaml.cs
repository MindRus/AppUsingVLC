using LibVLCSharp.Shared;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppUsingVLC
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = $"{DateTime.Now.ToString("HH\\:mm\\:ss").Replace(":", ".")}.ts";
            var storageFileVideo = await ApplicationData.Current.LocalFolder.CreateFileAsync(name);

            var LibVLC = new LibVLC();
            var mediaPlayer = new MediaPlayer(LibVLC);

            using (var media = new Media(LibVLC, new Uri("rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov")))
            {
                media.AddOption($":sout=#file{{dst={storageFileVideo.Path}}}");
                media.AddOption(":sout-keep");
                mediaPlayer.Play(media);

                await Task.Delay(5000);

                mediaPlayer.Stop();
            }
        }
    }
}
