using br.mateus.DesafioMinhaVida.Web.Exceptions;
using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace br.mateus.DesafioMinhaVida.Web.Utils
{
    public class ImageUploader : IUploader
    {
        //String do caminho lógico até a pasta de imagens
        private readonly string caminhoLogico = ConfigurationManager.AppSettings["ImagePath"];
        //recebe como parametro um arquivo recebido, nome e caminhoFisico
        public string Upload(HttpPostedFileBase arquivo, string nomeArquivo, string caminhoFisico)
        {
            if (arquivo != null)
            {
                if (arquivo.ContentLength > 0)
                {
                    //pega a extensão do arquivo
                    var extension = Path.GetExtension(arquivo.FileName);
                    //se for uma imagem no formato aceito
                    if (string.Equals(extension, ".jpg") || string.Equals(extension, ".png"))
                    {
                        //gera um nome com um pequeno timestamp e o nome do arquivo
                        var nome = string.Format("{0}_{1:}{2:00}{3:00}{4}", nomeArquivo, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, extension);
                        //combina o nome com o caminho fisico
                        var finalPath = Path.Combine(caminhoFisico, nome);
                        //Salva o arquivo no caminho fisico settado acima
                        arquivo.SaveAs(finalPath);
                        //retorna o caminho logico para fins de visualização da imagem na aplicação
                        return string.Format("{0}/{1}", caminhoLogico, nome);
                    }
                    else
                    {
                        //Se o arquivo não for uma imagem/extensão nao for a correta
                        throw new ImageUploaderExceptions(string.Format("Extensão {0} não permitida", extension));
                    }
                    
                }
                else
                {
                    //Arquivo vazio
                    throw new ImageUploaderExceptions("Arquivo vazio");
                }
            }
            else
            {
                //sem arquivo
                throw new ImageUploaderExceptions("Sem arquivo(nulo)");
            }

        }
    }
}