using ProductCatalog.Core.Abstractions;

namespace ProductCatalog.Core.ProductManagement.ValueObjects
{
    public class CurrencyCode : ValueObject
    {
        public static readonly CurrencyCode CZK = new CurrencyCode("CZK");
        public static readonly CurrencyCode EUR = new CurrencyCode("EUR");
        public static readonly CurrencyCode HUF = new CurrencyCode("HUF");

        internal CurrencyCode(string code) => Code = code;
 
        public string Code { get;}
        
        public static CurrencyCode FromCode(string code) => code switch
        {
            "CZK" => CZK,
            "EUR" => EUR,
            "HUF" => HUF,
            _ => throw new ArgumentException($"Currency code {code} is not supported.")
        };
       
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}
