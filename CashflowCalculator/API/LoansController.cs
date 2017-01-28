using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using CashflowCalculator.Models;

namespace CashflowCalculator.API
{
    public class LoansController : ApiController
    {
        private CashflowCalculatorContext db = new CashflowCalculatorContext();

        public IEnumerable<Loan> GetAllLoans()
        {
            return db.Loans.Select(c => c).ToList();
        }

        public IHttpActionResult GetLoan(int Id)
        {
            var loan = db.Loans.FirstOrDefault(l => l.Id == Id);
            if (loan == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(loan);
            }
        }
    }
}