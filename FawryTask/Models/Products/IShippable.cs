namespace FawryTask.Models.Products
{
    public interface IShippable
    {
        double Weight { get; }
        string GetName();
        double GetWeight();
    }
}