using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errores;
using Infrastructure.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ErrorTestController : Controller
    {
        private readonly ApplicationDbContext _bd;
        public ErrorTestController(ApplicationDbContext db)
        {
           _bd= db;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundError(){
            var algo= _bd.Lugares.Find(166);
            if (algo == null){
                return NotFound(new ApiResponse(404));
            }
            return Ok();
        }

         [HttpGet("servererror")]
        public ActionResult GetServerError(){
            var algo = _bd.Lugares.Find(166);
            var algoaRetornar = algo.ToString();
            
            return Ok();
        }

         [HttpGet("badrequest")]
        public ActionResult GetBadRequestError(){
          return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequestError(int id){
          return BadRequest(new ApiResponse(404));
        }

    
    }
}