using System.Linq;
using System.Collections.Generic;

using Xunit;
using FluentAssertions;

using TestTask.Logic.Services;
using TestTask.DataObjects;

namespace UnitTests.Services
{
    public class LineService_UnitTests
    {
        public LineService_UnitTests()
        {

        }

        [Fact]
        public void GetSplittedLines_When_LinesIsEmpty_Then_ShouldReturnEmptyCollection()
        {
            //Arrange
            const int expectedCount = 0;

            var lines = new List<string>();

            var target = new LineService();

            //Act
            var result = target.GetSplittedLines(lines);

            //Assert
            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void GetSplittedLines_When_LinesIsNotEmpty_Then_ShouldReturnCollectionWithTheSameCount()
        {
            //Arrange
            const int expectedCount = 3;

            var lines = new List<string>
            {
                "100500,100501,100502",
                "qwerty",
                "100503"
            };
            
            var target = new LineService();

            //Act
            var result = target.GetSplittedLines(lines);

            //Assert
            result.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void CheckForIncorrectLines_When_AllLinesWithCorrectElements_Then_ShouldSetAllLinesAsCorrect()
        {
            //Arrange
            var lines = new List<SeparatedLine>
            {
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100500",
                        "100501",
                        "100502"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100.503",
                        "100.504"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100505"
                    }
                }
            };

            var target = new LineService();

            //Act
            var result = target.CheckForIncorrectLines(lines);

            //Assert
            result.Select(r => r.IsCorrect).Should().AllBeEquivalentTo(true);
        }

        [Fact]
        public void CheckForIncorrectLines_When_NotAllLinesWithCorrectElements_Then_ShouldSetSomeLinesAsCorrect()
        {
            //Arrange
            var lines = new List<SeparatedLine>
            {
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100500",
                        "100501",
                        "100502"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100.503",
                        "100...504"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty"
                    }
                }
            };

            var target = new LineService();

            //Act
            var result = target.CheckForIncorrectLines(lines);

            //Assert
            result.Select(r => r.IsCorrect).Should().Contain(true);
            result.Select(r => r.IsCorrect).Should().Contain(false);
        }

        [Fact]
        public void CheckForIncorrectLines_When_AllLinesWithIncorrectElements_Then_ShouldNotSetSomeLinesAsCorrect()
        {
            //Arrange
            var lines = new List<SeparatedLine>
            {
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty",
                        "100501",
                        "100502"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100.503",
                        "100...504"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty"
                    }
                }
            };

            var target = new LineService();

            //Act
            var result = target.CheckForIncorrectLines(lines);

            //Assert
            result.Select(r => r.IsCorrect).Should().AllBeEquivalentTo(false);
        }

        [Fact]
        public void GetLineNumberWithMaxElementSum_When_AllLinesAreIncorrect_Then_ShouldReturnMinusOne()
        {
            //Arrange
            const int expected = -1;
            var lines = new List<SeparatedLine>
            {
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty",
                        "100501",
                        "100502"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100.503",
                        "100...504"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty"
                    }
                }
            };

            var target = new LineService();

            //Act
            var result = target.GetLineNumberWithMaxElementSum(lines);

            //Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void GetLineNumberWithMaxElementSum_When_NotAllLinesAreIncorrect_Then_ShouldReturnLineIndexInCollection()
        {
            //Arrange
            const int expected = 0;
            var lines = new List<SeparatedLine>
            {
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100500",
                        "100501",
                        "100502"
                    },
                    IsCorrect = true
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "100.503",
                        "100.504"
                    },
                    IsCorrect = true
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "qwerty"
                    }
                },
                new SeparatedLine
                {
                    Elements = new List<string>
                    {
                        "1"
                    },
                    IsCorrect = true
                }
            };

            var target = new LineService();

            //Act
            var result = target.GetLineNumberWithMaxElementSum(lines);

            //Assert
            result.Should().Be(expected);
        }
    }
}