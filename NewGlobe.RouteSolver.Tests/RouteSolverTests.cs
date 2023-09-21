using Microsoft.Extensions.Logging;
using Moq;
using NewGlobe.RouteSolver.Commands;
using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;
using System.Text;

namespace NewGlobe.RouteSolver.Tests
{
    public class RouteSolverTests
    {
        StringBuilder _ConsoleOutput;
        Mock<TextReader> _ConsoleInput;

        [SetUp]
        public void Setup()
        {
            _ConsoleOutput = new StringBuilder();
            var consoleOutputWriter = new StringWriter(_ConsoleOutput);
            _ConsoleInput = new Mock<TextReader>();
            Console.SetOut(consoleOutputWriter);
            Console.SetIn(_ConsoleInput.Object);
        }

        [Test]
        public async Task CalculateDistanceCommand_WithValidInput_ShouldReturnCorrectResult()
        {
            var mockDataAccess = new Mock<IDataAccess>();
            var mockLogger = new Mock<ILogger<CalculateDistanceCommand>>();
            var mockDistanceCalculatorService = new Mock<IDistanceCalculatorService>();

            mockDistanceCalculatorService.Setup(x => x.CalculateDistance(It.IsAny<List<string>>())).Returns(9);

            var handler = new CalculateDistanceCommandHandler(mockDistanceCalculatorService.Object, mockLogger.Object);
            var command = new CalculateDistanceCommand
            {
                Model = new InputModel { ExeId = 1, Path = { "A", "B", "C" } }
            };

            var expecctedResult = "1. 9";

            RefreshConsole();

            await handler.Handle(command, default);

            Assert.That(GetFirstLineConsoleOutput(), Is.EqualTo(expecctedResult));
        }

        [Test]
        public async Task CalculateDistanceCommand_WithNoPathResult()
        {
            var mockDataAccess = new Mock<IDataAccess>();
            var mockLogger = new Mock<ILogger<CalculateDistanceCommand>>();
            var mockDistanceCalculatorService = new Mock<IDistanceCalculatorService>();

            mockDistanceCalculatorService.Setup(x => x.CalculateDistance(It.IsAny<List<string>>())).Returns(-9);

            var handler = new CalculateDistanceCommandHandler(mockDistanceCalculatorService.Object, mockLogger.Object);
            var command = new CalculateDistanceCommand
            {
                Model = new InputModel { ExeId = 2, Path = { "A", "B", "C" } }
            };

            var expecctedResult = "2. NO SUCH ROUTE";

            RefreshConsole();

            await handler.Handle(command, default);

            Assert.That(GetFirstLineConsoleOutput(), Is.EqualTo(expecctedResult));
        }

        [Test]
        public async Task ProgramDisplaysPrompt()
        {
            SetupUserResponses("AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            var expectedPrompt = @"Please input routes in this format: AB5, BC4, CD8";

            var outputLines = await RunMainAndGetConsoleOutput();

            Assert.That(outputLines[0], Is.EqualTo(expectedPrompt));
        }

        [Test]
        public async Task ProgramDisplaysErrorWhenRouteIsIllegal()
        {
            SetupUserResponses("ABA, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            var expectedPrompt = "\nERROR: Invalid distance, couldn't parse as integer";

            var outputLines = await RunMainAndGetConsoleOutput();

            Assert.That(outputLines[3], Is.EqualTo(expectedPrompt));
        }

        [Test]
        public async Task ProgramDisplaysErrorWhenRouteIsLessThanThreeChars()
        {
            SetupUserResponses("AB, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7");
            var expectedPrompt = "\nERROR: Invalid input, each route should be at least 3 characters (e.g., \"AB5\")";

            var outputLines = await RunMainAndGetConsoleOutput();

            Assert.That(outputLines[3], Is.EqualTo(expectedPrompt));
        }

        public void RefreshConsole()
        {
            _ConsoleOutput = new StringBuilder();
            var consoleOutputWriter = new StringWriter(_ConsoleOutput);
            Console.SetOut(consoleOutputWriter);
        }

        public string GetFirstLineConsoleOutput()
        {
            return _ConsoleOutput.ToString().Split("\r\n")[0];
        }

        public async Task<string[]> RunMainAndGetConsoleOutput()
        {
            await Program.Main(default);
            return _ConsoleOutput.ToString().Split("\r\n");
        }

        private MockSequence SetupUserResponses(params string[] userResponses)
        {
            var sequence = new MockSequence();
            foreach (var response in userResponses)
            {
                _ConsoleInput.InSequence(sequence).Setup(x => x.ReadLine()).Returns(response);
            }
            return sequence;
        }
    }
}