using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Factorization.WPF.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<TEnum>(this TEnum value)
            where TEnum : struct, IConvertible
        {
            DescriptionAttribute descriptionAttribute = value.GetAttribute<TEnum, DescriptionAttribute>();
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            DisplayAttribute displayAttribute = value.GetAttribute<TEnum, DisplayAttribute>();
            return displayAttribute == null
                ? value.ToString(CultureInfo.InvariantCulture)
                : displayAttribute.GetDescription();
        }

        public static string GetEnumDescription(this Enum value)
        {
            MethodInfo method = typeof(EnumExtensions)
                .GetMethod(nameof(GetDescription), BindingFlags.Public | BindingFlags.Static)
                ?.MakeGenericMethod(value.GetType());

            return method?.Invoke(null, new object[] {value}).ToString();
        }

        public static IEnumerable<T> GetFlags<T>(this T args)
            where T : struct
        {
            ulong argsNum = Convert.ToUInt64(args);

            return Enum.GetValues(typeof(T)).Cast<T>().Where(v =>
            {
                ulong vNum = Convert.ToUInt64(v);
                return !v.Equals(default(T)) && (argsNum & vNum) == vNum;
            });
        }

        private static T GetAttribute<TEnum, T>(this TEnum value)
            where TEnum : struct, IConvertible
            where T : Attribute
        {
            MemberInfo[] members = value.GetType().GetMember(value.ToString(CultureInfo.InvariantCulture));
            if (!members.Any())
                return default(T);

            return members.First()
                .GetCustomAttributes(false)
                .OfType<T>()
                .LastOrDefault();
        }
    }
}