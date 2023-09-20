using Microsoft.Extensions.DependencyInjection;
using NewGlobe.RouteSolver.Commands;
using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Services.Interfaces;
using NewGlobe.RouteSolver.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewGlobe.RouteSolver.Handlers;

namespace NewGlobe.RouteSolver.StartupHelpers
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataAccess, DataAccess>();
            services.AddSingleton<IDistanceCalculatorService, DistanceCalculatorService>();
            services.AddSingleton<IPossibleTripsService, PossibleTripsService>();
            services.AddSingleton<IRoutesService, RoutesService>();
            services.AddSingleton<IUserInputHandler, UserInputHandler>();
            services.AddSingleton<Program>();

            services.AddSingleton<CalculateDistanceCommand>();
            services.AddSingleton<CountTripsWithMaxStopsCommand>();
            services.AddSingleton<FindShortestRouteLengthCommand>();
            services.AddSingleton<FindDifferentRoutesCommand>();
        }
    }
}
