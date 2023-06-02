using Count.Models;

namespace Count.App.Models
{
    public class HealthCheckModel
    {
        public List<BmiUser> UserBmis { get; set; }
        public LatetsBmiInfo LatestBmiInfo { get; set; }
    }
}
