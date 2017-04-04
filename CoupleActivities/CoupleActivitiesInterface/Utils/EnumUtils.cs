using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoupleActivities.Utils
{
    static class EnumUtils
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string[] GetEnumNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
