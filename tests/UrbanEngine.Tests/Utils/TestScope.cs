namespace UrbanEngine.UnitTests.Utils
{
    public abstract class TestScope<T> : ITestScope<T> where T : class
    {
        public T InstanceUnderTest { get; set; }

    }

    public interface ITestScope<T>
    {
        T InstanceUnderTest { get; set; }
    }
}
