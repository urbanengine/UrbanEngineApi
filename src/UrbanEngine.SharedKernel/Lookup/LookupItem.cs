namespace UrbanEngine.SharedKernel.Lookup
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
