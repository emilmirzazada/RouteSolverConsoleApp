using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NewGlobe.RouteSolver.Commands;
using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Handlers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NewGlobe.RouteSolver
{
    public class Program
    {
        private readonly IMediator _mediator;
        private readonly IDataAccess _dataAccess;
        private readonly IUserInputHandler _userInputHandler;

        public Program(IMediator mediator, IDataAccess dataAccess, IUserInputHandler userInputHandler)
        {
            _mediator = mediator;
            _dataAccess = dataAccess;
            _userInputHandler = userInputHandler;
        }

        public async Task RunAsync()
        {
            List<string> routes = _userInputHandler.GetValidatedUserInput();
            if (routes == null)
                return;

            _dataAccess.SetGraphData(routes);

            int exeId = 1;

            await ExecuteAsync(new CalculateDistanceCommand { Model = new InputModel { ExeId = exeId++, Path = { "A", "B", "C" } } });
            await ExecuteAsync(new CalculateDistanceCommand { Model = new InputModel { ExeId = exeId++, Path = { "A", "E", "B", "C", "D" } } });
            await ExecuteAsync(new CalculateDistanceCommand { Model = new InputModel { ExeId = exeId++, Path = { "A", "E", "D" } } });
            await ExecuteAsync(new CountTripsWithMaxStopsCommand { Model = new InputModel { ExeId = exeId++, StartPoint = "C", EndPoint = "C", MaxStops = 3 } });
            await ExecuteAsync(new CountTripsWithMaxStopsCommand { Model = new InputModel { ExeId = exeId++, StartPoint = "A", EndPoint = "C", MaxStops = 4, ExactStops = 4 } });
            await ExecuteAsync(new FindShortestRouteLengthCommand { Model = new InputModel { ExeId = exeId++, StartPoint = "A", EndPoint = "C" } });
            await ExecuteAsync(new FindShortestRouteLengthCommand { Model = new InputModel { ExeId = exeId++, StartPoint = "B", EndPoint = "B" } });
            await ExecuteAsync(new FindDifferentRoutesCommand { Model = new InputModel { ExeId = exeId++, StartPoint = "C", EndPoint = "C", MaxDistance = 30 } });
        }

        private async Task ExecuteAsync(IRequest command)
        {
            await _mediator.Send(command);
        }

        public static async Task Main(string[] args)
        {
            var services = Startup.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            var program = serviceProvider.GetService<Program>();

            await program.RunAsync();
        }
    }
}
