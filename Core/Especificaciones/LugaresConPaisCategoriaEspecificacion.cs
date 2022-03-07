using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entidades;

namespace Core.Especificaciones
{
    public class LugaresConPaisCategoriaEspecificacion : Especificacion<Lugar>
    {
        public LugaresConPaisCategoriaEspecificacion(int id): base(x => x.Id == id)
        {
            AgregarInclude(x=>x.Pais);
            AgregarInclude(x=>x.Categoria);
        }
        public LugaresConPaisCategoriaEspecificacion()
        {
            AgregarInclude(x=>x.Pais);
            AgregarInclude(x=>x.Categoria);
            
        }
        
    }
}