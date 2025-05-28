namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ObjetivosEspecificos
    {
        public int Id { get; set; }

        public int ObjetivoId { get; set; }

        public string Detalle { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ObjetivosEstrategicos ObjetivosEstrategicos { get; set; }
    }
}
