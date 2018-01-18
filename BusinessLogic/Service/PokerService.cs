using AutoMapper;
using BusinessLogic.DTO.Poker;
using BusinessLogic.Service.Base;
using DataAccess.Repositories.Base;
using Newtonsoft.Json;
using Poker;
using Poker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class PokerService : IPokerService
    {
        IRoomRepository _roomRepository = null;
        IMapper _mapper = null;

        public PokerService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public string Serialize(Game obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public Game Deserialize(string obj)
        {
            return JsonConvert.DeserializeObject<Game>(obj);
        }

        public int AddRoom(int bet = 10, int maxPlayersCount = 2)
        {
            Game game = new Game();
            game.MaxPlayersCount = maxPlayersCount;
            game.DefaultBet = bet;
            var serGame = Serialize(game);
            var room = _roomRepository.AddRoom("Poker", serGame);
            return room.Id;
        }

        public PokerViewModel AddUserToRoom(int userId, double balance, string nickname, int roomId)
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
                    Cards = new List<Card>(),
                    IsPlaying = true
                });

                if (game.Players.Count == game.MaxPlayersCount)
                {
                    game.GameStart();
                }

                room.State = Serialize(game);
                _roomRepository.UpdateRoom(room);
            }

            var gameViewModel = _mapper.Map<PokerViewModel>(game);
            gameViewModel.GameId = roomId;
            return gameViewModel;
        }

        public PokerViewModel GetRoomState(int id)
        {
            var room = _roomRepository.GetRoomById(id);
            if (room == null)
                return null;
            var game = Deserialize(room.State);
            var gameViewModel = _mapper.Map<PokerViewModel>(game);
            gameViewModel.GameId = id;
            if (game.IsEnd)
            {
                var winners = game.GetWinners();
                gameViewModel.Winners = "Winners: " + string.Join(", ", winners.Names) + " won " + winners.Money;
            }
            return gameViewModel;
        }

        public IEnumerable<PokerRoomSearchViewModel> GetPokerRooms()
        {
            return _roomRepository.GetPokerRooms().
                     Select(room => new { game = Deserialize(room.State), id = room.Id }).
                     Where(game => game.game.Players.Count < game.game.MaxPlayersCount).
                     Select(game =>
                     {
                         var gm = _mapper.Map<PokerRoomSearchViewModel>(game.game);
                         gm.GameId = game.id;
                         return gm;
                     });
        }

        public PokerViewModel RemoveUserFromRoom(int userId, int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);

            game.Players.Remove(game.Players.FirstOrDefault(player => player.Id == userId));
            var gameViewModel = _mapper.Map<PokerViewModel>(game);

            room.State = Serialize(game);
            _roomRepository.UpdateRoom(room);

            return gameViewModel;
        }

        public void Check(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(Poker.DTO.Action.Check);
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void Call(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(Poker.DTO.Action.Call);
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void Raise(int roomId, int userId, double bet)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(Poker.DTO.Action.Raise, bet);
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void Fold(int roomId, int userId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(Poker.DTO.Action.Fold);
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void Bet(int roomId, int userId, double bet)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);
            game.PlayerTurn(Poker.DTO.Action.Bet, bet);
            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }

        public void GiveNewCards(int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            var game = Deserialize(room.State);

            game.GetNextCardsToTable();
            game.CurrentPlayer = 0;
            game.IsfinishedCircle = false;

            var state = Serialize(game);
            room.State = state;
            _roomRepository.UpdateRoom(room);
        }
    }
}
