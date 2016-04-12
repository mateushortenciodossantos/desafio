using AutoMapper;
using br.mateus.DesafioMinhaVida.ViewModel;
using br.mateus.DesafioMinhaVida.Models;

namespace br.mateus.DesafioMinhaVida.Mappers
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Guitarra, GuitarraViewModel>();
            });
        }
    }
}