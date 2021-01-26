using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;

namespace Expeditions.Models
{
    public class Injuries
    {
        [Key]
        [Column("InjurySustained")]
        public bool _injury { get; set; }
    };
}
