using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Domain.Enum
{
    public enum Priority
    {
        [Display(Name = "Easy")]
        Easy = 1,
        [Display(Name = "Medium")]
        Medium = 2,
        [Display(Name = "Hard")]
        Hard = 3
    }
}
