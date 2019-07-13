namespace UrbanEngine.Tests.Application.TestHelpers.Scopes
{
    public class TestScope<T> where T : class
    {
        public T InstanceUnderTest { get; protected set; }
    }
}
