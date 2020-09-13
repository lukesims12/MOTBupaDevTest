using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BupaDentalTestMOT
{
    public class Vehicle
    {
        [JsonProperty("registration")]
        public string Registration { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("firstUsedDate")]
        public string FirstUsedDate { get; set; }

        [JsonProperty("fuelType")]
        public string FuelType { get; set; }

        [JsonProperty("primaryColour")]
        public string PrimaryColour { get; set; }

        [JsonProperty("motTests")]
        public List<MOTTest> MotTests { get; set; }
    }
}
