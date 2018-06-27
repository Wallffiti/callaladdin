using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CallAladdin.Helper.Interfaces;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CallAladdin.iOS.IosMediaPlayer))]
namespace CallAladdin.iOS
{
    public class IosMediaPlayer : IDisposable, IMediaPlayer
    {
        public IosMediaPlayer()
        {
            Debug.WriteLine("Entering IosMediaPlayer constructor");
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Play(string pathToAudioFile, Action<object, System.EventArgs> OnPlaybackCompleted = null)
        {
            throw new NotImplementedException();
        }

        public void Record(string pathToAudioFile)
        {
            throw new NotImplementedException();
        }

        public void StopPlaying()
        {
            throw new NotImplementedException();
        }

        public void StopRecording()
        {
            throw new NotImplementedException();
        }
    }
}