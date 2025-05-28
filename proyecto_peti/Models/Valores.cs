namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Valores
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        [StringLength(200)]
        public string Valor { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
