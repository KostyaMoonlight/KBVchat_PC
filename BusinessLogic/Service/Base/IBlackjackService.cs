using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.Base
{
    public interface IBlackjackService
    {
        int AddRoom();
        void AddUserToRoom(int userId);

    }
}
