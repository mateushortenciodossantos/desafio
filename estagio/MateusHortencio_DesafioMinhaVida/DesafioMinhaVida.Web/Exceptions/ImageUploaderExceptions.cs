using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace br.mateus.DesafioMinhaVida.Exceptions
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