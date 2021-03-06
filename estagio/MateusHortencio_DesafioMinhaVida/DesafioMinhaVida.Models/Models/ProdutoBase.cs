﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace br.mateus.DesafioMinhaVida.Models.Models
{
    public class ProdutoBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
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