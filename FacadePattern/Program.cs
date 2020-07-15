using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    class VideoFile
    {
        public VideoFile(string fullName, bool compression)
        {
            FullName = fullName;
            var parts = fullName.Split('.');
            if (parts.Length > 0)
            {
                Name = parts[0];
                Format = parts[1];
            } else
            {
                Name = fullName;
            }

            Compression = compression;
        }
        public string FullName { get; private set; }
        public string Name { get; private set; }
        public string Format { get; private set; }
        public bool Compression { get; private set; }

        public override string ToString()
        {
            return $"File name: {FullName}, compression: {Compression}";
        }
    }

    static class CheckFormat
    {
        static string[] formats = { "mp4", "mpeg4", "mov" };
        public static bool IsConvertableFormat(VideoFile video)
        {
            if (formats.Contains(video.Format))
                return true;
            else
                return false;
        }
    }

    class VideoConverter
    {
        static string[] formats = { "mp4", "mpeg4", "mov", "avi" };
        VideoFile _video;

        string name;
        string format;
        bool compression;

        public VideoConverter(VideoFile video)
        {
            name = video.Name;
            format = video.Format;
            compression = video.Compression;
        }

        public void SetName(string newName)
        {
            name = newName;
        }

        public void SetCompression(bool c)
        {
            this.compression = c;
        }
        public void SetFormat(string newFormat)
        {
            format = newFormat;
        }

        public VideoFile GetConvertableVideo()
        {
            return new VideoFile($"{name}.{format}", compression);
        }
    }

    class FacadeVideoConverter
    {
        public VideoFile Convert(VideoFile video, string newFormat)
        {
            if(CheckFormat.IsConvertableFormat(video))
            {
                VideoConverter converter = new VideoConverter(video);
                converter.SetName(video.Name + "Convertable");
                converter.SetFormat(newFormat);
                return converter.GetConvertableVideo();
            } else
            {
                Console.WriteLine("Формат неконвертируем");
                return video;
            }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            VideoFile video = new VideoFile("MyVideo.mp4", false);
            Console.WriteLine(video);

            Console.WriteLine("Конвертируем видео");

            FacadeVideoConverter converter = new FacadeVideoConverter();
            VideoFile newVideo = converter.Convert(video, "mov");

            Console.WriteLine(newVideo);

            Console.Read();
        }
    }
}
