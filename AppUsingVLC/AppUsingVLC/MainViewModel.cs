using LibVLCSharp.Platforms.UWP;
using LibVLCSharp.Shared;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace AppUsingVLC
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel
    {
        public MediaPlayer MediaPlayer { get; set; }

        public ICommand InitializedCommand { get; }

        public MainViewModel()
        {
            InitializedCommand = new RelayCommand<InitializedEventArgs>(Initialize);
        }

        private void Initialize(InitializedEventArgs eventArgs)
        {
            List<string> args = new List<string>(eventArgs.SwapChainOptions)
            {
                "--no-osd",
                "--no-snapshot-preview"
            };

            var LibVLC = new LibVLC(args.ToArray());
            var MediaPlayer = new MediaPlayer(LibVLC);
            using (var media = new Media(LibVLC, new Uri("rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov")))
            {
                MediaPlayer.Play(media);
            }
        }
    }
}
