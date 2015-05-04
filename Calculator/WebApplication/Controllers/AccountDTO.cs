
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
	public class AccountDTO
    {
		public int Transactions_Count { get; set; }
		public System.Int32 Id { get; set; }
		public System.String Name { get; set; }
		public System.String Institution { get; set; }
		public System.Boolean Business { get; set; }

        public static System.Linq.Expressions.Expression<Func< Account,  AccountDTO>> SELECT =
            x => new  AccountDTO
            {
                Transactions_Count = x.Transactions.Count(),
                Id = x.Id,
                Name = x.Name,
                Institution = x.Institution,
                Business = x.Business,
            };

	}
}