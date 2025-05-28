namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AnalisisPEST")]
    public partial class AnalisisPEST
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public string Politico { get; set; }

        public string Economico { get; set; }

        public string Social { get; set; }

        public string Tecnologico { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
