using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Shared
{
    [Table("Bases")]
    public class Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<BaseItem> BaseItems { get; set; } = new List<BaseItem>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
