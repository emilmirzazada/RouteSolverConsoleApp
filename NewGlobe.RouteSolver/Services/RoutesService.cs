using NewGlobe.RouteSolver.Data;
using NewGlobe.RouteSolver.Entities;
using NewGlobe.RouteSolver.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGlobe.RouteSolver.Services
{
    public class RoutesService : IRoutesService
    {
        private readonly IDataAccess dataAccess;
        private const string TempStartNode = "~";

        public RoutesService(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public string AddTempStartWhenStartAndEndAreSame(string start, string end)
        {
            var tempRoutes = new List<string>();
            foreach (var item in dataAccess.Graph.Keys)
            {
                if (item == start)
                {
                    foreach (var neighbour in dataAccess.Graph[item].Keys)
                    {
                        tempRoutes.Add($"{TempStartNode}{neighbour}{dataAccess.Graph[item][neighbour]}");
                    }
                }
            }
            start = $"{TempStartNode}";
            dataAccess.SetGraphData(tempRoutes);
            return start;
        }

        public int GetShortestRoute(string start, string end)
        {
            if (start == end)
                start = AddTempStartWhenStartAndEndAreSame(start, end);

            Dictionary<string, int> distances = new();
            HashSet<string> visited = new();

            foreach (var vertex in dataAccess.Graph.Keys)
            {
                distances[vertex] = int.MaxValue;
            }

            distances[start] = 0;

            while (visited.Count < dataAccess.Graph.Count)
            {
                string? current = null;
                int minDistance = int.MaxValue;

                foreach (var vertex in distances.Keys)
                {
                    if (!visited.Contains(vertex) && distances[vertex] < minDistance)
                    {
                        minDistance = distances[vertex];
                        current = vertex;
                    }
                }

                if (current == null)
                    break;

                visited.Add(current);

                if (dataAccess.Graph.ContainsKey(current))
                {
                    foreach (var neighbor in dataAccess.Graph[current].Keys)
                    {
                        int altDistance = distances[current] + dataAccess.Graph[current][neighbor];
                        if (altDistance < distances[neighbor])
                        {
                            distances[neighbor] = altDistance;
                        }
                    }
                }
            }
            dataAccess.Graph.Remove(TempStartNode);
            return distances[end] == int.MaxValue ? -1 : distances[end];
        }

        public int FindDifferentRoutes(InputModel model)
        {
            return FindDifferentRoutesRecursively(model.StartPoint, model.EndPoint, model.MaxDistance, 0, 0);
        }

        public int FindDifferentRoutesRecursively(string current, string end, int maxDistance, int distance, int trips)
        {
            if (current == end && distance < maxDistance && distance != 0)
                trips++;

            if (distance >= maxDistance)
                return trips;

            if (!dataAccess.Graph.ContainsKey(current))
                return trips;

            foreach (var neighbor in dataAccess.Graph[current])
            {
                int newDistance = distance + neighbor.Value;
                trips = FindDifferentRoutesRecursively(neighbor.Key, end, maxDistance, newDistance, trips);
            }

            return trips;
        }
    }
}
