using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils;

public class DateUtilities
{

    public bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek== DayOfWeek.Sunday;
    }

    public bool IsWeekDay(DateTime date)
    {
        return !IsWeekend(date);
    }
}
