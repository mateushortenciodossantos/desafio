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

    }
}