using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmEf6Example.Interfaces;

namespace WpfMvvmEf6Example.Models
{
    public class Post : IObjectState
    {
        public int PostId { get; set; }

        public int TopicId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Message { get; set; }

        public DateTime Created { get; set; }

        public virtual Topic Topic { get; set; }

        [NotMapped]
        public ItemState State { get; set; }
    }
}
