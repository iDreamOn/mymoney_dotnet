using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mymoney.Utilities
{
    public class DateUtility
    {
        public static int GetDaysBetweenDates(DateTime StartDate, DateTime EndDate)
        {
            int weeks = (int)(EndDate - StartDate).TotalDays;
            return weeks;
        }
        public static int GetWeeksBetweenDates(DateTime StartDate, DateTime EndDate)
        {
            int weeks = (int)(EndDate - StartDate).TotalDays / 7;
            return weeks;
        }
        public static int GetBiWeeksBetweenDates(DateTime StartDate, DateTime EndDate)
        {
            int weeks = (int)(EndDate - StartDate).TotalDays / 14;
            return weeks;
        }
        public static int GetMonthsBetweenDates(DateTime StartDate, DateTime EndDate)
        {
            int weeks = (int)(EndDate - StartDate).TotalDays / 30;
            return weeks;
        }
    }
}