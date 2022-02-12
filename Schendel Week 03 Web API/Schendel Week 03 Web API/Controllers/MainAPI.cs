using Microsoft.AspNetCore.Mvc;
using System;

namespace Schendel_Week_03_Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainAPI : ControllerBase
    {
        [HttpPost(Name = "VarianceInfo")]
        public ActionResult<List<string>> VarianceInfo(List<int> parent)
        {
            //this function is gnarly. I would MUCH prefer
            //to split this into several funtions.
            //string getVarianceInfoString(List<int> intList)
            //double getStandardDeviation(List<int> intList)
            //double getSquaredDifference(int i, double mean)
            //In fact, that is how I first implemented this, 
            //however, when I did that, I was getting :
            //SwaggerGeneratorException: Ambiguous HTTP method for action
            List<string> stringList = new List<string>();
            parent.Sort();
            List<int> child = new List<int>();
            foreach (int i in parent)
            {
                child.Add(i);
                string varianceInfo;
                int count = child.Count;
                if (count < 2)
                {
                    varianceInfo = "List is too small";
                }
                else
                {
                    double mean = child.Average();
                    List<double> squaredDifferences = new List<double>();
                    foreach (int j in child)
                    {
                        squaredDifferences.Add((j - mean) * (j - mean));
                    }
                    double standardDeviation = Math.Sqrt(squaredDifferences.Average());
                    varianceInfo = "Elements: " +
                                    count +
                                    " Current Standard Deviation(population): " +
                                    standardDeviation;
                }
                stringList.Add(varianceInfo);
            }
            return stringList;
        }
    }
}