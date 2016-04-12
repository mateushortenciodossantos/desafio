using AutoMapper;
using br.mateus.DesafioMinhaVida.Models;
using br.mateus.DesafioMinhaVida.ViewModel;

namespace br.mateus.DesafioMinhaVida.Mappers
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {

            get { return "ViewModelToDomainMappings"; }

        }

        protected override void Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GuitarraViewModel, Guitarra>();
            });
        }
    }
}