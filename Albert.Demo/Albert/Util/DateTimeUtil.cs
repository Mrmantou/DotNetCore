using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Util
{
    public class DateTimeUtil
    {
        public static DateTime GetPeriodStart(DateTime date, DateFrequency frequency)
        {
            DateTime result = date;
            switch (frequency)
            {
                case DateFrequency.Weekly:
                    result = GetWeekStartDate(date);
                    break;
                case DateFrequency.Monthly:
                    result = GetMonthStartDate(date);
                    break;
                case DateFrequency.Quarterly:
                    result = GetQuarterStartDate(date);
                    break;
                case DateFrequency.SemiAnnually:
                    result = GetHalfYearStartDate(date);
                    break;
                case DateFrequency.Annually:
                case DateFrequency.AnnuallyIncludingYearToDate:
                    result = GetYearStartDate(date);
                    break;
                default:
                    break;
            }
            return result;
        }
        
        public static DateTime GetWeekStartDate(DateTime date)
        {
            return date.AddDays(-(double)date.DayOfWeek);
        }

        public static DateTime GetMonthStartDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime GetQuarterStartDate(DateTime date)
        {
            int month = (date.Month - 1) / 3 * 3 + 1;
            return new DateTime(date.Year, month, 1);
        }

        public static DateTime GetHalfYearStartDate(DateTime date)
        {
            int month = ((date.Month - 1) / 6) * 6 + 1;
            return new DateTime(date.Year, month, 1);
        }

        public static DateTime GetYearStartDate(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
    }

    public enum DateFrequency
    {
        None,
        Daily,
        Weekly,
        QuarterMonthly,
        SemiMonthly,
        Monthly,
        Bimonthly,
        Quarterly,
        SemiAnnually,
        Annually,
        Irregular,
        AnnuallyIncludingYearToDate
    }
}
