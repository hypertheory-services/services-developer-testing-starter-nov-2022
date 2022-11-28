using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingDomain;

public class ConsoleLogger : ILogger
{
    public void LogError(string message, decimal amountToWithdraw)
    {
        Console.Write($"{message} {amountToWithdraw:c}");
    }
}
