namespace proyecto_peti.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ObjetivosEstrategicos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjetivosEstrategicos()
        {
            ObjetivosEspecificos = new HashSet<ObjetivosEspecificos>();
        }

        public int Id { get; set; }

        public int PlanId { get; set; }

        public string Objetivo { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObjetivosEspecificos> ObjetivosEspecificos { get; set; }

        public virtual PlanEstrategico PlanEstrategico { get; set; }
    }
}
