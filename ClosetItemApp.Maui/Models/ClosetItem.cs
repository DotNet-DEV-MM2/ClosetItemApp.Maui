using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosetItemApp.Maui.Models
{
    [Table("closetItems") ]
    public class ClosetItem : BaseEntity
    {
        public string ShortName { get; set; }

        [MaxLength(12)]
        [Unique]
        public string ItemType { get; set; } 
        public string Color { get; set; }   

    }
}
