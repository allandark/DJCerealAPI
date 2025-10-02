namespace CerealAPI.src.Repository
{
    /// <summary>
    /// Class representing a filter query entity
    /// </summary>
    public class ProductFilterEntity
    {
        public ProductFilterEntity(string key, string @operator, string value)
        {
            Key = key;
            Operator = @operator;
            Value = value;
        }

        public string Key { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}
