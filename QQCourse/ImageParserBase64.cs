using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace QQCourse
{
    public class ImageParserBase64
    {
        public ImageParserBase64() { }

        public Byte[] ParseImageToByte(BitmapSource bitmap) {
            using (MemoryStream ms = new MemoryStream())
            {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(ms);

                return ms.GetBuffer();
            }
        }

        public BitmapSource ParseByteToImage(Byte[] ImageData)
        {
            using (MemoryStream ms = new MemoryStream(ImageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }
    }
}
