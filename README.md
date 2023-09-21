# Route Solver Console App

## Overview

The Route Solver is a console application designed to solve various route-related problems. It utilizes :
  MediatR for command handling,
  Microsoft.Extensions.DependencyInjection for dependency injection
  NUnit for unit testing.


## Installation

1. **Clone the Repository**

   ```bash
   git clone https://github.com/YourUsername/NewGlobe-RouteSolver.git
   cd NewGlobe-RouteSolver

Build the solution:
     
    dotnet build

To run the application, execute the following command:

    dotnet run

Follow the prompts to input the routes and solve route-related problems.

Input the data in this format: 

    AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7

If we take AB5 as an example:

    A-> start point, B-> end point, 5-> distance between them.

In the current version, the program gives output for the following problems:

    1. The distance of the route A-B-C.
    2. The distance of the route A-E-B-C-D.
    3. The distance of the route A-E-D.
    4. The number of trips starting at C and ending at C with a maximum of 3 stops.
    5. The number of trips starting at A and ending at C with exactly 4 stops.
    6. The length of the shortest route (in terms of distance to travel) from A to C.
    7. The length of the shortest route (in terms of distance to travel) from B to B.
    8. The number of different routes from C to C with a distance of less than 30.


## Features
  Calculate distances along specified routes. <br>
  Count trips with a maximum number of stops. <br>
  Find the shortest route between two points. <br>
  Identify different routes within a specified distance. <br>

## Tests
There are some unit tests implemented to ensure the correctness of the application. To run the tests, use the following command:

    dotnet test

