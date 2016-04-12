using System.Web;

namespace br.mateus.DesafioMinhaVida.Utils
{
    public interface IUploader
    {
        string Upload(HttpPostedFileBase arquivo, string nomeArquivo, string caminhoFisico);
    }
}
