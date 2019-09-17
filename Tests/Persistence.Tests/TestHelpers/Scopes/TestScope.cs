namespace UrbanEngine.Tests.Persistence.Tests.TestHelpers.Scopes
{
    public class TestScope<T> where T : class
    {
        public T InstanceUnderTest { get; protected set; }
    }
}
