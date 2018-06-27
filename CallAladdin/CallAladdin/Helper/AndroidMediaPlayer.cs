using Android.Media;
using CallAladdin.Helper.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CallAladdin.Helper
{
    //TODO: in future this can be transferred over to android project and this can be called using DependencyServices
    public class AndroidMediaPlayer : IDisposable, IMediaPlayer
    {
        private MediaPlayer player;
        private MediaRecorder recorder;

        public AndroidMediaPlayer()
        {

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
                if (player == null)
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

            }
        }

        public void Play(string pathToAudioFile)
        {
            try
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                }

                player.Reset();
                player.SetDataSource(pathToAudioFile);
                player.Prepare();
                player.Start();
            }
            catch (Exception ex)
            {

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

            }
        }
    }
}
