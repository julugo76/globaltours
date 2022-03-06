using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Core.Entidades;
using Infrastructure.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        
        
        
        private readonly ApplicationDbContext _db;

        public LugaresController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<ActionResult<List<Lugar>>> GetLugares()
        {
            var lugares =  await _db.Lugares.ToListAsync();
            return Ok(lugares);
        }

        [HttpGet("{id}")]
        public async Task<Lugar> GetLugar(int id)
        {
            return await _db.Lugares.FirstOrDefaultAsync(l=>l.Id==id);
        }
        
    }
}