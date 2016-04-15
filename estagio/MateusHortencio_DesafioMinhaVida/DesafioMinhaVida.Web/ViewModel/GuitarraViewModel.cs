using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace br.mateus.DesafioMinhaVida.Web.ViewModel
{
    public class GuitarraViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite um Nome!")]
        [MaxLength(400, ErrorMessage = "O Nome deve ter menos de 400 Caracteres")]
        public string Nome { get; set; }
        [DisplayName("Preço(R$)")]
        [Required(ErrorMessage = "Digite um Preço!")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public double Preco { get; set; }
        public string PrecoString
        {
            get
            {
                return string.Format("{0:#.00}", Preco);
            }
            set
            {
                Preco = Convert.ToDouble(value);
            }
        }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Digite uma Descrição!")]
        public string Descricao { get; set; }
        [DisplayName("Data de Inclusão")]
        public DateTime DataInclusao { get; set; }
        [DisplayName("Imagem")]
        public string UrlImagem { get; set; }
        public string MensagemErro { get; set; }
        public string MensagemSucesso { get; set; }
        public string SKU
        {
            get
            {
                return string.Format("{0}_{1}", Id, Nome.Replace(" ", "_"));
            }
        }
    }
}