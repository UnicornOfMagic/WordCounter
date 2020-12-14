using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FileReader
{
    public class FileReaderService
    {
        private readonly IFileRepository _repository;

        public FileReaderService(IFileRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<string> Top50()
        {
            var contents = _repository.ReadFile();
            contents = contents.Replace('\n', ' ');
            contents = contents.Replace('\t', ' ');

            var allWords = contents.Split(' ');
            var answerKey = new Dictionary<string, int>();

            foreach (var word in allWords)
            {
                if (string.IsNullOrWhiteSpace(word))
                    continue;

                if (answerKey.ContainsKey(word))
                    answerKey[word]++;
                else
                    answerKey.Add(word, 1);
            }

            var answerList = answerKey.ToList();
            answerList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            var sortedWords = new List<string>();

            foreach (var answer in answerList)
            {
                sortedWords.Add(answer.Key);
            }
            
            return sortedWords.Take(50);
        }

        public string ReadFile()
        {
            return _repository.ReadFile();
        }
    }
}