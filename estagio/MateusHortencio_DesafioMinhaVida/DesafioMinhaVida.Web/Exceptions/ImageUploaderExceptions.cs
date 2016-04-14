using System;

namespace br.mateus.DesafioMinhaVida.Web.Exceptions
{
    public class ImageUploaderExceptions : Exception
    {
        public ImageUploaderExceptions()
        {

        }

        public ImageUploaderExceptions(string mensagem)
            : base(mensagem)
        {

        }

        public partial class ImagemVaziaException : ImageUploaderExceptions
        {
            public ImagemVaziaException()
            {

            }

            public ImagemVaziaException(string mensagem)
                :base(mensagem)
            {
                
            }
        }

    }
}