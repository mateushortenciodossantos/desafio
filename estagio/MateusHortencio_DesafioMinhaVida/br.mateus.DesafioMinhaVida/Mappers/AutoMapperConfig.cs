using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace br.mateus.DesafioMinhaVida.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}