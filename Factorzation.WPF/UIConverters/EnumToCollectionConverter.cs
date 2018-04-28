using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using Factorization.WPF.Extensions;

namespace Factorization.WPF.UIConverters
{
    public class EnumToCollectionConverter : IValueConverter
    {
        public Type EnumType { get; set; }

        public bool IsSortByValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Array enumValues = Enum.GetValues(EnumType);
            MethodInfo methodInfo = typeof(EnumExtensions).GetMethod(nameof(EnumExtensions.GetDescription))
                ?.MakeGenericMethod(EnumType);

            var enumCollection = enumValues.Cast<Enum>().Select(e =>
                new CollectionViewModel<Enum>
                {
                    Value = e,
                    DisplayText = methodInfo?.Invoke(null, new object[] {e}).ToString()
                });

            enumCollection = IsSortByValue
                ? enumCollection.OrderBy(e => e.Value)
                : enumCollection.OrderBy(e => e.DisplayText);

            return enumCollection.ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}