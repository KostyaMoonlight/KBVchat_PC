using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class RoomRepository
        : IRoomRepository
    {
        string tableName;
        DataSet _dataSet;
        DataTable _table;
        SqlDataAdapter _adapter;
        SqlConnection _connection;

        public RoomRepository()
        {
            tableName = "Rooms";
            _connection = new SqlConnection(@"Server=.\SQLExpress;Initial Catalog=KVBchatDB;Integrated Security=true;");
            _adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", _connection);
            _dataSet = new DataSet();
            _adapter.Fill(_dataSet);
            _dataSet.Tables[0].TableName = tableName;
            _table = _dataSet.Tables[0];
        }

        public Room AddRoom(string name, string state)
        {
            var row = _table.NewRow();
            row["Name"] = name;
            row["State"] = state;
            _table.Rows.Add(row);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_adapter);
            _adapter.Update(_table);
            _dataSet.AcceptChanges();

            SqlCommand command = new SqlCommand("SELECT @@IDENTITY", _connection);
            var id = Convert.ToInt32(command.ExecuteScalar());
            var room = new Room { Name = name, State = state, Id = id };
            return room;
        }

        public void DeleteRoom(int id)
        {
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if ((int)_table.Rows[i]["Id"] == id)
                {
                    _table.Rows[i].Delete();
                }
            }
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_adapter);
            _adapter.Update(_table);
            _dataSet.AcceptChanges();
        }

        public Room GetRoomById(int id)
        {
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if ((int)_table.Rows[i]["Id"] == id)
                {
                    var row = _table.Rows[i];
                    return new Room { Id = id, Name = (string)row["Name"], State = (string)row["State"] };
                }
            }
            return null;
        }
     
        public void UpdateRoom(Room room)
        {
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if ((int)_table.Rows[i]["Id"] == room.Id)
                {
                    var row = _table.Rows[i];
                    row["Name"] = room.Name;
                    row["State"] = room.State;                    
                    break;
                }
            }
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(_adapter);
            _adapter.Update(_table);
            _dataSet.AcceptChanges();
        }

        public IEnumerable<Room> GetBlakJackRooms()
        {
            var rooms = new List<Room>();
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if ((string)_table.Rows[i]["Name"] == "Blackjack")
                {
                    var row = _table.Rows[i];
                    rooms.Add(new Room { Id = (int)row["Id"], Name = (string)row["Name"], State = (string)row["State"] });
                }
            }
            return rooms;
        }

        public IEnumerable<Room> GetPokerRooms()
        {
            var rooms = new List<Room>();
            for (int i = 0; i < _table.Rows.Count; i++)
            {
                if ((string)_table.Rows[i]["Name"] == "Poker")
                {
                    var row = _table.Rows[i];
                    rooms.Add(new Room { Id = (int)row["Id"], Name = (string)row["Name"], State = (string)row["State"] });
                }
            }
            return rooms;
        }
    }
}
