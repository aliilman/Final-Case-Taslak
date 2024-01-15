// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations.Schema;
// using System.Linq;
// using System.Threading.Tasks;

// namespace MOS.Data.Entity
// {
//     [Table("Expense", Schema = "dbo")]
//     public class Decision
//     {
//         public int ExpenseId { get; set; }
//         public virtual Expense Expense { get; set; }

//         public int AdminId { get; set; }
//         public virtual Admin Admin { get; set; }

//         public string DecisionDescription{ get; set; }

//         public DateTime DecisionDate { get; set; }
//     }
// }