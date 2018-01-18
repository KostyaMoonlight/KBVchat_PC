using BackpropagationNetwork.Base;
using BackpropagationNetworkTrainer.Tools;
using BJ.DTO;
using DataAccess.Context;
using DataAccess.Repositories;
using DataAccess.Repositories.Base;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Utility;

namespace BackpropagationNetworkTrainer.ViewModel
{
    public class MainViewModel
        : INotifyPropertyChanged
    {
        BackpropagationNetwork.BackpropagationNetwork backpropagationNetwork = null;
        private IEnumerable<GameResultDTO> gameResults = null;
        int hiddenLayersCount;
        int neuronCount;
        double learningRate;
        double momentum;
        int epochCount;
        int inputCount;
        int outputCount;
        string errorStatistic;

        public IEnumerable<string> Input
        {
            get => gameResults
                .Select(x => "0." + x.Action.ToString() + " 0."
                    + x.Player.ToString() + " 0."
                    + x.Casino.ToString());
        }
        public IEnumerable<string> Output
        {
            get => gameResults
                .Select(x => x.GameResult.ToString());
        }

        public int HiddenLayersCount
        {
            get => hiddenLayersCount;
            set
            {
                hiddenLayersCount = value;
                OnPropertyChanged("HiddenLayersCount");
            }
        }
        public int NeuronCount
        {
            get => neuronCount; set
            {
                neuronCount = value;
                OnPropertyChanged("");
            }
        }
        public double LearningRate
        {
            get => learningRate; set
            {
                learningRate = value;
                OnPropertyChanged("");
            }
        }
        public double Momentum
        {
            get => momentum; set
            {
                momentum = value;
                OnPropertyChanged("");
            }
        }
        public int EpochCount
        {
            get => epochCount; set
            {
                epochCount = value;
                OnPropertyChanged("");
            }
        }
        public int InputCount
        {
            get => inputCount; set
            {
                inputCount = value;
                OnPropertyChanged("");
            }
        }
        public int OutputCount
        {
            get => outputCount; set
            {
                outputCount = value;
                OnPropertyChanged("");
            }
        }
        public string ErrorStatistic
        {
            get { return errorStatistic; }
            set
            {
                errorStatistic = value;
                OnPropertyChanged("ErrorStatistic");
            }
        }



        public MainViewModel()
        {
            HiddenLayersCount = 2;
            NeuronCount = 12;
            LearningRate = 0.05;
            Momentum = 0.55;
            EpochCount = 100;
            InputCount = 3;
            OutputCount = 1;
        }

        private RelayCommand addToDb;
        public RelayCommand AddToDb
        {
            get
            {

                return addToDb ?? (addToDb = new RelayCommand((obj) =>
                {
                    //Task.Factory.StartNew(() =>
                    //{

                    var nnName = "BPNNBJ1To1";
                    INNRepository repository = new NNRepository(new KVBchatDbContext());
                    repository.RemoveNN(nnName);
                    string nn = "";
                    nn = JsonConvert.SerializeObject(backpropagationNetwork/*, Formatting.None, settings*/);
                    ErrorStatistic += "Json before send: " + nn.Count();
                    repository.AddNN(nnName, nn);
                    var nnf = repository.GetNN(nnName)?.JsonNN;
                    ErrorStatistic += "Json get: " + nnf.Count();

                    //});
                }));
            }
        }

        private RelayCommand openSet;
        public RelayCommand OpenSet
        {
            get
            {
                return openSet ?? (openSet = new RelayCommand((obj) =>
                {
                    var openFileDialog = new OpenFileDialog();
                    var fileData = "";
                    if (openFileDialog.ShowDialog() == true)
                        fileData = File.ReadAllText(openFileDialog.FileName);
                    gameResults = JsonConvert.DeserializeObject<IEnumerable<GameResultDTO>>(fileData);
                }));
            }
        }

        private RelayCommand addSet;
        public RelayCommand AddSet
        {
            get
            {
                return addSet ?? (addSet = new RelayCommand((obj) =>
                {
                    var openFileDialog = new OpenFileDialog();
                    var fileData = "";
                    if (openFileDialog.ShowDialog() == true)
                        fileData = File.ReadAllText(openFileDialog.FileName);
                    if (gameResults == null)
                    {
                        gameResults = new List<GameResultDTO>();
                    }
                    (gameResults as List<GameResultDTO>)
                    .AddRange(JsonConvert
                        .DeserializeObject<IEnumerable<GameResultDTO>>(fileData)
                        );
                }));
            }
        }

        private RelayCommand train;
        public RelayCommand Train
        {
            get
            {
                return train ?? (train = new RelayCommand((obj) =>
                {
                    ErrorStatistic = "";
                    Shuffle();
                    var activationFunctions = new ActivationFunctions();
                    var inputLayer = new Layer(InputCount, true, activationFunctions.Line);
                    var outputLayer = new Layer(OutputCount, false, activationFunctions.Sigmoid);
                    var hiddenLayers = new List<Layer>();
                    for (int i = 0; i < HiddenLayersCount; i++)
                    {
                        hiddenLayers.Add(new Layer(NeuronCount, true, activationFunctions.Sigmoid));
                    }
                    var layers = new List<Layer>();
                    layers.Add(inputLayer);
                    layers.AddRange(hiddenLayers);
                    layers.Add(outputLayer);

                    backpropagationNetwork = new BackpropagationNetwork.BackpropagationNetwork
                    {
                        Layers = layers,
                        Momentum = Momentum,
                        LearningRate = LearningRate
                    };
                    backpropagationNetwork.ConnectLayers();
                    var task = Task.Factory.StartNew(() =>
                    {
                        backpropagationNetwork.Train(Input.ToArray(), Output.ToArray(), EpochCount, Writer);
                        System.Windows.MessageBox.Show("End");
                        //Sound(1);
                    });
                    //task.Wait();

                }));
            }
        }




        public void Writer(string text)
        {
            ErrorStatistic = text + Environment.NewLine + ErrorStatistic;
        }

        void Sound(int count)
        {
            for (int i = 0; i < count; i++)
            {
                SoundPlayer player = new SoundPlayer("D:\\beep.wav");
                player.LoadCompleted += delegate (object sender, AsyncCompletedEventArgs e)
                {
                    player.Play();
                };
                player.LoadAsync();
            }

        }

        void Shuffle()
        {
            gameResults = gameResults.Shuffle(20);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
