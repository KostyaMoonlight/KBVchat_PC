using AutoMapper;
using BusinessLogic.DTO.Poker;
using BusinessLogic.Service.Base;
using DataAccess.Repositories.Base;
using Newtonsoft.Json;
using Poker;
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
            game.Bet = bet;
            var serGame = Serialize(game);
            var room = _roomRepository.AddRoom("Poker", serGame);
            return room.Id;
        }

        public PokerViewModel AddUserToRoom(int userId, string nickname, int roomId)
        {
            throw new Exception();
            //var room = _roomRepository.GetRoomById(roomId);
            //var game = Deserialize(room.State);
            //if (game.Players.Count >= 1)
            //{
            //    var gameVM = _mapper.Map<PokerViewModel>(game);
            //    gameVM.RoomId = roomId;
            //    return gameVM;
            //}
            //game.Players.Add(new Player { Id = userId, Nickname = nickname, Bet = 100 });

            //game.GameStart();
            //game.CasinosTurn();
            //game.PlayerTurn(PlayerAction.FirstTurn);


            //var gameViewModel = _mapper.Map<BlackjackViewModel>(game);
            //gameViewModel.RoomId = roomId;

            //room.State = Serialize(game);
            //_roomRepository.UpdateRoom(room);
            //return gameViewModel;
        }

        public PokerViewModel GetRoomState(int id)
        {
            throw new NotImplementedException();
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
    }
}
