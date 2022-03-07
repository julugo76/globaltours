using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entidades;

namespace API.Helpers
{
    public class LugarUrlResolver : IValueResolver<Lugar, LugarDto, string>
    {
        private IConfiguration _config { get; }
        public LugarUrlResolver(IConfiguration config)
        {
            _config = config;
            
        }
        public string Resolve(Lugar source, LugarDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImagenUrl))
            {
                return  _config["ApiUrl"] + source.ImagenUrl;

            }
            return null;
        }
    }
}