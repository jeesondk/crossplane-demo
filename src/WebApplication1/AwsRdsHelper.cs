namespace WebApplication1;

public class AwsRdsHelper: IAwsRdsHelper
{
    private readonly IConfiguration config;
    private readonly ILogger<AwsRdsHelper> logger;

    public AwsRdsHelper(IConfiguration config, ILogger<AwsRdsHelper> logger)
    {
        this.config = config;
        this.logger = logger;
    }
    public string GetRdsConnectionString()
    {
        var username = config.GetValue<string>("DB_USERNAME")?? "postgres";
        var password = config.GetValue<string>("DB_PASSWORD")?? "postgres";
        var hostname = config.GetValue<string>("DB_HOST") ?? "localhost";
        var database = config.GetValue<string>("DB_DATABASE") ?? "testapp";
        var port = config.GetValue<string>("DB_PORT") ?? "5432";

        logger.LogInformation($"Created ConnectionString: Host={hostname};Port={port};Database={database};Username={username};Password={password}");
        
        return $"Server={hostname};Port={port};Database={database};User Id={username};Password={password};";
    }
}

public interface IAwsRdsHelper
{
    string GetRdsConnectionString();
}