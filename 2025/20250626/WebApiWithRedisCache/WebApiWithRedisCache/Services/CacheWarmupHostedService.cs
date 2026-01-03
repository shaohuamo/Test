namespace WebApiWithRedisCache.Services
{
    //IHostedService 是 .NET Core 中用于实现后台任务的接口，
    //它允许开发者在应用程序生命周期中启动和停止长时间运行的后台服务。
    //IHostedService 的派生类会在应用程序启动时自动启动，并在应用程序关闭时自动停止
    public class CacheWarmupHostedService : IHostedService
    {
        private readonly CacheWarmupService _warmupService;
        public CacheWarmupHostedService(CacheWarmupService warmupService)
            => _warmupService = warmupService;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // 异步预热，不阻塞应用启动
            return Task.Run(_warmupService.WarmUpAsync, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
