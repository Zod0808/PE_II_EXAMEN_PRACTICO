namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ResumenEjecutivo")]
    public partial class ResumenEjecutivo
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        public string Introduccion { get; set; }

        public string Alcance { get; set; }

        public string ResultadosEsperados { get; set; }

        public string Conclusiones { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
