using Count.Models;

namespace Count.App.Models
{
    public class LatetsBmiInfo
    {
        public BmiUser LatestBmi { get; set; }
        public double HealthyWeight { get; set; }
        public double DistanceFromGoalWeight { get; set; }
        public string Status { get; set; }
    }
}
