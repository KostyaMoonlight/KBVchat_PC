using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Base
{
    public interface INNRepository
        : ISaveChanges
    {
        void AddNN(string name, string nn);
        Task<NN> GetNN(string name);
        Task RemoveNN(string name);
    }
}
