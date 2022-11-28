



using BankingDomain;

namespace Banking.UnitTests;

public class AccountDeposits
{
    [Fact]
    public void DepositIncreasesTheBalance()
    {
        // Given
        var account = new BankAccount();
        var amountToDeposit = 100M;
        var openingBalance = account.GetBalance();

        // When
        account.Deposit(amountToDeposit);

        // Then
        Assert.Equal(amountToDeposit + openingBalance, account.GetBalance());
    }
}
