namespace FawryTask.Models
{
    public interface IShippable
    {
        double Weight { get; }
        string GetName();
        double GetWeight();
    }
} 