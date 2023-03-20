﻿using Mapster;
using System.ComponentModel.DataAnnotations.Schema;



namespace Backend_aflevering_2.Models
{
    [AdaptTo("[name]Dto")]
    public class Expense
    {
        public long ExpenseId { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
