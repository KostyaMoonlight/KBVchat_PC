using BackpropagationNetwork.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackpropagationNetwork
{
    [JsonObject]
    public class BackpropagationNetwork
    {
        private CalculatingError calculatingError;
        [JsonProperty]
        public IEnumerable<Layer> Layers { get; set; }
        public Layer InputLayer { get => Layers.FirstOrDefault(); }
        public Layer OutputLayer { get => Layers.LastOrDefault(); }
        public IEnumerable<Layer> HiddenLayers { get => Layers.Take(LayersCount - 1).Skip(1); }
        public int LayersCount { get => Layers.Count(); }
        [JsonIgnore]
        public double LearningRate { get; set; } = 0.3;
        [JsonIgnore]
        public double Momentum { get; set; } = 0.3;

        public BackpropagationNetwork() => calculatingError = new CalculatingError();

        public void ConnectLayers()
        {
            var layers = Layers.ToArray();
            for (int i = 0; i < LayersCount - 1; i++)
            {
                layers[i].ConnectToNextLayer(layers[i + 1]);
            }

        }

        public double CalculateNeuron(Neuron neuron)
        {
            var sum = 0.0;
            foreach (var weight in neuron.InputWeight)
                sum += weight.Value * weight.Start.Output;
            sum += neuron.Bias?.Value ?? 0;

            neuron.Input = sum;
            return neuron.Output;
        }

        public void CalculateOutput(double[] inputs = null)
        {
            if (inputs != null)
            {
                for (int i = 0; i < InputLayer.Neurons.Count; i++)
                {
                    InputLayer.Neurons[i].Input = inputs[i];
                }
            }
            var layers = Layers.ToArray();
            for (int i = 1; i < LayersCount; i++)
            {
                foreach (var neuron in layers[i].Neurons)
                {
                    CalculateNeuron(neuron);
                }
            }
        }

        public double CalculateError(IEnumerable<double> expected)
        {
            var real = OutputLayer.Neurons.Select(x => x.Output).ToArray();
            var error = calculatingError.RootMSE(real, expected.ToArray());
            return error;
        }

        public void Train(string[] inputs, string[] expected, int epochs, Action<string> writer)
        {
            for (int epoch = 0; epoch < epochs; epoch++)
            {
                var epochError = new List<double>();
                var sets = inputs.Count();
                for (int set = 0; set < sets; set++)
                {
                    var inputS = inputs[set].Split(' ');
                    var input = inputS.Select(x => double.Parse(x)).ToArray();
                    var expectedResult = expected[set].Split(' ').Select(x => double.Parse(x)).ToArray();
                    if (InputLayer.Neurons.Count != input.Count())
                        throw new Exception("Wrong input set!");
                    for (int i = 0; i < InputLayer.Neurons.Count; i++)
                    {
                        InputLayer.Neurons[i].Input = input[i];
                    }
                    CalculateOutput();
                    var result = OutputLayer.Neurons.Select(x => x.Output).ToArray();
                    var error = calculatingError.RootMSE(result, expectedResult);
                    epochError.Add(error);
                    WeightsCalibration(expectedResult);
                }
                var midError = epochError.Sum() / epochError.Count;
                if ((epoch + 1) % 100 == 0)
                {
                    writer?.Invoke($"Epoch {epoch + 1} ; error = {midError}%");
                }


                if (midError < 0.05)
                {
                    writer?.Invoke($"Epoch {epoch + 1} ; error = {midError}%");
                    return;
                }
            }
        }

        public void WeightsCalibration(double[] expected)
        {
            //Output neuron
            for (int i = 0; i < OutputLayer.Neurons.Count(); i++)
            {
                CalculateDeltaOutputNeuronSigmoid(OutputLayer.Neurons[i], expected[i]);
            }
            foreach (var neuron in OutputLayer.Neurons)
            {
                foreach (var weight in neuron.InputWeight)
                {
                    weight.CalcDeltaWeight(LearningRate, Momentum);
                    weight.UpdateWeight();
                }
            }

            //Hidden neuron
            var reverseHiddenLayers = HiddenLayers.Reverse();
            foreach (var layer in reverseHiddenLayers)
            {


                for (int i = 0; i < layer.Neurons.Count(); i++)
                {
                    CalculateDeltaHiddenNeuronSigmoid(layer.Neurons[i]);
                }
                foreach (var neuron in layer.Neurons)
                {
                    foreach (var weight in neuron.InputWeight)
                    {
                        weight.CalcDeltaWeight(LearningRate, Momentum);
                        weight.UpdateWeight();
                    }
                }
            }


        }

        public void CalculateDeltaOutputNeuronSigmoid(Neuron neuron, double expected)
        {
            neuron.Delta = (expected - neuron.Output) * DerivativeSigmoid(neuron);
        }

        public void CalculateDeltaHiddenNeuronSigmoid(Neuron neuron)
        {
            var sum = 0.0;
            foreach (var weight in neuron.OutputWeight)
            {
                sum += weight.Value * weight.End.Delta;
            }
            neuron.Delta = DerivativeSigmoid(neuron) * sum;
        }

        public double DerivativeSigmoid(Neuron neuron)
        {
            return (1 - neuron.Output) * neuron.Output;
        }
    }
}
