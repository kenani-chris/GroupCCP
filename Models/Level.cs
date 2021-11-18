using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCCP.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Level Name")]
        public string LevelName { get; set; }
        [Display(Name = "Level Parent")]
        public int? ParentId { get; set; }
        public Level ParentLevel { get; set; }
        public ICollection<Level> ChildLevels { get; set; }

        public int LevelCategoryId { get; set; }
        public LevelCategory LevelCategory { get; set; }

        public ICollection<ComplaintLogDetail> Logs { get; set; }

    }
}
