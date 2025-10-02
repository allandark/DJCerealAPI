namespace CerealAPI.src.Repository
{
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
