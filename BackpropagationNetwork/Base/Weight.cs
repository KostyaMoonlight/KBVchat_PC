using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork.Base
{
    [JsonObject]
    public class Weight
    {
        [JsonProperty]
        public double Value { get; set; }
        [JsonIgnore]
        public double DeltaWeight { get; private set; }
        [JsonIgnore]
        public double DeltaWeightPrev { get; private set; }
        [JsonProperty]
        public Neuron Start { get; set; }
        
        [JsonIgnore]
        public Neuron End { get; set; }

        public Weight() => DeltaWeightPrev = 0;

        public double GRAD() => Start.Output * End.Delta;
        public void CalcDeltaWeight(double learningRate, double momentum)
        {
            DeltaWeight = learningRate * GRAD() + DeltaWeightPrev * momentum;
            DeltaWeightPrev = DeltaWeight;
        }
        public void UpdateWeight() => Value += DeltaWeight;

    }
}
