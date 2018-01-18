using AutoMapper;
using Blackjack;
using Blackjack.DTO;
using Blackjack.Enum;
using BusinessLogic.DTO.BJ;
using BusinessLogic.Service.Base;
using DataAccess.Repositories.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class BlackjackService
        : IBlackjackService
    {
        INNRepository _nnRepository = null;
        IRoomRepository _roomRepository = null;
        IMapper _mapper = null;

        public BlackjackService(INNRepository nnRepository, IRoomRepository roomRepository, IMapper mapper)
        {
            _nnRepository = nnRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public int AddRoom(int bet = 100)
        {
            var game = new Game
            {
                Casino = new Player() { Id = 3, Nickname = "Casino", Bet = bet }
            };
            var serGame = Serialize(game);
            var room = _roomRepository.AddRoom("Blackjack", serGame);
            return room.Id;
        }

        public BlackjackViewModel AddUserToRoom(int userId, string nickname, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            if (game.Players.Count >= 1)
            {
                var gameVM = _mapper.Map<BlackjackViewModel>(game);
                gameVM.RoomId = roomId;
                return gameVM;
            }
            game.Players.Add(new Player { Id = userId, Nickname = nickname, Bet = 100 });

            game.GameStart();
            game.CasinosTurn();
            game.PlayerTurn(PlayerAction.FirstTurn);


            var gameViewModel = _mapper.Map<BlackjackViewModel>(game);
            gameViewModel.RoomId = roomId;

            room.State = Serialize(game);
            _roomRepository.UpdateRoom(room);
            return gameViewModel;
        }

        public void Double(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(PlayerAction.Double);
            game.CasinosTurn();
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);

        }

        public BlackjackViewModel GetRoomState(int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            var gameViewModel = _mapper.Map<BlackjackViewModel>(game);
            gameViewModel.RoomId = roomId;
            if (game.IsEnd)
                gameViewModel.Winners = "Winners: " + string.Join(", ", game.GetWinners().Names) + " won " + game.GetWinners().Money;
            return gameViewModel;
        }

        public void Hit(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(PlayerAction.Hit);
            if (game.IsEnd)
            {
                game.CasinosTurn();
            }
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void Stand(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(PlayerAction.Stand);
            game.CasinosTurn();
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public string Serialize(Game obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public Game Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<Game>(obj);
        }

        public IEnumerable<string> GetHintFromNN(double casino, double player)
        {
            var nnName = "BPNNBJ1To1";
            var nnJson = _nnRepository.GetNN(nnName).JsonNN;
            var nn = JsonConvert.DeserializeObject<BackpropagationNetwork.BackpropagationNetwork>(nnJson);

            nn.CalculateOutput(new double[] { 0.0,
                double.Parse("0." + player.ToString()),
                double.Parse("0." + casino.ToString())
                });
            var resultH = $"Hit: {nn.OutputLayer.Neurons.FirstOrDefault().Output}%";
            nn.CalculateOutput(new double[] { 1.0,
                double.Parse("0." + player.ToString()),
                double.Parse("0." + casino.ToString())
                });
            var resultS = $"Stand: {nn.OutputLayer.Neurons.FirstOrDefault().Output}%";

            var results = new string[] { resultH, resultS };

            return results;
        }
    }
}
