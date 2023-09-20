namespace NewGlobe.RouteSolver.Data
{
    public interface IDataAccess
    {
        public abstract Dictionary<string, Dictionary<string, int>> Graph { get; set; }
        public void SetGraphData(List<string> routes);
    }
}