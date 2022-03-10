using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errores;
using AutoMapper;
using Core.Entidades;
using Core.Especificaciones;
using Core.Interfaces;
using Infrastructure.Datos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LugaresController : ControllerBase
    {
        public  IRepositorio<Lugar> _repoLugar { get; }
        public  IRepositorio<Pais> _repoPais { get; }
        public IMapper _mapper { get; }

        private readonly IRepositorio<Categoria> _repoCategoria;
        public LugaresController(IRepositorio<Lugar> repoLugar,
                                 IRepositorio<Pais> repoPais, 
                                 IRepositorio<Categoria> repoCategoria,
                                 IMapper mapper)
        {
            _repoCategoria = repoCategoria;
            _repoPais = repoPais;
            _repoLugar = repoLugar;
            _mapper = mapper;


        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LugarDto>>> GetLugares()
        {
            var espec = new LugaresConPaisCategoriaEspecificacion();
            var lugares = await _repoLugar.ObtenerTodosEspec(espec);
            return Ok(_mapper.Map<IReadOnlyList<Lugar>,IReadOnlyList<LugarDto>>(lugares));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LugarDto>> GetLugar(int id)
        {
            var espec = new LugaresConPaisCategoriaEspecificacion(id);
            var lugar= await _repoLugar.ObtenerEspec(espec);
            if (lugar == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Lugar, LugarDto>(lugar);
        }

        [HttpGet("paises")]
        public async Task<ActionResult<List<Pais>>> GetPaises()
        {
            var paises = await _repoPais.ObtenerTodosAsync();
            return Ok(paises);
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            var categorias = await _repoCategoria.ObtenerTodosAsync();
            return Ok(categorias);
        }

    }
}