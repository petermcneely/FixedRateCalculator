using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CashflowCalculator.Models;

namespace CashflowCalculator.Logic
{
    public class MonthDetail
    {
        public int Month { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Interest { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Principal { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double RemainingBalance { get; set; }
    }


    public class Calculator
    {
        public static List<MonthDetail> CalculateFixedRateCashflow(Loan loan)
        {
            List<MonthDetail> flow = new List<MonthDetail>();
            double totalMonthlyPayment = GetTotalMonthlyPayment(loan);
            double remainingBalance = loan.Balance;
            for (int i = 0; i < loan.Term; ++i)
            {
                MonthDetail detail = new MonthDetail();
                detail.Month = i + 1;
                detail.Interest = remainingBalance * loan.Rate / 1200;
                detail.Principal = totalMonthlyPayment - detail.Interest;
                remainingBalance -= detail.Principal;
                detail.RemainingBalance = remainingBalance;
                flow.Add(detail);
            }

            return flow;
        }

        public static List<MonthDetail> CalculateFixedRateCashflow(IEnumerable<Loan> loans)
        {
            List<MonthDetail> flow = new List<MonthDetail>();
            List<List<MonthDetail>> workingFlow = new List<List<MonthDetail>>();
            if (loans == null || !loans.Any())
                return flow;

            foreach (var loan in loans)
                workingFlow.Add(CalculateFixedRateCashflow(loan));

            int maxMonth = workingFlow.Max(f => f.Count);
            
            for (int i = 0; i < maxMonth; ++i)
            {
                MonthDetail detail = new MonthDetail();
                detail.Month = i + 1;
                foreach (var loanFlow in workingFlow)
                {
                    if (loanFlow.Count > i)
                    {
                        detail.Interest += loanFlow.ElementAt(i).Interest;
                        detail.Principal += loanFlow.ElementAt(i).Principal;
                        detail.RemainingBalance += loanFlow.ElementAt(i).RemainingBalance;
                    }
                    
                }
                flow.Add(detail);
            }
            return flow;
        }

        private static double GetTotalMonthlyPayment(Loan loan)
        {
            return (loan.Balance * loan.Rate / 1200) / (1 - Math.Pow(1 + loan.Rate / 1200, -loan.Term));
        }
    }
}