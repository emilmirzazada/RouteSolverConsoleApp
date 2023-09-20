using MediatR;
using System;
using System.Collections.Generic;

namespace NewGlobe.RouteSolver.Entities
{
    public class InputModel : IRequest
    {
        public int ExeId { get; set; }
        public List<string> Path { get; set; } = new List<string>();
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int MaxStops { get; set; }
        public int ExactStops { get; set; }
        public int MaxDistance { get; set; }
    }
}