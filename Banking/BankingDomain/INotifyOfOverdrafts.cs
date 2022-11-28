namespace BankingDomain
{
    public interface INotifyOfOverdrafts
    {
        void NotifyOfOverdraftAttempt(BankAccount bankAccount, decimal amountToWithdraw);
    }
}