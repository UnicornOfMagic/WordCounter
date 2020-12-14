using System;
using System.Net;

namespace FileReader
{
    public class FileRepository : IFileRepository
    {
        private readonly string _url;

        public FileRepository(string Url)
        {
            _url = Url;
        }

        public string ReadFile()
        {
            using (var webClient = new WebClient())
            {
                var contents = webClient.DownloadString(new Uri(_url));
                return contents;
            }
        }
    }
}