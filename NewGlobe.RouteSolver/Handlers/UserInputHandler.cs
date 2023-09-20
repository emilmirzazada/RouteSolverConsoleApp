using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGlobe.RouteSolver.Handlers
{
    public interface IUserInputHandler
    {
        List<string> GetValidatedUserInput();
    }

    public class UserInputHandler : IUserInputHandler
    {
        public List<string> GetValidatedUserInput()
        {
            string? input;
            do
            {
                Console.WriteLine(@"Please input routes in this format: AB5, BC4, CD8
Guide: A-> start point, B-> end point, 5-> distance between them");
                input = Console.ReadLine();
            } while (string.IsNullOrEmpty(input));

            Console.WriteLine("\n");

            List<string> routes = input.Replace(" ", string.Empty).Split(",").ToList();
            foreach (var route in routes)
            {
                if (route.Length != 3)
                {
                    Console.WriteLine("\nERROR: Invalid input, each route should be 3 characters (e.g., \"AB5\")");
                    return null;
                }

                if (!int.TryParse(route.AsSpan(2), out _))
                {
                    Console.WriteLine("\nERROR: Invalid distance, couldn't parse as integer");
                    return null;
                }
            }
            return routes;
        }
    }
}
