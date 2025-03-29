using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reciter.Utils
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo != null)
            {
                var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0)
                {
                    return ((DescriptionAttribute)descriptionAttributes[0]).Description;
                }
            }
            return value.ToString();
        }
    }
}
