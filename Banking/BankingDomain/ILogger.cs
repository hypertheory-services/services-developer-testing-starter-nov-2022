namespace BankingDomain;

public interface ILogger
{
    void LogError(string message, decimal amountToWithdraw);
}