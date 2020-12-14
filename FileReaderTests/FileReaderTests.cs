using System.Linq;
using FileReader;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace FileReaderTests
{
    public class Tests
    {
        private readonly string _mobyDickUrl = "http://www.gutenberg.org/files/2701/2701-0.txt";
        private readonly IFileRepository _fileRepository = Substitute.For<IFileRepository>();
        
        [Test]
        public void GivenAListOf50WordsWhenTop50ThenReturnsAllWords()
        {
            var stringToReturn = "1";
            for (int i = 2; i <= 50; i++)
            {
                stringToReturn += $" {i}";
            }
            _fileRepository.ReadFile().Returns(stringToReturn);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            for (int i = 1; i <= 50; i++)
            {
                result.Should().Contain(i.ToString());
            }
        }

        [Test]
        public void GivenAListOfTwentyWordsWhenTop50ThenReturnsAllWords()
        {
            var stringToReturn = "1";
            for (int i = 2; i <= 20; i++)
            {
                stringToReturn += $" {i}";
            }
            _fileRepository.ReadFile().Returns(stringToReturn);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            for (int i = 1; i <= 20; i++)
            {
                result.Should().Contain(i.ToString());
            }

            result.Should().NotContain("21");
        }

        [Test]
        public void GivenAListOf100WordsWhenTop50ThenReturnsOnlyFiftyWords()
        {
            var stringToReturn = "1";
            for (int i = 2; i <= 100; i++)
            {
                stringToReturn += $" {i}";
            }
            _fileRepository.ReadFile().Returns(stringToReturn);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            result.Count.Should().Be(50);
        }

        [Test]
        public void GivenAListOfTheSameWord100TimesWhenTop50ThenReturnsOnlyTheOneWord()
        {
            var stringToReturn = "word";
            for (int i = 1; i < 100; i++)
            {
                stringToReturn += " word";
            }

            _fileRepository.ReadFile().Returns(stringToReturn);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            result.Should().Contain("word");
            result.Count.Should().Be(1);
        }

        [Test]
        public void GivenAListOfWordsWhenTop50ThenReturnsWordsSortedByFrequency()
        {
            var stringToReturn = "10";
            for (int i = 1; i < 10; i++)
            {
                for (int j = 10 - i; j <= 10; j++)
                {
                    stringToReturn += " " + (10 - i);
                }
            }
            _fileRepository.ReadFile().Returns(stringToReturn);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            result[0].Should().Be("1");
            result[1].Should().Be("2");
            result[2].Should().Be("3");
            result[3].Should().Be("4");
            result[4].Should().Be("5");
            result[5].Should().Be("6");
            result[6].Should().Be("7");
            result[7].Should().Be("8");
            result[8].Should().Be("9");
            result[9].Should().Be("10");
        }

        [Test]
        public void GivenALotOfWhiteSpaceWhenTop50ThenDoesNotReturnWhiteSpace()
        {
            _fileRepository.ReadFile().Returns("\n\t    \n  \t   Hello      batman");
            var sut = new FileReaderService(_fileRepository);

            var result = sut.Top50().ToList();

            result.Count.Should().Be(2);
            result.Should().Contain("Hello");
            result.Should().Contain("batman");
        }

        [Test]
        public void GivenAllOfMobyDickWhenTop50ThenReturnsTheTop50Words()
        {
            var sut = new FileReaderService(new FileRepository("http://www.gutenberg.org/files/2701/2701-0.txt"));

            var result = sut.Top50().ToList();
            
            result.Count.Should().Be(50);
        }

        [Test]
        public void GivenUrlWhenReadFileThenReturnsFileContents()
        {
            var allOfMobyDick = "All of Moby Dick";
            _fileRepository.ReadFile().Returns(allOfMobyDick);
            var sut = new FileReaderService(_fileRepository);

            var result = sut.ReadFile();

            result.Should().Be(allOfMobyDick);
        }

    }
}