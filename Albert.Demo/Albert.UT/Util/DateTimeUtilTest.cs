using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albert.Util
{
    [TestFixture]
    public class DateTimeUtilTest
    {
        [Test]
        public void GetPeriodStart_Test()
        {
            var date= new DateTime(2019, 3, 27);

            var dateResult = DateTimeUtil.GetPeriodStart(date, DateFrequency.Weekly);
            Assert.AreEqual(new DateTime(2019, 3, 24), dateResult);

            dateResult = DateTimeUtil.GetPeriodStart(date, DateFrequency.Monthly);
            Assert.AreEqual(new DateTime(2019, 3, 1), dateResult);

            dateResult = DateTimeUtil.GetPeriodStart(date, DateFrequency.SemiAnnually);
            Assert.AreEqual(new DateTime(2019, 1, 1), dateResult);

            dateResult = DateTimeUtil.GetPeriodStart(date, DateFrequency.Annually);
            Assert.AreEqual(new DateTime(2019, 1, 1), dateResult);
        }

        [Test]
        public void GetWeekStartDate_Test()
        {
            var date = new DateTime(2019, 3, 27);

            var dateResult = DateTimeUtil.GetWeekStartDate(date);

            Assert.AreEqual(new DateTime(2019, 3, 24), dateResult);

            date = new DateTime(2019, 3, 17);

            dateResult = DateTimeUtil.GetWeekStartDate(date);

            Assert.AreEqual(new DateTime(2019, 3, 17), dateResult);

            date = new DateTime(2019, 3, 16);

            dateResult = DateTimeUtil.GetWeekStartDate(date);

            Assert.AreEqual(new DateTime(2019, 3, 10), dateResult);
        }

        [Test]
        public void GetMonthStartDate_Test()
        {
            var date = DateTime.Now;

            var dateResult = DateTimeUtil.GetMonthStartDate(date);

            Assert.AreEqual(new DateTime(date.Year, date.Month, 1), dateResult);
        }

        [Test]
        public void GetQuarterStartDate_Test()
        {
            var date = new DateTime(2019, 3, 27);

            var dateResult = DateTimeUtil.GetQuarterStartDate(date);

            Assert.AreEqual(new DateTime(date.Year, 1, 1), dateResult);
        }

        [Test]
        public void GetHalfYearStartDate_Test()
        {
            var date = new DateTime(2019, 3, 27);

            var dateResult = DateTimeUtil.GetHalfYearStartDate(date);

            Assert.AreEqual(new DateTime(date.Year, 1, 1), dateResult);

            date = new DateTime(2019, 8, 27);

            dateResult = DateTimeUtil.GetHalfYearStartDate(date);

            Assert.AreEqual(new DateTime(date.Year, 7, 1), dateResult);
        }

        [Test]
        public void GetYearStartDate_Test()
        {
            var date = new DateTime(2019, 3, 27);

            var dateResult = DateTimeUtil.GetYearStartDate(date);

            Assert.AreEqual(new DateTime(date.Year, 1, 1), dateResult);
        }
    }
}
