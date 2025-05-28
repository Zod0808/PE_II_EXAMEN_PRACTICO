namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Mision")]
    public partial class Mision
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public string Contenido { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
