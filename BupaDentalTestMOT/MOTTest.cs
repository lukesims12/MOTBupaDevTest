using Newtonsoft.Json;

namespace BupaDentalTestMOT
{
    public class MOTTest
    {

        [JsonProperty("completedDate")]
        public string CompletedDate { get; set; }

        [JsonProperty("testResult")]
        public string TestResult { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate { get; set; }

        [JsonProperty("odometerValue")]
        public string OdometerValue { get; set; }

        [JsonProperty("odometerUnit")]
        public string OdometerUnit { get; set; }

        [JsonProperty("motTestNumber")]
        public string MotTestNumber { get; set; }
    }
}
