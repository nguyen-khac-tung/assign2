using ViewModels;

namespace FUNewsManagement.Repository
{
    public interface IAccountRepository
    {
        public AccountDetailVM? GetAccount(AccountLoginVM accountLogin);
        public List<AccountDetailVM> GetAllAccount();
        public AccountDetailVM? GetAccountById(int accountId);
        public AccountDetailVM? GetAccountByEmail(string email);
        public bool CheckEmailExist(AccountDetailVM account);
        public void EditAccount(AccountDetailVM account);
        public void AddAccount(AccountDetailVM account);
        public void DeleteAccount(int id);
        public void EditProfile(AccountDetailVM account);
    }
}
