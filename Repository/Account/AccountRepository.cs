using Models;
using ViewModels;

namespace FUNewsManagement.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly NewsDBContext _context;

        public AccountRepository(NewsDBContext dbContext)
        {
            _context = dbContext;
        }

        public AccountDetailVM? GetAccount(AccountLoginVM accountLogin)
        {
            var account = (from a in _context.SystemAccounts
                           where a.AccountEmail == accountLogin.AccountEmail
                                        && a.AccountPassword == accountLogin.AccountPassword
                           select new AccountDetailVM()
                           {
                               AccountEmail = a.AccountEmail,
                               AccountRole = a.AccountRole,
                               RoleName = GetRoleName(a.AccountRole)
                           }).FirstOrDefault();
            return account;
        }

        public List<AccountDetailVM> GetAllAccount()
        {
            var list = (from a in _context.SystemAccounts
                        where a.AccountRole != 0
                        select new AccountDetailVM()
                        {
                            AccountId = a.AccountId,
                            AccountName = a.AccountName,
                            AccountEmail = a.AccountEmail,
                            AccountRole = a.AccountRole,
                            RoleName = GetRoleName(a.AccountRole),
                            IsActive = a.IsActive,
                            Status = (a.IsActive == true ? "Active" : "InActive")
                        }).ToList();
            return list;
        }

        public AccountDetailVM? GetAccountById(int accountId)
        {
            var account = (from a in _context.SystemAccounts
                           where a.AccountId == accountId
                           select new AccountDetailVM()
                           {
                               AccountId = a.AccountId,
                               AccountName = a.AccountName,
                               AccountEmail = a.AccountEmail,
                               AccountPassword = a.AccountPassword,
                               AccountRole = a.AccountRole,
                               RoleName = GetRoleName(a.AccountRole),
                               IsActive = a.IsActive,
                               Status = (a.IsActive == true ? "Active" : "InActive")
                           }).FirstOrDefault();
            return account;
        }

        public AccountDetailVM? GetAccountByEmail(string email)
        {
            var account = (from a in _context.SystemAccounts
                           where a.AccountEmail == email
                           select new AccountDetailVM()
                           {
                               AccountId = a.AccountId,
                               AccountName = a.AccountName,
                               AccountEmail = a.AccountEmail,
                               AccountRole = a.AccountRole,
                               RoleName = GetRoleName(a.AccountRole)
                           }).FirstOrDefault();
            return account;
        }

        public void AddAccount(AccountDetailVM account)
        {
            SystemAccount newAccount = new SystemAccount();
            newAccount.AccountName = account.AccountName;
            newAccount.AccountEmail = account.AccountEmail;
            newAccount.AccountRole = account.AccountRole;
            newAccount.AccountPassword = account.AccountPassword;
            newAccount.IsActive = true;

            _context.SystemAccounts.Add(newAccount);
            _context.SaveChanges();
        }

        public void EditAccount(AccountDetailVM account)
        {
            var oldAccount = _context.SystemAccounts.First(a => a.AccountId == account.AccountId);
            oldAccount.AccountName = account.AccountName;
            oldAccount.AccountEmail = account.AccountEmail;
            oldAccount.AccountPassword = account.AccountPassword;
            oldAccount.AccountRole = account.AccountRole;
            oldAccount.IsActive = account.IsActive;

            _context.SaveChanges();
        }

        public void DeleteAccount(int id)
        {
            var account = _context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);

            _context.SystemAccounts.Remove(account);
            _context.SaveChanges(); 
        }

        public void EditProfile(AccountDetailVM account)
        {
            var oldAccount = _context.SystemAccounts.First(a => a.AccountId == account.AccountId);
            oldAccount.AccountName = account.AccountName;
            oldAccount.AccountEmail = account.AccountEmail;
            oldAccount.AccountPassword = account.AccountPassword;

            _context.SaveChanges();
        }

        public bool CheckEmailExist(AccountDetailVM account)
        {
            var acc = (from a in _context.SystemAccounts
                       where a.AccountId != account.AccountId && a.AccountEmail.ToLower() == account.AccountEmail.ToLower()
                       select a).FirstOrDefault();
            return acc != null ? true : false;
        }

        private static string GetRoleName(int? roleId)
        {
            switch (roleId)
            {
                case 0:
                    return "Admin";
                case 1:
                    return "Staff";
                case 2:
                    return "Lecture";
                default:
                    return "";
            }
        }
    }
}
