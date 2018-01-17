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

        IRoomRepository _roomRepository = null;
        IMapper _mapper = null;

        public BlackjackService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public int AddRoom(int bet = 10, int maxPlayersCount = 2)
        {
            var game = new Game
            {
                Casino = new Player()
                {
                    Id = 3,
                    Nickname = "Casino",
                    Bet = bet,
                    Cards = new List<Card>()
                },
                MaxPlayersCount = maxPlayersCount,
                DefaultBet = bet
            };
            var serGame = Serialize(game);
            var room = _roomRepository.AddRoom("Blackjack", serGame);
            return room.Id;
        }

        public BlackjackViewModel AddUserToRoom(int userId, double balance, string nickname, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            if (game.Players.Count < game.MaxPlayersCount)
            {
                game.Players.Add(new Player
                {
                    Id = userId,
                    Nickname = nickname,
                    Bet = game.DefaultBet,
                    Balance = balance - game.DefaultBet,
                    Cards = new List<Card>()
                });

                if (game.Players.Count == game.MaxPlayersCount)
                {
                    game.GameStart();
                    game.CasinosTurn();
                    game.PlayerTurn(PlayerAction.FirstTurn);
                }

                room.State = Serialize(game);
                _roomRepository.UpdateRoom(room);
            }

            var gameViewModel = _mapper.Map<BlackjackViewModel>(game);
            gameViewModel.GameId = roomId;
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
            if (room == null)
                return null;
            var game = Deserialize(room.State);
            var gameViewModel = _mapper.Map<BlackjackViewModel>(game);
            gameViewModel.GameId = roomId;
            if (game.IsEnd)
            {
                var winners = game.GetWinners();
                gameViewModel.Winners = "Winners: " + string.Join(", ", winners.Names) + " won " + winners.Money;
            }
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

        public IEnumerable<BlackJackSearchViewModel> GetBlackJackRooms()
        {
            return _roomRepository.GetBlakJackRooms().
                     Select(room => new { game = Deserialize(room.State), id = room.Id }).
                     Where(game => game.game.PlayersCount < game.game.MaxPlayersCount).
                     Select(game =>
                     {
                         var gm = _mapper.Map<BlackJackSearchViewModel>(game.game);
                         gm.GameId = game.id;
                         return gm;
                     });
        }

        public BlackjackViewModel RemoveUserFromRoom(int userId, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);

            game.Players.Remove(game.Players.FirstOrDefault(player => player.Id == userId));
            var gameViewModel = _mapper.Map<BlackjackViewModel>(game);

            room.State = Serialize(game);
            _roomRepository.UpdateRoom(room);

            return gameViewModel;
        }
    }
}
