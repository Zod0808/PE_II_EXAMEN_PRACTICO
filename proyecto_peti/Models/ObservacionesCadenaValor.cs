using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_peti.Models
{
    public class ObservacionesCadenaValor
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public string Fortalezas1 { get; set; }
        public string Fortalezas2 { get; set; }
        public string Fortalezas3 { get; set; }
        public string Fortalezas4 { get; set; }
        public string Debilidades1 { get; set; }
        public string Debilidades2 { get; set; }
        public string Debilidades3 { get; set; }
        public string Debilidades4 { get; set; }
    }
}
