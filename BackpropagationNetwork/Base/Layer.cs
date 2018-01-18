using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork.Base
{
    [JsonObject]
    public class Layer
    {
        [JsonProperty]
        public List<Neuron> Neurons { get; set; }
        
        [JsonIgnore]
        public Neuron Bias { get; set; }

        public Layer()
        {

        }

        public Layer(int neuronCount, bool bias = true, Func<double, double> activation = null)
        {
            var activationFunctions = new ActivationFunctions();
            if (activation == null)
                activation = activationFunctions.Sigmoid;

            Neurons = new List<Neuron>();
            for (int i = 0; i < neuronCount; i++)
                Neurons.Add(new Neuron { Activation = activation });

            foreach (var neuron in Neurons)
                neuron.Activation = activation;

            if (bias)
                Bias = new Neuron() { Input = 1, Activation = x => x };

        }

        public void ConnectToNextLayer(Layer layer)
        {
            var random = new Random(Seed: 10);
            foreach (var endNeuron in layer.Neurons)
            {
                foreach (var startNeuron in Neurons)
                {
                    var weight = new Weight()
                    {
                        Value = random.NextDouble(),
                        Start = startNeuron,
                        End = endNeuron
                    };
                    endNeuron.InputWeight.Add(weight);
                    startNeuron.OutputWeight.Add(weight);
                }
                if (Bias != null)
                {
                    var weightB = new Weight()
                    {
                        Value = random.NextDouble(),
                        Start = Bias,
                        End = endNeuron
                    };
                    endNeuron.InputWeight.Add(weightB);
                    Bias.OutputWeight.Add(weightB);
                }
            }
        }
    }
}
