using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Core.Entities.Base
{
    public abstract class BaseEntity : IValidatableObject
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        //TODO: move date generation to database
        public DateTime CreationDate { get; set; }
        //TODO: move updating date to database
        public DateTime LastUpdatedDate { get; set; }

        //[ConcurrencyCheck]
        //[Timestamp]
        //public virtual byte[] RowVersion { get; set; }

        public RecordStatus RecordStatus { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }

    }
}
