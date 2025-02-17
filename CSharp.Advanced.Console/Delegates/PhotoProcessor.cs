using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Delegates
{
    public static class PhotoProcessor
    {
        public delegate void PhotoHandler(Photo photo);

        public static void ProcessPhoto(string photoPath, PhotoHandler filterHandler) 
        {
            var photo = Photo.Load(photoPath);

            filterHandler(photo);

            photo.Save();
        }

    }

    public class Photo
    {
        internal static Photo Load(object path)
        {
            throw new NotImplementedException();
        }

        internal void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class PhotoFilters
    {
        public void ApplyBrightness(Photo photo)
        {
            System.Console.WriteLine("Apply brightness");
        }

        public void ApplyContrast(Photo photo)
        {
            System.Console.WriteLine("Apply contrast");
        }

        public void Resize(Photo photo)
        {
            System.Console.WriteLine("Resize photo");
        }
    }
}
