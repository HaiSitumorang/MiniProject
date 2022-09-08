using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Model.Entitites;

namespace MiniProject.Data.Interface.Repositories
{
    public interface IGameRepository
    {
        public Task<bool> InsertGame(Game game);
        public Task<bool> InsertGenre(Game game);
        public Task<bool> InsertGamehasGenre(Game game);
        public Task<List<Game>> GetAll();
        public Task<List<Game>> GetAllByGenre();
        public Task<List<Genre>> GetGenreData();
        public Task<bool> DeleteGame(string name);
        public Task<bool> DeleteGameHasGenre(string name);
        public Task<Game> Update(Game game);
    }
}
