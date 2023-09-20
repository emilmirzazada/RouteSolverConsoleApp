using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Services.Interfaces;

namespace NewGlobe.RouteSolver.Services
{
    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        private readonly IDataAccess dataAccess;

        public DistanceCalculatorService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public int CalculateDistance(List<string> path)
        {
            int distance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                string start = path[i];
                string end = path[i + 1];

                if (!dataAccess.Graph.ContainsKey(start) || !dataAccess.Graph[start].ContainsKey(end))
                {
                    return -1;
                }

                distance += dataAccess.Graph[start][end];
            }

            return distance;
        }
    }
}
