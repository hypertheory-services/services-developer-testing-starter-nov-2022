
using BankingDomain;

namespace Banking.UnitTests;

public class AccountWithdrawals
{
    [Fact]
    public void WithhdrawingMoneyDecreasesTheBalance()
    {
        var account = new BankAccount();
        var openingBalance = account.GetBalance();
        var amountToWithdraw = 100M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact(Skip ="Working on it")]
    public void WithdrawingAllMoney()
    {

    }

    [Fact (Skip ="Working on it")]
    public void Overdraft()
    {

    }
}
