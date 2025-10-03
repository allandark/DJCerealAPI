using CerealAPI.src.Models;

namespace CerealAPI.src.Repository
{
    public class ProductFilter
    {

        /// <summary>
        /// Dictionary mapping the different operations to functions
        /// </summary>
        static public Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>> FilterTable =
            new Dictionary<string, Func<ProductFilterEntity, List<Product>, List<Product>>>
        {
            {   "=", Equal},
            {   "!=", NotEqual},
            {   "<", Less},
            {   "<=", LessOrEqual},
            {   ">", Greater},
            {   ">=", GreaterOrEqual}
        };


        public static List<Product> Sort(List<Product> products, ProductFilterEntity filter)
        {
            switch(filter.Value)
            {
                case "Mfr":
                    if(filter.SortAscending)
                        products = products.OrderBy(x => x.Mfr).ToList();
                    else
                        products = products.OrderByDescending(x => x.Mfr).ToList();
                    break;
                case "Type":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Type).ToList();
                    else
                        products = products.OrderByDescending(x => x.Type).ToList();
                    break;
                case "Name":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Name).ToList();
                    else
                        products = products.OrderByDescending(x => x.Name).ToList();
                    break;

                case "Calories":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Calories).ToList();
                    else
                        products = products.OrderByDescending(x => x.Calories).ToList();
                    break;
                case "Protien":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Protien).ToList();
                    else
                        products = products.OrderByDescending(x => x.Protien).ToList();
                    break;
                case "Fat":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Fat).ToList();
                    else
                        products = products.OrderByDescending(x => x.Fat).ToList();
                    break;
                case "Sodium":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Sodium).ToList();
                    else
                        products = products.OrderByDescending(x => x.Sodium).ToList();
                    break;
                case "Fiber":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Fiber).ToList();
                    else
                        products = products.OrderByDescending(x => x.Fiber).ToList();
                    break;
                case "Carbo":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Carbo).ToList();
                    else
                        products = products.OrderByDescending(x => x.Carbo).ToList();
                    break;
                case "Sugars":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Sugars).ToList();
                    else
                        products = products.OrderByDescending(x => x.Sugars).ToList();
                    break;
                case "Potass":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Potass).ToList();
                    else
                        products = products.OrderByDescending(x => x.Potass).ToList();
                    break;
                case "Vitamins":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Vitamins).ToList();
                    else
                        products = products.OrderByDescending(x => x.Vitamins).ToList();
                    break;
                case "Shelf":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Shelf).ToList();
                    else
                        products = products.OrderByDescending(x => x.Shelf).ToList();
                    break;
                case "Weight":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Weight).ToList();
                    else
                        products = products.OrderByDescending(x => x.Weight).ToList();
                    break;
                case "Cups":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Cups).ToList();
                    else
                        products = products.OrderByDescending(x => x.Cups).ToList();
                    break;
                case "Rating":
                    if (filter.SortAscending)
                        products = products.OrderBy(x => x.Rating).ToList();
                    else
                        products = products.OrderByDescending(x => x.Rating).ToList();
                    break;
            }
            return products;
        }

        public static List<Product> Equal(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                var value = prop.GetValue(p);
                return value != null && value.Equals(convertedFilterValue);
            }).ToList();
        }
        public static List<Product> NotEqual(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                var value = prop.GetValue(p);
                return value != null && !value.Equals(convertedFilterValue);
            }).ToList();
        }
        public static List<Product> Less(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                if(filter.Key == "Mfr" || filter.Key == "Type") return false; // cannot compare these types
                var value = prop.GetValue(p);
                if (value == null || filter.Value == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                
                if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                    return comparableValue.CompareTo(convertedFilterValue) < 0;
                return false;
            }).ToList();
        }
        public static List<Product> LessOrEqual(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                if (filter.Key == "Mfr" || filter.Key == "Type") return false; // cannot compare these types
                var value = prop.GetValue(p);
                if (value == null || filter.Value == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                    return comparableValue.CompareTo(convertedFilterValue) <= 0;
                return false;
            }).ToList();
        }
        public static List<Product> Greater(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                if (filter.Key == "Mfr" || filter.Key == "Type") return false; // cannot compare these types
                var value = prop.GetValue(p);
                if (value == null || filter.Value == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                    return comparableValue.CompareTo(convertedFilterValue) > 0;
                return false;
            }).ToList();
        }
        public static List<Product> GreaterOrEqual(ProductFilterEntity filter, List<Product> products)
        {
            return products.Where(p =>
            {
                var prop = typeof(Product).GetProperty(filter.Key);
                if (prop == null) return false;
                if (filter.Key == "Mfr" || filter.Key == "Type") return false; // cannot compare these types
                var value = prop.GetValue(p);
                if (value == null || filter.Value == null) return false;
                var convertedFilterValue = Convert.ChangeType(filter.Value, prop.PropertyType);
                if (value is IComparable comparableValue && convertedFilterValue is IComparable comparableFilter)
                    return comparableValue.CompareTo(convertedFilterValue) >= 0;
                return false;
            }).ToList();
        }
    }
}
