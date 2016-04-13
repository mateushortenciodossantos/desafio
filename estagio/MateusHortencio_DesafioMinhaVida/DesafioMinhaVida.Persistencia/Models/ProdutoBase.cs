using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace br.mateus.DesafioMinhaVida.Models
{
    public class ProdutoBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite um Nome!")]
        [MaxLength(400, ErrorMessage = "O Nome deve ter menos de 400 Caracteres")]
        public string Nome { get; set; }
        [DisplayName("Preço(R$)")]
        [Required(ErrorMessage = "Digite um Preço!")]
        public double Preco { get; set; }
        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Digite uma Descrição!")]
        public string Descricao { get; set; }
        public DateTime DataInclusao { get; set; }
        public string UrlImagem { get; set; }
        public string SKU
        {
            get
            {
                return string.Format("{0}_{1}", Id, Nome.Replace(" ", "_"));
            }
        }
    }
}