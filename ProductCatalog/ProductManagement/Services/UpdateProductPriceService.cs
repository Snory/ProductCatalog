
using ProductCatalog.API.ProductManagement.Facades;

namespace ProductCatalog.API.ProductManagement.Services
{
    public class UpdateProductPriceService : BackgroundService
    {
        private int _internalInSeconds;
        private ILogger<UpdateProductPriceService> _logger;
        public UpdateProductPriceService(ILogger<UpdateProductPriceService> logger, IServiceProvider services)
        {
            Services = services;
            _internalInSeconds = 60;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Update price service is running");

                using (var scope = Services.CreateScope())
                {
                    var scopedProductFacade =
                        scope.ServiceProvider
                            .GetRequiredService<IProductManagementFacade>();

                    await scopedProductFacade.RefreshPrices(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(_internalInSeconds), stoppingToken);
            }
        }
    }
}
