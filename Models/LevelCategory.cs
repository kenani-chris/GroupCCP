using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace GroupCCP.Models
{
    public class LevelCategory
    {
        public int LevelCategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(30)]
        public string CategorName { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int? ParentId { get; set; }
        public virtual LevelCategory ParentCategory { get; set; }

        public ICollection<LevelCategory> ChildCategories { get; set; }
        public ICollection<Level> Levels { get; set; }

    }
}
