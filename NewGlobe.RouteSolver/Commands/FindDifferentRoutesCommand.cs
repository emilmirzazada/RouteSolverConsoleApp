using MediatR;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;
using System.Reflection;

namespace NewGlobe.RouteSolver.Commands
{
    public class FindDifferentRoutesCommand : IRequest
    {
        public InputModel Model { get; set; }
    }

    public class FindDifferentRoutesCommandHandler : IRequestHandler<FindDifferentRoutesCommand>
    {
        private readonly IRoutesService routesService;

        public FindDifferentRoutesCommandHandler(IRoutesService routesService)
        {
            this.routesService = routesService;
        }

        public Task Handle(FindDifferentRoutesCommand request, CancellationToken cancellationToken)
        {
            int distance = routesService.FindDifferentRoutes(request.Model);
            if (distance > 0)
                Console.WriteLine($"{request.Model.ExeId}. {distance}");
            else
                Console.WriteLine($"{request.Model.ExeId}. NO SUCH ROUTE");
            return Task.CompletedTask;
        }
    }
}
