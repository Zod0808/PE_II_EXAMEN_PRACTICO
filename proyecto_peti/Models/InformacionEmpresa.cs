namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformacionEmpresa")]
    public partial class InformacionEmpresa
    {
        public int Id { get; set; }

        public int PlanId { get; set; }

        [StringLength(255)]
        public string NombreEmpresa { get; set; }

        public string Descripcion { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
