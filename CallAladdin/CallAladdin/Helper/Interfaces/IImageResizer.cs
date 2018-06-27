using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.Helper.Interfaces
{
    public interface IImageResizer
    {
        byte[] ResizeImage(string sourceFile, float maxWidth, float maxHeight, float resizeFactor);
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
