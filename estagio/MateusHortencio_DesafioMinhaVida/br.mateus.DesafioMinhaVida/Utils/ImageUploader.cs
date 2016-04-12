using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace br.mateus.DesafioMinhaVida.Utils
{
    public class ImageUploader : IUploader
    {
        private readonly string caminhoLogico = ConfigurationManager.AppSettings["ImagePath"];

        public string Upload(HttpPostedFileBase arquivo, string nomeArquivo, string caminhoFisico)
        {
            try
            {
                if (arquivo != null)
                {
                    if (arquivo.ContentLength > 0)
                    {
                        var nome = string.Format("{0}_{1:}{2:00}{3:00}{4}", nomeArquivo, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Path.GetExtension(arquivo.FileName));
                        var finalPath = Path.Combine(caminhoFisico, nome);
                        arquivo.SaveAs(finalPath);

                        return string.Format("{0}/{1}", caminhoLogico, nome);
                    }
                    else
                    {
                        throw new Exception("Arquivo vazio");
                    }
                }
                else
                {
                    throw new Exception("Sem arquivo");
                }
                
            }
            catch (Exception)
            {
                throw new Exception("Erro no processo de salvamento das imagens");//todo implementar erros
            }
        }
    }
}