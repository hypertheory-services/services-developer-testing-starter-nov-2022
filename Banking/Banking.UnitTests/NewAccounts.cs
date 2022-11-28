
using BankingDomain;

namespace Banking.UnitTests;

public class NewAccounts
{
    [Fact]
    public void NewAccountsHaveCorrectOpeningBalance()
    {
        // GIVEN
        var account = new BankAccount();
        var expectedBalance = 5000M;

        // WHEN
        decimal actualBalance = account.GetBalance();

        // THEN
        Assert.Equal(expectedBalance, actualBalance);
    }
}
