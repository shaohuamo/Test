using StackExchange.Redis;
using WebApiWithRedisCache.Services;

namespace WebApiWithRedisCache
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //配置Redis缓存

            //Microsoft.Extensions.Caching.StackExchangeRedis
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
                options.InstanceName = "WebApi_";
            });

            //StackExchange.Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configuration = builder.Configuration.GetConnectionString("MyRedisConStr");

                //连接主从集群
                //var configuration = new ConfigurationOptions
                //{
                //    // 主节点和从节点地址
                //    EndPoints =
                //    {
                //        { "master-ip", 6379 },  // Master
                //        { "slave1-ip", 6380 },   // Slave 1
                //        { "slave2-ip", 6381 }    // Slave 2
                //    },
                //    Password = "your_master_password",  // 如果设置了密码
                //    AbortOnConnectFail = false,        // 连接失败时不抛出异常
                //    AllowAdmin = true,                 // 允许执行管理命令（可选）
                //    ConnectRetry = 3,                  // 重试次数
                //    SyncTimeout = 5000                 // 同步超时（毫秒）
                //};

                //连接哨兵集群
                //var sentinelOptions = new ConfigurationOptions
                //{
                //    // 哨兵节点列表
                //    EndPoints =
                //    {
                //        { "sentinel1-ip", 26379 },
                //        { "sentinel2-ip", 26379 },
                //        { "sentinel3-ip", 26379 }
                //    },
                //    TieBreaker = "",  // （可选）禁用自动 Master 选举，强制客户端依赖 外部机制（如哨兵 Sentinel） 来获取当前 Master 地址。
                //    ServiceName = "mymaster",  // 哨兵中配置的主节点名称
                //    Password = "your_master_password",  // 如果 Master 有密码
                //    CommandMap = CommandMap.Sentinel,  // 启用 Sentinel 模式
                //    AbortOnConnectFail = false,
                //    ConnectRetry = 3
                //};
                //return ConnectionMultiplexer.Connect(sentinelOptions!);

                //配置分片集群
                //var configurationOptions = new ConfigurationOptions
                //{
                //    EndPoints =                 
                //    {
                //        "192.168.1.100:6379", // 主节点1
                //        "192.168.1.101:6379", // 主节点2
                //        "192.168.1.102:6379"  // 主节点3
                //    },
                //    Password = "your_cluster_password", // 如果集群有密码
                //    AbortOnConnectFail = false,
                //    ConnectRetry = 3,
                //    // 关键配置：启用集群模式
                //    CommandMap = CommandMap.Default, // 或 CommandMap.Cluster
                //    DefaultVersion = new Version(6, 0) // Redis 6.0+ 集群
                //};
                //return ConnectionMultiplexer.Connect(configurationOptions!);

                //读写分离代码
                // 写入操作会自动路由到当前 Master
                //var connection = ConnectionMultiplexer.Connect(sentinelOptions);
                //var db = connection.GetDatabase();
                ////写入操作会自动路由到当前 Master
                //db.StringSet("key", "value");
                //// 读取操作可以通过CommandFlags.PreferReplica指定优先从 Slave 读取
                //var value = db.StringGet("key", CommandFlags.PreferReplica);

                return ConnectionMultiplexer.Connect(configuration!);
            });

            builder.Services.AddScoped<SmsCodeService>();
            builder.Services.AddScoped<SignInService>();
            builder.Services.AddScoped<FollowService>();
            builder.Services.AddScoped<FeedService>();
            //AddHostedService注册的服务是Singleton
            builder.Services.AddHostedService<CacheWarmupHostedService>();
            builder.Services.AddSingleton<CacheWarmupService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
