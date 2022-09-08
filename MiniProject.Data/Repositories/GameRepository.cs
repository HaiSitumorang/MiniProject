using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Data.Interface.Repositories;
using MiniProject.Model.Entitites;
using MiniProject.Service.Interface.Service;

namespace MiniProject.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly IDBService _dBService;

        public GameRepository(IDBService dBService)
        {
            _dBService = dBService;
        }

        public async Task<bool> InsertGenre(Game game)
        {
            await _dBService.ModifyData("INSERT IGNORE INTO genre" +
                "(nama) " +
                "values " +
                "(@Genre)", game);

            return true;
        }

        public async Task<bool> InsertGame(Game game)
        {
            await _dBService.ModifyData("INSERT INTO game " +
                "(name, release_date, rating, genre) " +
                "values " +
                "(@Name, @Release_date, @Rating, (SELECT idgenre FROM genre WHERE nama = @Genre LIMIT 1)" +
                ")", game);

            return true;
        }

        public async Task<bool> InsertGamehasGenre(Game game)
        {
            await _dBService.ModifyData("INSERT INTO game_has_genre " +
                "(game_idgame, genre_idgenre) " +
                "VALUES (" +
                "(SELECT idgame FROM game WHERE name = @Name " +
                "LIMIT 1), " +
                "(SELECT idgenre FROM genre WHERE nama = @Genre " +
                "LIMIT 1)" +
                ")", game);

            return true;
        }

        public async Task<bool> DeleteGame(string name)
        {
            await _dBService.ModifyData("DELETE FROM game " +
                "WHERE name = @Name", new { @name });
            return true;
        }

        public async Task<bool> DeleteGameHasGenre(string name)
        {
            await _dBService.ModifyData("DELETE FROM game_has_genre " +
                "WHERE game_idgame = " +
                "(SELECT idgame FROM game WHERE name = @Name)", new { name });
            return true;
        }

        public async Task<List<Game>> GetAll()
        {
            var result = await _dBService.GetData<Game>("SELECT " +
                "ga.idgame, ga.name, ga.release_date, ga.rating, ge.nama as genre " +
                "FROM game_has_genre gg " +
                "JOIN genre ge " +
                "ON gg.genre_idgenre = ge.idgenre " +
                "JOIN game ga " +
                "ON gg.game_idgame = ga.idgame " +
                "ORDER BY ga.idgame ASC", new { });
            return result;
        }

        public async Task<List<Game>> GetAllByGenre()
        {
            var result = await _dBService.GetData<Game>("SELECT " +
                "ga.name as name, ge.nama as genre " +
                "FROM game_has_genre gg " +
                "JOIN genre ge " +
                "ON gg.genre_idgenre = ge.idgenre " +
                "JOIN game ga " +
                "ON gg.game_idgame = ga.idgame " +
                "ORDER BY ge.nama ASC", new { });
            return result;
        }

        public async Task<List<Genre>> GetGenreData()
        {
            var result = await _dBService.GetData<Genre>("SELECT * FROM genre", new { });
            return result;
        }

        public async Task<Game> Update(Game game)
        {
            await _dBService.ModifyData("UPDATE game " +
                "SET release_date=@Release_date, rating=@Rating, " +
                "genre=(SELECT idgenre FROM genre WHERE nama = @Genre LIMIT 1) " +
                "WHERE name=@Name", game);
            return game;
        }
    }
}