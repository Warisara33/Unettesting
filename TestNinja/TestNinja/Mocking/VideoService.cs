using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _repository;

        public VideoService (IFileReader fileReader = null, IVideoRepository repository = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _repository = repository ?? new IVideoRepository();
        }
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var viddeoIds = new List<int>();

            var videos = _repository.GetUnprocessedVideos();
            foreach (var v in videos)
                viddeoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }
}
