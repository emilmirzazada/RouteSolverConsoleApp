namespace NewGlobe.RouteSolver.Data
{
    public class DataAccess : IDataAccess
    {
        public Dictionary<string, Dictionary<string, int>> Graph { get; set; }

        public DataAccess()
        {
            Graph = new Dictionary<string, Dictionary<string, int>>();
        }

        public void SetGraphData(List<string> routes)
        {
            foreach (var route in routes)
            {
                string start = route.Substring(0, 1);
                string end = route.Substring(1, 1);
                int distance = int.Parse(route.Substring(2));

                if (!Graph.ContainsKey(start))
                {
                    Graph[start] = new Dictionary<string, int>();
                }

                Graph[start][end] = distance;
            }
        }
    }
}