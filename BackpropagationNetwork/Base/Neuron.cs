using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork.Base
{
    [JsonObject]
    public class Neuron
    {

        [JsonIgnore]
        public double Input { get; set; }
        public double Output { get => Activation(Input); }
        [JsonIgnore]
        public double Delta { get; set; }
        [JsonProperty]
        public Func<double, double> Activation { get; set; }

        [JsonProperty]
        public List<Weight> InputWeight { get; set; }

        [JsonIgnore]
        public List<Weight> OutputWeight { get; set; }

        [JsonIgnore]
        public Weight Bias { get; set; }

        public Neuron()
        {
            InputWeight = new List<Weight>();
            OutputWeight = new List<Weight>();
        }

    }
}
