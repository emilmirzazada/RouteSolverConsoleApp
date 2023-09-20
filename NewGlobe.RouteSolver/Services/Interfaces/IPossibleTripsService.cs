using NewGlobe.RouteSolver.Entities;

namespace NewGlobe.RouteSolver.Services.Interfaces
{
    public interface IPossibleTripsService
    {
        public int CountPossibleTrips(InputModel model);
    }
}
