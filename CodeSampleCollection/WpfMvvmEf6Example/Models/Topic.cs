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
    public class Topic : IObjectState
    {
        public Topic()
        {
            Posts = new HashSet<Post>();
        }

        public int TopicId { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        public DateTime Created { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        [NotMapped]
        public ItemState State { get; set; }
    }
}
