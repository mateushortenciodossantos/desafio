using br.mateus.DesafioMinhaVida.Web.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace br.mateus.DesafioMinhaVida.Web.Utils
{
    public class ImageUploader : IUploader
    {
        private readonly string caminhoLogico = ConfigurationManager.AppSettings["ImagePath"];

        public string Upload(HttpPostedFileBase arquivo, string nomeArquivo, string caminhoFisico)
        {
            if (arquivo != null)
            {
                if (arquivo.ContentLength > 0)
                {
                    var extension = Path.GetExtension(arquivo.FileName);

                    if (string.Equals(extension, ".jpg") || string.Equals(extension, ".png"))
                    {
                        var nome = string.Format("{0}_{1:}{2:00}{3:00}{4}", nomeArquivo, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, extension);
                        var finalPath = Path.Combine(caminhoFisico, nome);
                        arquivo.SaveAs(finalPath);

                        return string.Format("{0}/{1}", caminhoLogico, nome);
                    }
                    else
                    {
                        throw new ImageUploaderExceptions(string.Format("Extensão {0} não permitida", extension));
                    }
                    
                }
                else
                {
                    throw new ImageUploaderExceptions("Arquivo vazio");
                }
            }
            else
            {
                throw new ImageUploaderExceptions("Sem arquivo(nulo)");
            }

        }
    }
}