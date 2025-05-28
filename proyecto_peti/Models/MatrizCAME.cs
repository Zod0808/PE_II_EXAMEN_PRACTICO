namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatrizCAME")]
    public partial class MatrizCAME
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public string Corregir { get; set; }

        public string Afrontar { get; set; }

        public string Mantener { get; set; }

        public string Explotar { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
