using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model.Entitites;

namespace MiniProject.Service.Interface.Service
{
    public interface IGameService
    {
        public Task<bool> Create(Games games);
        public Task<List<Games>> GetAll(int page);
        public Task<List<GameByGenre>> GetAllbyGenre(int page);
        public Task<bool> Delete(string name);
        public Task<Game> Update(Games games);
    }
}
