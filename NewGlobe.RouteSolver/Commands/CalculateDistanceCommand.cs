using MediatR;
using Microsoft.Extensions.Logging;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;

namespace NewGlobe.RouteSolver.Commands
{
    public class CalculateDistanceCommand : IRequest
    {
        public InputModel Model { get; set; }
    }

    public class CalculateDistanceCommandHandler : IRequestHandler<CalculateDistanceCommand>
    {
        private readonly IDistanceCalculatorService distanceCalculator;
        private readonly ILogger<CalculateDistanceCommand> _logger;

        public CalculateDistanceCommandHandler(IDistanceCalculatorService distanceCalculator, ILogger<CalculateDistanceCommand> logger)
        {
            this.distanceCalculator = distanceCalculator;
            _logger = logger;
        }

        public Task Handle(CalculateDistanceCommand request, CancellationToken cancellationToken)
        {
            int distance = distanceCalculator.CalculateDistance(request.Model.Path);
            if (distance > 0)
                Console.WriteLine($"{request.Model.ExeId}. {distance}");
            else
                Console.WriteLine($"{request.Model.ExeId}. NO SUCH ROUTE");
            return Task.CompletedTask;
        }
    }
}