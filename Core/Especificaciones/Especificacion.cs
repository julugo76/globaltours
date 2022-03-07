using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces.Especificacion;

namespace Core.Especificaciones
{
    public class Especificacion<T> : IEspecificacion<T>
    {
        public Especificacion()
        {
            
        }
        public Especificacion(Expression<Func<T, bool>> filtro)
        {
            Filtro = filtro;
            
        }
        public Expression<Func<T, bool>> Filtro {get;}

        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        protected void AgregarInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);

        }
    }
}