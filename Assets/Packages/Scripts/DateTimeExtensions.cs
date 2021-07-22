//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-06-12 17:15:23
// Description: TODO >> This is a script Description.
//============================================================

using System;

namespace Zoranner.Engine.Extensions
{
    public static class DateTimeExtensions
    {
        [Obsolete("扩展方法PreviousMonths已经弃用，请使用for循环进行前几个月份的遍历。")]
        public static int[] PreviousDays(this DateTime dateTime, bool containsCurrent = true)
        {
            var length = containsCurrent ? dateTime.Day : dateTime.Day - 1;
            var intArray = new int[length];
            for (var i = 0; i < length; i++)
            {
                intArray[i] = i + 1;
            }

            return intArray;
        }

        [Obsolete("扩展方法PreviousMonths已经弃用，请使用for循环进行前几个月份的遍历。")]
        public static int[] PreviousMonths(this DateTime dateTime, bool containsCurrent = true)
        {
            var length = containsCurrent ? dateTime.Month : dateTime.Month - 1;
            var intArray = new int[length];
            for (var i = 0; i < length; i++)
            {
                intArray[i] = i + 1;
            }

            return intArray;
        }
    }
}