﻿using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Backend_aflevering_2.Models
{
    [AdaptTo("[name]Dto"), GenerateMapper,]
    public class Job
    {
        public long JobId { get; set; }
        [MaxLength(64)]
        public string? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        [MaxLength(128)]
        public string? Location { get; set; }
        [MaxLength(2000)]
        public string? Comments { get; set; }
        public List<Model>? Models { get; set; }
        public List<Expense>? Expenses { get; set; }

    }
}
