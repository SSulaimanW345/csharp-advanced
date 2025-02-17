using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.Advanced.Console.Events
{
    internal class VideoEncoder
    {
        // 1 - DEFINE DELegate for the event
        // 2 - Define an event based on that deglate
        // 3- raise an event

        public delegate void VideoEncoderEvent(object source,VideoEventArgs args );
        public event VideoEncoderEvent VideoEncoded;

        public event EventHandler<VideoEventArgs> VideoEncodingEvent;

        public void Encode(Video video) 
        {
            System.Console.WriteLine("Encoding video");
            Thread.Sleep(1000);
            VideoEncodingEvent?.Invoke(this, new VideoEventArgs { Video = video });
            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video) 
        {
            if (VideoEncoded != null) VideoEncoded(this, new VideoEventArgs { Video= video});
        }
    }

    public class Video
    {
        public string Title { get; set; }
    }

    public class VideoEventArgs : EventArgs 
    {
        public Video? Video { get;  set; }
    }

    public class MailService 
    {
        public void OnVideoEncoded(object? source, VideoEventArgs e) 
        {
            System.Console.WriteLine("Mail Service sending email " + e?.Video?.Title);
        }
    
    }
    public class MessageService 
    {
        public void OnVideoEncoded(object source, VideoEventArgs e)
        {
            System.Console.WriteLine("Message Service sending email " + e.Video.Title);
        }
    }

    public class Program 
    {
        static void Main(string[] args) 
        {
            var video = new Video();
            var videoEncoder = new VideoEncoder();
            var mailService = new MailService();
            var mesgService = new MessageService(); 
            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += mesgService.OnVideoEncoded;
            videoEncoder.VideoEncodingEvent += mailService.OnVideoEncoded;
            videoEncoder.VideoEncodingEvent += mesgService.OnVideoEncoded;
            videoEncoder.Encode(video);
        }
    }

    // events -> list of pointer to method
}
