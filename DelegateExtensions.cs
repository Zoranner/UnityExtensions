//============================================================
// Project: DigitalFactory
// Author: Zoranner@ZORANNER
// Datetime: 2019-05-18 23:49:54
// Description: TODO >> This is a script Description.
//============================================================

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Zoranner.Engine.Extensions
{
    public static class DelegateExtensions
    {
        /// <summary>
        /// Memoizes the specified func - returns the memoized version
        /// </summary>
        // Token: 0x06000009 RID: 9 RVA: 0x00002498 File Offset: 0x00000698
        [UsedImplicitly]
        public static Func<TResult> Memoize<TResult>(this Func<TResult> getValue)
        {
            var value = default(TResult);
            var hasValue = false;
            return delegate
            {
                if (hasValue)
                {
                    return value;
                }

                hasValue = true;
                value = getValue();

                return value;
            };
        }

        /// <summary>
        /// Memoizes the specified func - returns the memoized version
        /// </summary>
        // Token: 0x0600000A RID: 10 RVA: 0x000024D4 File Offset: 0x000006D4
        [UsedImplicitly]
        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func)
        {
            var dic = new Dictionary<T, TResult>();
            return delegate(T n)
            {
                TResult tResult;
                if (dic.TryGetValue(n, out tResult))
                {
                    return tResult;
                }

                tResult = func(n);
                dic.Add(n, tResult);

                return tResult;
            };
        }
    }
}