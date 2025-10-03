namespace CerealAPI.src.Repository
{
    /// <summary>
    /// Class representing a filter query entity
    /// </summary>
    public class ProductFilterEntity
    {
        public ProductFilterEntity(string key, string @operator, string value, bool sortAscending = false)
        {
            Key = key;
            Operator = @operator;
            Value = value;
            SortAscending = sortAscending;
        }

        public bool SortAscending { get; set; }
        public string Key { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
