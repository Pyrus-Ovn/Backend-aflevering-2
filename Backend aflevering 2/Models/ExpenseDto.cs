using System;

namespace Backend_aflevering_2.Models
{
    public partial class ExpenseDto
    {
        public long ExpenseId { get; set; }
        public long ModelId { get; set; }
        public long JobId { get; set; }
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        public decimal amount { get; set; }
    }
}