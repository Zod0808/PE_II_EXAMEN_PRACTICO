namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FuerzasPorter")]
    public partial class FuerzasPorter
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public string AmenazaNuevos { get; set; }

        public string RivalidadCompetidores { get; set; }

        public string PoderProveedores { get; set; }

        public string PoderClientes { get; set; }

        public string AmenazaSustitutos { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
