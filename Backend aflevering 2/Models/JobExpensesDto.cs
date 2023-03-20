using System;
using System.Collections.Generic;
using Backend_aflevering_2.Models;

namespace Backend_aflevering_2.Models
{
    public partial class JobExpensesDto
    {
        public long JobId { get; set; }
        public string? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        public string? Location { get; set; }
        public string? Comments { get; set; }
       
    }
}