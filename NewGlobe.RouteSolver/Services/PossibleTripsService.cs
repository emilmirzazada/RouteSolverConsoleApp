using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;

namespace NewGlobe.RouteSolver.Services
{
    public class PossibleTripsService : IPossibleTripsService
    {
        private readonly IDataAccess dataAccess;

        public PossibleTripsService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public int CountPossibleTrips(InputModel model)
        {
            int count = 0;
            CountPossibleTripsRecursively(model, 0, ref count);
            return count;
        }

        public void CountPossibleTripsRecursively(InputModel model, int stops, ref int count)
        {
            if (stops <= model.MaxStops)
            {
                if (model.StartPoint == model.EndPoint)
                {
                    if (model.ExactStops != 0)
                    {
                        if (stops == model.ExactStops)
                            count++;
                    }
                    else
                    {
                        if (stops < model.MaxStops)
                            count++;
                    }
                }
                if (dataAccess.Graph.ContainsKey(model.StartPoint))
                {
                    foreach (var neighbor in dataAccess.Graph[model.StartPoint])
                    {
                        model.StartPoint = neighbor.Key;
                        CountPossibleTripsRecursively(model, stops + 1, ref count);
                    }
                }
            }
        }
    }
}
