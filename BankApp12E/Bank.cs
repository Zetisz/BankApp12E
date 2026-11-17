namespace BankApp12E
{
    public class Bank
    {
        Dictionary<string, Account> accounts = new Dictionary<string, Account>();

        public void CreateAccount(string num, string name)
        {
            accounts.Add(num, new Account(name, 0));
        }

        public int GetBalance(string account)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[account].Balance;
        }

        public void Deposit(string account, int amount)
        {
            if (!accounts.ContainsKey(account))
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            accounts[account].Balance += amount;
        }
        
        public bool Withdraw(string account, int amount)
        {
            if (!accounts.ContainsKey(account)) 
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            if (accounts[account].Balance - amount < 0)
            {
                return false;
            }
            
            accounts[account].Balance -= amount;
            return true;
        }

        public bool Transfer(string fromAccount, string toAccount, int amount)
        {
            if (!accounts.ContainsKey(fromAccount) || !accounts.ContainsKey(toAccount)) 
            {
                throw new ArgumentException("Invalid account");
            }
            if (amount < 0)
            {
                throw new ArgumentException("Invalid amount");
            }
            if (accounts[fromAccount].Balance - amount < 0)
            {
                return false;
            }

            accounts[fromAccount].Balance -= amount;
            accounts[toAccount].Balance += amount;
            return true;
        }
        
        public bool CloseAccount(string num)
        {
            if (!accounts.TryGetValue(num, out var account))
            {
                throw new ArgumentException("Invalid account");
            }
            if (account.Balance != 0)
            {
                throw new InvalidOperationException("Account balance has to be Zero");
            }
            accounts.Remove(num);
            return true;
        }

        public string GetOwner(string num)
        {
            if (!accounts.ContainsKey(num)) 
            {
                throw new ArgumentException("Invalid account");
            }
            return accounts[num].Name;
        }
    }
}
