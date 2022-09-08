using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniProject.Data.Interface.Repositories;
using MiniProject.Model.Entitites;
using MiniProject.Service.Interface.Service;

namespace MiniProject.Service.Service
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task<bool> Create(Games games)
        {       
            Game game = new Game();
            game.Idgame = games.Idgame;
            game.Name = games.Name.ToLower();
            game.Release_date = games.Release_date;
            game.Rating = games.Rating;

            await GenreCheck(games, game);
            await GameCheck(games, game);

            for (int i = 0; i < games.Genre.Count; i++)
            {
                game.Genre = games.Genre[i].ToLower();
                await gameRepository.InsertGamehasGenre(game);
            }
            return true;
        }

        public async Task<bool> Delete(string name)
        {
            var result = await gameRepository.DeleteGame(name);
            return result;
        }

        public async Task<List<Games>> GetAll(int page)
        {
            var dataGame = await gameRepository.GetAll();
            List<Games> games = new List<Games>();
            Game game = new Game();
            int x = 0;

            game = dataGame[0];
            games.Add(new Games
            {
                Idgame = game.Idgame,
                Name = game.Name,
                Release_date = game.Release_date,
                Rating = game.Rating,
            });

            for (int i = 0; i < dataGame.Count; i++)
            {
                if(game.Name == dataGame[i].Name)
                {
                    games[x].Genre.Add(dataGame[i].Genre);
                }
                else
                {
                    x++;
                    game = dataGame[i];
                    games.Add(new Games
                    {
                        Idgame = game.Idgame,
                        Name = game.Name,
                        Release_date = game.Release_date,
                        Rating = game.Rating,
                        Genre = { game.Genre }
                    });
                }
            }

            var result = pagingSystem(games, page);
            return result;
        }

        public async Task<List<GameByGenre>> GetAllbyGenre(int page)
        {
            var dataGame = await gameRepository.GetAllByGenre();
            List<GameByGenre> gamebyGenres = new List<GameByGenre>();
            Game game = new Game();int x = 0;
            game = dataGame[0];
            gamebyGenres.Add(new GameByGenre
            {
                Genre = game.Genre
            });
            for (int i = 0; i < dataGame.Count; i++)
            {
                if (game.Genre == dataGame[i].Genre)
                {
                    gamebyGenres[x].Name.Add(dataGame[i].Name);
                }
                else
                {
                    x++;
                    game = dataGame[i];
                    gamebyGenres.Add(new GameByGenre
                    {
                        Genre = game.Genre,
                        Name = { game.Name }
                    });
                }
            }
            var result = pagingSystem(gamebyGenres, page);
            return result;
        }

        public async Task<Game> Update(Games games)
        {
            Game game = new Game();
            game.Idgame = games.Idgame;
            game.Name = games.Name.ToLower();
            game.Release_date = games.Release_date;
            game.Rating = games.Rating;

            await GenreCheck(games, game);
            await gameRepository.DeleteGameHasGenre(game.Name.ToLower());
            var result = await gameRepository.Update(game);
            for (int i = 0; i < games.Genre.Count; i++)
            {
                game.Genre = games.Genre[i].ToLower();
                await gameRepository.InsertGamehasGenre(game);
            }
            return result;
        }

        public async Task<bool> GenreCheck(Games games, Game game)
        {
            var genreList = await gameRepository.GetGenreData();
            List<string> genreNama = new List<string>();

            for (int i = 0; i < genreList.Count; i++)
            {
                genreNama.Add(genreList[i].Nama.ToLower());
            }

            for (int i = 0; i < games.Genre.Count; i++)
            {
                game.Genre = games.Genre[i].ToLower();
                if (!genreNama.Contains(games.Genre[i].ToLower()))
                {
                    await gameRepository.InsertGenre(game);
                }
            }
            return true;
        }
        public async Task<bool> GameCheck(Games games, Game game)
        {
            var gameList = await gameRepository.GetAll();
            List<string> gameName = new List<string>();

            for (int i = 0; i < gameList.Count; i++)
            {
                gameName.Add(gameList[i].Name.ToLower());
            }

            if (!gameName.Contains(games.Name))
            {
                await gameRepository.InsertGame(game);
                gameList = await gameRepository.GetAll();
                for (int j = 0; j < gameList.Count; j++)
                {
                    gameName.Add(gameList[j].Name.ToLower());
                }
            }
            return true;
        }
        public List<T> pagingSystem<T>(List<T> games, int page)
        {
            var result = games;
            int start = 10 * (page - 1); int step = 10;
            decimal maxPage = Math.Ceiling(Convert.ToDecimal(games.Count / 10)) * 10;
            if (maxPage < start)
            {
                result = new List<T>();
                return result;
            }
            else if (start + step > games.Count)
            {
                step = games.Count - start;
            }
            result = games.GetRange(start, step);
            return result;
        }
    }
}
