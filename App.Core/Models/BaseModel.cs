using App.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public RecordStatus RecordStatus { get; set; }
    }
}
