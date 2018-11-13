namespace UrbanEngine.Infrastructure.Context {
    public class DbContextSettings : IDbContextSettings {
        public string ConnectionString { get; set; }
        public string SchemaName { get; set; }
    }
}
