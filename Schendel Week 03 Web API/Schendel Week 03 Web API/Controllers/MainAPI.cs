using Microsoft.AspNetCore.Mvc;

namespace Schendel_Week_03_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainAPI : ControllerBase
    {
        [HttpPost(Name = "VarianceInfo")]
        public ActionResult<List<string>> Variance(List<int> parent)
        {
            VarianceInfo vi = new VarianceInfo();
            ObjectLogger logger = new ObjectLogger();
            List<string> stringList = new List<string>();
            logger.LogObject(parent);
            parent.Sort();
            List<int> child = new List<int>();
            foreach (int i in parent)
            {
                child.Add(i);
                stringList.Add(vi.getVarianceInfoString(child));
            }
            return stringList;
        }

        public class VarianceInfo
        {
            public string getVarianceInfoString(List<int> intList)
            {
                string varianceInfo;
                int count = intList.Count;
                if (count < 2)
                {
                    varianceInfo = "List is too small";
                }
                else
                {
                    double standardDeviation = getStandardDeviation(intList);
                    varianceInfo = "Elements: " + count + " Current Standard Deviation(Population): " + standardDeviation;
                }
                return varianceInfo;
            }

            public double getStandardDeviation(List<int> intList)
            {
                double mean = intList.Average();
                List<double> squaredDifferences = new List<double>();
                foreach (int i in intList)
                {
                    squaredDifferences.Add(getSquaredDifference(i, mean));
                }
                return Math.Sqrt(squaredDifferences.Average());
            }
            public double getSquaredDifference(int i, double mean)
            {
                return (i - mean) * (i - mean);
            }
        }

        public class ObjectLogger
        {
            public void LogObject(List<int> intList)
            {
                foreach (var item in intList)
                {
                    System.Diagnostics.Debug.WriteLine(item);
                }
            }
        }
    }
}