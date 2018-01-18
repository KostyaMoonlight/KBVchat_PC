using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class NNRepository
        : INNRepository
    {
        KVBchatDbContext _context = null;

        public NNRepository(KVBchatDbContext context)
        {
            _context = context;
        }

        public void AddNN(string name, string nn)
        {
            _context.NNs.Add(new NN { Name = name, JsonNN = nn });
            SaveChanges();
        }

        public NN GetNN(string name)
        {
            return _context.NNs.FirstOrDefault(x => x.Name == name);
        }

        public void RemoveNN(string name)
        {
            var nn = GetNN(name);
            if (nn != null)
            {
                _context.NNs.Remove(nn);
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
