using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFramework.Infra.Util.Extensions
{
    public static class StringExtensions
    {
        public static string ReturnEmptyIfNull(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value;
        }
    }

    public static class PageExtensions
    {

        public static int FormatPageIndex(this int pageIndex)
        {
            if (pageIndex <= 0)
                return 1;

            return pageIndex;
        }

        public static int FormatPagesize(this int pageSize, int size = 20)
        {
            if (pageSize <= 0)
                return size;

            return pageSize;
        }
    }
}
