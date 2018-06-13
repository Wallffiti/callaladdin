using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.EventArgs
{
    public class ProfilePhotoChangedEventArgs : System.EventArgs
    {
        public string FilePath { get; private set; }

        public ProfilePhotoChangedEventArgs(string filePath)
        {
            FilePath = filePath;
        }
    }
}
