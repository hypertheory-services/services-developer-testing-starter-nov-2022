

namespace Banking.UnitTests;

public class NewAccounts
{
    [Fact]
    public void NewAccountsHaveCorrectOpeningBalance()
    {
        // GIVEN
        // dummy objects are usually created in the constructor
        // of the SUT with the NEW keyword.
        // They don't pass or fail, or have anything to do with the test.
        // You just have to have something. 
        var account = new BankAccount(new Mock<INotifyOfOverdrafts>().Object, new Mock<ILogger>().Object);
        var expectedBalance = 5000M;

        // WHEN
        decimal actualBalance = account.GetBalance();

        // THEN
        Assert.Equal(expectedBalance, actualBalance);
    }
}

//public class NotifierDummy : INotifyOfOverdrafts
//{
//    public void NotifyOfOverdraftAttempt(BankAccount bankAccount, decimal amountToWithdraw)
//    {
//       // Dummy! I don't care!
//    }
//}