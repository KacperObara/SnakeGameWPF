using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace SnakeGameWPF
{
    public class BackgroundManager
    {
        private class BitmapURL
        {
            public string url;
            public BitmapImage bitmap;

            public BitmapURL(string url)
            {
                this.url = url;
            }
        }

        private List<BitmapURL> bitmaps = new List<BitmapURL>();

        private Random random = new Random();

        public BackgroundManager()
        {
            bitmaps.Add(new BitmapURL("https://cdn.pixabay.com/photo/2014/11/23/21/22/green-tree-python-543243_960_720.jpg"));
            bitmaps.Add(new BitmapURL("https://cdn.pixabay.com/photo/2019/02/06/17/09/snake-3979601_960_720.jpg"));
            bitmaps.Add(new BitmapURL("https://cdn.pixabay.com/photo/2018/04/06/11/49/snake-3295605_960_720.jpg"));
            bitmaps.Add(new BitmapURL("https://cdn.pixabay.com/photo/2016/03/28/10/07/snake-1285354_960_720.jpg"));
            bitmaps.Add(new BitmapURL("https://cdn.pixabay.com/photo/2019/04/04/04/39/snake-4101998_960_720.jpg"));
        }

        public BitmapImage ChooseRandomBitmap()
        {
            int index = random.Next(0, bitmaps.Count);

            // The bitmap is downloaded only once when it's needed
            if (bitmaps[index].bitmap == null)
            {
                bitmaps[index].bitmap = DownloadImage(bitmaps[index].url);
            }

            return bitmaps[index].bitmap;
        }

        private BitmapImage DownloadImage(string url)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url, UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }
    }
}
