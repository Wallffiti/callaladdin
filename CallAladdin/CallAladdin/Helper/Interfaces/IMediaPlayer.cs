using System;

namespace CallAladdin.Helper.Interfaces
{
    public interface IMediaPlayer
    {
        void Play(string pathToAudioFile, Action<object, System.EventArgs> OnPlaybackCompleted = null);
        void Record(string pathToAudioFile);
        void StopPlaying();
        void StopRecording();
    }
}