using NewGlobe.RouteSolver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGlobe.RouteSolver.Services.Interfaces
{
    public interface IRoutesService
    {
        public int GetShortestRoute(string start, string end);
        public int FindDifferentRoutes(InputModel model);
    }
}
