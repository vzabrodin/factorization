using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Factorization.WPF.UIConverters
{
    public class IntRangeToCollectionConverter : IValueConverter
    {
        public int RangeStart { get; set; }

        public int RangeEnd { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Enumerable
                .Range(RangeStart, RangeEnd)
                .Select(i => new CollectionViewModel<int?>
                {
                    DisplayText = i.ToString(),
                    Value = i
                });

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => null;
    }
}