using MediatR;
using Microsoft.Extensions.Logging;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;
using System.Reflection;

namespace NewGlobe.RouteSolver.Commands
{
    public class CountTripsWithMaxStopsCommand : IRequest
    {
        public InputModel Model { get; set; }
    }

    public class CountTripsWithMaxStopsCommandHandler : IRequestHandler<CountTripsWithMaxStopsCommand>
    {
        private readonly IPossibleTripsService possibleTripsService;
        private readonly ILogger<CalculateDistanceCommand> _logger;

        public CountTripsWithMaxStopsCommandHandler(IPossibleTripsService possibleTripsService, ILogger<CalculateDistanceCommand> logger)
        {
            this.possibleTripsService = possibleTripsService;
            _logger = logger;
        }

        public Task Handle(CountTripsWithMaxStopsCommand request, CancellationToken cancellationToken)
        {
            int count = possibleTripsService.CountPossibleTrips(request.Model);
            Console.WriteLine($"{request.Model.ExeId}. {count}");
            return Task.CompletedTask;
        }
    }
}