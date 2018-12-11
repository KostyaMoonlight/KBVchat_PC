using DataAccess.Context;
using DataAccess.Repositories.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<NN> GetNN(string name)
        {
            return await _context.NNs.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task RemoveNN(string name)
        {
            var nn = await GetNN(name);
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
