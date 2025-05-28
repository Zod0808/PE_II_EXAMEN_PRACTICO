namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IniciativasEstrategicas
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        [StringLength(100)]
        public string Perspectiva { get; set; }

        public string Descripcion { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
