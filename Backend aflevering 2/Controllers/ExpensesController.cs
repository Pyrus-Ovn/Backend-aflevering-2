﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_aflevering_2.Data;
using Backend_aflevering_2.Models;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Backend_aflevering_2.Hubs;

namespace Backend_aflevering_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly Backend_aflevering_2Context _context;
        private readonly IHubContext<ExpenseHub> _hubContext;
        public ExpensesController(Backend_aflevering_2Context context, IHubContext<ExpenseHub> hub)
        {
            _context = context;
            _hubContext = hub;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpense()
        {
            return await _context.Expense.ToListAsync();
        }

        //// GET: api/Expenses/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Expense>> GetExpense(long id)
        //{
        //    var expense = await _context.Expense.FindAsync(id);

        //    if (expense == null)
        //    {
        //        return NotFound();
        //    }

        //    return expense;
        //}

        //// PUT: api/Expenses/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutExpense(long id, Expense expense)
        //{
        //    if (id != expense.ExpenseId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(expense).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ExpenseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expense>> PostExpense(ExpenseDto expenseDto)
        {
            var expense = expenseDto.Adapt<Expense>();
            _context.Expense.Add(expense);
            await _context.SaveChangesAsync();


            await _hubContext.Clients.All.SendAsync("expenseadded", expenseDto.Adapt<Expense>());

            return CreatedAtAction("GetExpense", new { id = expense.ExpenseId }, expense);
        }

    //    // DELETE: api/Expenses/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteExpense(long id)
    //    {
    //        var expense = await _context.Expense.FindAsync(id);
    //        if (expense == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Expense.Remove(expense);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ExpenseExists(long id)
    //    {
    //        return _context.Expense.Any(e => e.ExpenseId == id);
    //    }
    }
}
