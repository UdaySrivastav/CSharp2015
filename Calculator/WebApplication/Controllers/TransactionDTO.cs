
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Model
{
	public class TransactionDTO
    {
		public string Account_Name { get; set; }
		public System.Int32 Id { get; set; }
		public System.DateTime Date { get; set; }
		public System.String Payee { get; set; }
		public System.Int32 AccountId { get; set; }
		public System.Double Amount { get; set; }
		public System.String Tag { get; set; }

        public static System.Linq.Expressions.Expression<Func< Transaction,  TransactionDTO>> SELECT =
            x => new  TransactionDTO
            {
                Account_Name = x.Account.Name,
                Id = x.Id,
                Date = x.Date,
                Payee = x.Payee,
                AccountId = x.AccountId,
                Amount = x.Amount,
                Tag = x.Tag,
            };

	}
}