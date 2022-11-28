
using BankingDomain;

namespace Banking.UnitTests;

public class AccountWithdrawals
{
    [Fact]
    public void WithhdrawingMoneyDecreasesTheBalance()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object, new Mock<ILogger>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = 100M;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(openingBalance - amountToWithdraw, account.GetBalance());
    }

    [Fact]
    public void WithdrawingAllMoney()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object, new Mock<ILogger>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance;

        account.Withdraw(amountToWithdraw);

        Assert.Equal(0, account.GetBalance());

    }

    [Fact]
    public void OverdraftDoesNotDecreaseBalance()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object, new Mock<ILogger>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance + 1;

        try
        {
            account.Withdraw(amountToWithdraw);
        }
        catch (AccountOverdraftException)
        {
            // ignored for this test
        }
        finally
        {
            Assert.Equal(openingBalance, account.GetBalance());

        }


    }
    [Fact]
    public void OverdraftThrowsAnException()
    {
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object, new Mock<ILogger>().Object);
        var openingBalance = account.GetBalance();
        var amountToWithdraw = openingBalance + 1;

        Assert.Throws<AccountOverdraftException>(() =>
        {
            account.Withdraw(amountToWithdraw);
        });



    }
}