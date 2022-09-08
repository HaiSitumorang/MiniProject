using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Model.Entitites;
using MiniProject.Service.Interface.Service;

namespace MiniProject.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class GameController : Controller
    {        
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Games games)
        {
            var result = await gameService.Create(games);
            return Ok(result);
        }

        [HttpGet("GetAll/{page}:int")]
        public async Task<List<Games>> GetAll(int page)
        {
            var result = await gameService.GetAll(page);
            return result;
        }

        [HttpGet("GetAllbyGenre/{page}:int")]
        public async Task<List<GameByGenre>> GetAllbyGenre(int page)
        {
            var result = await gameService.GetAllbyGenre(page);
            return result;
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string name)
        {
            var result = await gameService.Delete(name);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Games games)
        {
            var result = await gameService.Update(games);
            return Ok(result);
        }
    }
}