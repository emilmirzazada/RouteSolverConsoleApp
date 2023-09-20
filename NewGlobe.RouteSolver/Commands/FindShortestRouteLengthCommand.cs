using MediatR;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;
using System.Reflection;

namespace NewGlobe.RouteSolver.Commands
{
    public class FindShortestRouteLengthCommand : IRequest
    {
        public InputModel Model { get; set; }
    }

    public class FindShortestRouteLengthCommandHandler : IRequestHandler<FindShortestRouteLengthCommand>
    {
        private readonly IRoutesService routesService;

        public FindShortestRouteLengthCommandHandler(IRoutesService routesService)
        {
            this.routesService = routesService;
        }

        public Task Handle(FindShortestRouteLengthCommand request, CancellationToken cancellationToken)
        {
            int distance = routesService.GetShortestRoute(request.Model.StartPoint, request.Model.EndPoint);
            if (distance > 0)
                Console.WriteLine($"{request.Model.ExeId}. {distance}");
            else
                Console.WriteLine($"{request.Model.ExeId}. NO SUCH ROUTE");
            return Task.CompletedTask;
        }
    }
}
