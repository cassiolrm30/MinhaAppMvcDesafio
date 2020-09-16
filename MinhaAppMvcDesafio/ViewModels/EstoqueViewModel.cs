using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MinhaAppMvcDesafio.ViewModels
{
    public class EstoqueViewModel
    {
        [DisplayName("Data")]
        public string Data { get; set; }

        [DisplayName("Hora")]
        public string Hora { get; set; }

        [DisplayName("Transação")]
        public string Transacao { get; set; }

        [DisplayName("Valor Total")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
        public decimal ValorTotal { get; set; }

        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        [DisplayName("Estoque")]
        public int Estoque { get; set; }
    }
}