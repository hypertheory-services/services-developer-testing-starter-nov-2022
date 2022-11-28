
using BankingDomain;

namespace Banking.UnitTests;

public class BankAccountOverdraftNotifications
{
    [Fact]
    public void ApiIsNotifiedUponOverdraft()
    {
        // Given
        var mockedNotifier = new Mock<INotifyOfOverdrafts>();
        var account = new BankAccount(mockedNotifier.Object, new Mock<ILogger>().Object); // TODO: Need a mock object.
        var amountToWithdraw = account.GetBalance() + .01M;

        // When I overdraft
        try
        {
            account.Withdraw(amountToWithdraw); // Cause an Overdraft
        }
        catch (Exception)
        {
            // swallow it!
        }


        // THEN

        // Verify the notifier was called with the account and the amount;
        mockedNotifier.Verify(m => m.NotifyOfOverdraftAttempt(account, amountToWithdraw), Times.Once);

    }

    [Fact]
    public void ApiIsNotNotifiedWhenNoOverdraft()
    {
        var mockedNotifier = new Mock<INotifyOfOverdrafts>();
        var account = new BankAccount(mockedNotifier.Object, new Mock<ILogger>().Object); // TODO: Need a mock object.
        var amountToWithdraw = 1M;

        // When I overdraft

        account.Withdraw(amountToWithdraw); // Cause an Overdraft



        // THEN

        // Verify the notifier was called with the account and the amount;
        mockedNotifier.Verify(m => m.NotifyOfOverdraftAttempt(account, amountToWithdraw), Times.Never);
    }

    [Fact]
    public void IfApiThrowsWriteToTheLogger()
    {
        // Given
        var stubbedNotifier = new Mock<INotifyOfOverdrafts>();
        var mockedLogger = new Mock<ILogger>();
        var account = new BankAccount(stubbedNotifier.Object, mockedLogger.Object); // TODO: Need a mock object.
        var amountToWithdraw = account.GetBalance() + 1M;
        stubbedNotifier.Setup(m => 
        m.NotifyOfOverdraftAttempt(It.IsAny<BankAccount>(), It.IsAny<decimal>())).Throws(new HttpRequestException());


        // When

        try
        {
            account.Withdraw(amountToWithdraw);
        }
        catch (AccountOverdraftException)
        {

           // was expecting that.
        }

        // Then....
        mockedLogger.Verify(m => m.LogError("The Notification API Is Down - Overdraft", amountToWithdraw));
    }
}
