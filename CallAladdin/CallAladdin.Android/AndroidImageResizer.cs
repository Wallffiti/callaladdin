using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CallAladdin.Helper.Interfaces;
using Xamarin.Forms;
using Android.Graphics;
using System.IO;
using CallAladdin.Helper;

[assembly: Dependency(typeof(CallAladdin.Droid.AndroidImageResizer))]
namespace CallAladdin.Droid
{
    public class AndroidImageResizer : IImageResizer
    {
        public byte[] ResizeImage(string sourceFile, float maxWidth, float maxHeight, float resizeFactor)
        {
            byte[] results = null;

            try
            {
                // First decode with inJustDecodeBounds=true to check dimensions
                var options = new BitmapFactory.Options()
                {
                    InJustDecodeBounds = false,
                    InPurgeable = true,
                };
                using (var image = BitmapFactory.DecodeFile(sourceFile, options))
                {
                    if (image != null)
                    {
                        var sourceSizeHeight = (int)image.GetBitmapInfo().Height;
                        var sourceSizeWidth = (int)image.GetBitmapInfo().Width;

                        var width = (int)(resizeFactor * sourceSizeWidth);
                        var height = (int)(resizeFactor * sourceSizeHeight);

                        using (var bitmapScaled = Bitmap.CreateScaledBitmap(image, height, width, true))
                        {
                            using (var outStream = new MemoryStream())
                            {
                                if (sourceFile.ToLower().EndsWith("png"))
                                    bitmapScaled.Compress(Bitmap.CompressFormat.Png, 75, outStream);
                                else
                                    bitmapScaled.Compress(Bitmap.CompressFormat.Jpeg, 65, outStream);

                                results = Utilities.StreamToBytes(outStream);
                            }

                            bitmapScaled.Recycle();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }

            return results;
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            byte[] results = null;

            try
            {
                // Load the bitmap 
                Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
                //
                float ZielHoehe = 0;
                float ZielBreite = 0;
                //
                var Hoehe = originalImage.Height;
                var Breite = originalImage.Width;
                //
                if (Hoehe > Breite) // Höhe (71 für Avatar) ist Master
                {
                    ZielHoehe = height;
                    float teiler = Hoehe / height;
                    ZielBreite = Breite / teiler;
                }
                else // Breite (61 für Avatar) ist Master
                {
                    ZielBreite = width;
                    float teiler = Breite / width;
                    ZielHoehe = Hoehe / teiler;
                }
                //
                Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)ZielBreite, (int)ZielHoehe, false);
                // 
                using (MemoryStream ms = new MemoryStream())
                {
                    resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                    results = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }

            return results;
        }


    }
}