namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatrizRACI")]
    public partial class MatrizRACI
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        [StringLength(255)]
        public string Actividad { get; set; }

        [StringLength(100)]
        public string Responsable { get; set; }

        [StringLength(100)]
        public string Aprobador { get; set; }

        [StringLength(100)]
        public string Consultado { get; set; }

        [StringLength(100)]
        public string Informado { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
