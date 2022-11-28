namespace BankingDomain;

public class BankAccount
{
    private decimal _balance = 5000m;
    public BankAccount()
    {
    }

    public void Deposit(decimal amountToDeposit)
    {
        _balance += amountToDeposit;
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public void Withdraw(decimal amountToWithdraw)
    {
        _balance -= amountToWithdraw;
    }
}