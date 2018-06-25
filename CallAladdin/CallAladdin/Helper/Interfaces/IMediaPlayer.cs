namespace CallAladdin.Helper.Interfaces
{
    public interface IMediaPlayer
    {
        void Play(string pathToAudioFile);
        void Record(string pathToAudioFile);
        void StopPlaying();
        void StopRecording();
    }
}