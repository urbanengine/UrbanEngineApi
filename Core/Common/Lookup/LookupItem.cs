using System.Security.Cryptography.X509Certificates;

namespace UrbanEngine.Core.Common.Lookup
{
    public class LookupItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public LookupItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public  LookupItem() { }
    }
}
