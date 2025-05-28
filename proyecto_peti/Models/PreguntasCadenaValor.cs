using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_peti.Models
{
    public class PreguntasCadenaValor
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public int PreguntaNumero { get; set; }
        public string PreguntaTexto { get; set; }
        public int Valoracion { get; set; }
    }
}
