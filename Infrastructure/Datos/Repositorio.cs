using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Interfaces.Especificacion;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Datos
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public async Task<T> ObtenerAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

       

        public async Task<IReadOnlyList<T>> ObtenerTodosAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ObtenerTodosEspec(IEspecificacion<T> espec)
        {
            return await AplicarEspecificacion(espec).ToListAsync();
           
        }

         public async Task<T> ObtenerEspec(IEspecificacion<T> espec)
        {
           return await AplicarEspecificacion(espec).FirstOrDefaultAsync();
        }

        private IQueryable<T> AplicarEspecificacion(IEspecificacion<T> espec)
        {
            return EvaluadorEspecificacion<T>.GetQuery(_db.Set<T>().AsQueryable(),espec);

        }
    }
}