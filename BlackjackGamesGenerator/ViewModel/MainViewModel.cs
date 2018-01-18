using BJ.DTO;
using Blackjack;
using Blackjack.DTO;
using Blackjack.Enum;
using BlackjackGamesGenerator.Tools;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGamesGenerator.ViewModel
{
    public class MainViewModel
        : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            gameResults = new List<GameResultDTO>();
            random = new Random(Seed: 20);
        }
        private Random random;
        private IEnumerable<GameResultDTO> gameResults;
        public ObservableCollection<GameResultDTO> ObservableGameResult { get; set; }

        private double gamesCount;
        public double GamesCount
        {
            get { return gamesCount; }
            set
            {
                gamesCount = value;
                OnPropertyChanged("GamesCount");
            }
        }

        private string action;
        public string Action
        {
            get { return action; }
            set
            {
                action = value;
                OnPropertyChanged("Action");
            }
        }

        private RelayCommand generate;
        public RelayCommand Generate
        {
            get
            {
                return generate ?? (generate = new RelayCommand((obj) =>
                {
                    PlayerAction playerAction;
                    gameResults = new List<GameResultDTO>();
                    if (Action == PlayerAction.Hit.ToString())
                    {
                        playerAction = PlayerAction.Hit;
                    }
                    else if (Action == PlayerAction.Stand.ToString())
                    {
                        playerAction = PlayerAction.Stand;
                    }
                    else return;

                    for (int i = 0; i < GamesCount; i++)
                    {
                        var game = new Game(random)
                        {
                            Casino = new Player() { Id = 3, Nickname = "Casino", Bet = 100 }
                        };
                        game.Players.Add(new Player { Id = 1, Nickname = "nickname", Bet = 100 });
                        game.GameStart();
                        game.CasinosTurn();
                        game.PlayerTurn(PlayerAction.FirstTurn);
                        if (game.CurrentPlayer != game.Players.Count)
                        {
                            game.PlayerTurn(playerAction);
                        }
                        var winners = game.GetWinners();
                        var isWin = winners.Names.Contains("nickname") ? 1.0 : 0.0;
                        var result = new GameResultDTO
                        {
                            Player = game.Players.FirstOrDefault().Cards.Take(2).Select(x => x.Value).Sum() * 1.0,
                            Enemies = new List<double> { game.Casino.Cards.FirstOrDefault().Value },
                            Action = playerAction == PlayerAction.Hit ? 0.0 : 1.0,
                            GameResult = isWin
                        };
                        (gameResults as List<GameResultDTO>).Add(result);

                        ObservableGameResult = new ObservableCollection<GameResultDTO>(gameResults);
                        OnPropertyChanged("ObservableGameResult");
                    }
                }));
            }

        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ?? (save = new RelayCommand((obj) =>
                {
                    var gameResultJson = JsonConvert.SerializeObject(gameResults);
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    var result = saveFileDialog.ShowDialog();

                    if (result == true)
                    {
                        File.WriteAllText(saveFileDialog.FileName, gameResultJson);
                    }
                }));
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
