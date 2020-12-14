using FileReader;
using FluentAssertions;
using NUnit.Framework;

namespace FileReaderTests
{
    public class FileRepositoryTests
    {
        [Test]
        public void GivenMobyDickUrlWhenReadFileThenReturnsMobyDick()
        {
            var mobyDickRepository = new FileRepository("http://www.gutenberg.org/files/2701/2701-0.txt");

            var result = mobyDickRepository.ReadFile();

            result.Split(" ").Length.Should().BeGreaterThan(10000);
        }
    }
}