using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.CheckBook
{
    public class Transaction: BaseVM
    {
        public int Id { get; set; }

        private CheckBookVM _VM;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public CheckBookVM VM
        {
            get { return _VM; }
            set { _VM = value; OnPropertyChanged(); }
        }

        public IEnumerable<Transaction> SimilarTransactions {
            get {
                return from t in VM.Transactions
                       where t.Payee == this.Payee
                       select t;
            }
        }
        
        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged(); }
        }

        private string _Payee;
        public string Payee
        {
            get { return _Payee; }
            set { _Payee = value; OnPropertyChanged(); }
        }

        public int AccountId { get; set; }

        private Account _Account;
        public virtual Account Account
        {
            get { return _Account; }
            set { _Account = value; OnPropertyChanged(); if (VM != null) VM.OnPropertyChanged("Accounts"); }
        }

        private double _Amount;
        public double Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged(); }
        }

        private string _Tag;
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; OnPropertyChanged(); }
        }
        

    }

    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
        public bool Business { get; set; }

        public virtual IList<Transaction> Transactions { get; set; }
    }

    public class CheckBookVM: BaseVM
    {
        public CheckBookVM()
        {
            var db = new CbDb();
        }

        private int _RowsPerPage = 5;
        private int _CurrentPage = 1;
        public int CurrentPage
        {
            get { return _CurrentPage; }
            set { _CurrentPage = value; OnPropertyChanged(); OnPropertyChanged("CurrentTransactions"); }
        }

        private ObservableCollection<Transaction> _Transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get { return _Transactions; }
            set { _Transactions = value; OnPropertyChanged(); OnPropertyChanged("Accounts"); }
        }

        public IEnumerable<string> Accounts
        {
            get { return Transactions.Select(t=> t.Account.Name).Distinct(); }
        }

        public IEnumerable<Transaction> CurrentTransactions
        {
            get { return Transactions.Skip((_CurrentPage - 1) * _RowsPerPage).Take(_RowsPerPage);  }
        }

        public DelegateCommand MoveNext
        {
            get { return new DelegateCommand {
                ExecuteFunction = _ => CurrentPage ++,
                CanExecuteFunction = _ => CurrentPage * _RowsPerPage < Transactions.Count
                }; 
            }
        }

        public DelegateCommand NewTransaction
        {
            get { return new DelegateCommand {
                ExecuteFunction = _ => {
                    Transactions.Add(new Transaction { });
                    CurrentPage = Transactions.Count / _RowsPerPage + 1;
                }
            }; }
        }

        public void Fill()
        {
            Transactions = new ObservableCollection<Transaction>( new[] {
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-1), Account= new Account{ Name="Checking" }, Payee="Moshe", Amount=30, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-3), Account= new Account{ Name="Checking" }, Payee="Tim", Amount=130, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-4), Account= new Account{ Name="Checking" }, Payee="Moshe", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-5), Account= new Account{ Name="Checking" }, Payee="Bracha", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-6), Account= new Account{ Name="Checking" }, Payee="Tim", Amount=20, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-1), Account= new Account{ Name="Credit" }, Payee="Moshe", Amount=30, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-2), Account= new Account{ Name="Credit" }, Payee="Bracha", Amount=30.5, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-3), Account= new Account{ Name="Credit" }, Payee="Tim", Amount=130, Tag="Auto" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-4), Account= new Account{ Name="Credit" }, Payee="Moshe", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-5), Account= new Account{ Name="Credit" }, Payee="Bracha", Amount=35, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-2), Account= new Account{ Name="Checking" }, Payee="Bracha", Amount=30.5, Tag="Food" },
                new Transaction { VM=this, Date= DateTime.Now.AddDays(-6), Account= new Account{ Name="Credit" }, Payee="Tim", Amount=20, Tag="Auto" },
            });
        }
    }
}
