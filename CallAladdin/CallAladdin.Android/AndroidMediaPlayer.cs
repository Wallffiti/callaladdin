using System;
using Android.Media;
using CallAladdin.Helper.Interfaces;
using System.IO;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Dependency(typeof(CallAladdin.Droid.AndroidMediaPlayer))]
namespace CallAladdin.Droid
{
    public class AndroidMediaPlayer : IDisposable, IMediaPlayer
    {
        private MediaPlayer player;
        private MediaRecorder recorder;

        public AndroidMediaPlayer()
        {
            Debug.WriteLine("Entering AndroidMediaPlayer constructor");
        }

        public void Dispose()
        {
            try
            {
                if (recorder != null)
                {
                    recorder.Release();
                    recorder.Dispose(); //test
                    recorder = null;    //test
                }
                if (player != null)
                {
                    player.Release();
                    player.Dispose();   //test
                    player = null;  //test
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        public void Record(string pathToAudioFile)
        {
            try
            {
                if (File.Exists(pathToAudioFile))
                {
                    File.Delete(pathToAudioFile);
                }
                if (recorder == null)
                {
                    recorder = new MediaRecorder(); // Initial state.
                }

                recorder.Reset();
                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                recorder.SetAudioEncoder(AudioEncoder.AmrNb);

                // Initialized state.
                recorder.SetOutputFile(pathToAudioFile);

                // DataSourceConfigured state.
                recorder.Prepare(); // Prepared state
                recorder.Start(); // Recording state.
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        public void Play(string pathToAudioFile, Action<object, System.EventArgs> OnPlaybackCompleted = null)
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }
                player.Completion += (s, e) =>
                {
                    OnPlaybackCompleted?.Invoke(this, e);
                };
                player.Reset();
                player.SetDataSource(pathToAudioFile);
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        public void StopRecording()
        {
            try
            {
                if (recorder != null)
                {
                    recorder.Stop();
                    recorder.Reset();

                    Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }

        public void StopPlaying()
        {
            try
            {
                if (player != null)
                {
                    player.Stop();
                    player.Reset();

                    Dispose();
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
        }
    }
}