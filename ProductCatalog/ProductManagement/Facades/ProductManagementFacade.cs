using AutoMapper;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.ApiModel.ProductManagement.WriteModels;
using ProductCatalog.Core.ProductCatalogs.ValueObjects;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.Factories;
using ProductCatalog.Core.ProductManagement.Repositories;
using ProductCatalog.Core.ProductManagement.ValueObjects;
using ProductCatalog.Core.Shared;
using ProductCatalog.Persistence.ProductManagement.Repositories;
using ProductCatalog.Persistence.Utils;

namespace ProductCatalog.API.ProductManagement.Facades
{
    public class ProductManagementFacade : IProductManagementFacade
    {
        private readonly IProductAggregateFactory _productAggregateFactory;
        private readonly IProductAggregateRepository<ProductAggregate> _productAggregateRepository;
        private readonly IProductQuery _productQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductManagementFacade> _logger;

        public ProductManagementFacade(
                IProductAggregateFactory productAggregateFactory,
                IProductAggregateRepository<ProductAggregate> productAggregateRepository,
                IProductQuery productQuery,
                IMapper mapper,
                ILogger<ProductManagementFacade> logger
            )
        {
            _productAggregateFactory = productAggregateFactory;
            _productAggregateRepository = productAggregateRepository;
            _productQuery = productQuery;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateProductFrom(CreateProductInput input)
        {
            var quantity = new Quantity(input.Quantity);
            var productvalue = new MonetaryValue(CurrencyCode.FromCode(input.CurrencyCode), input.Price);

            var product = _productAggregateFactory.CreateProductAggregateFrom(quantity, productvalue, input.Ean, input.Description);

            await _productAggregateRepository.Add(product);
            await _productAggregateRepository.Save();

            return product.Id.Value;
        }

        public async Task UpdateProduct(UpdateProductInput input)
        {
            var aggregate = _productAggregateRepository.GetById(ProductId.Create(input.ProductId)).Result;

            if (aggregate == null)
            {
                throw new ArgumentException("Product does not exists");
            }

            var quantity = new Quantity(input.Quantity);
            var productvalue = new MonetaryValue(CurrencyCode.FromCode(input.CurrencyCode), input.Price);

            aggregate.UpdateProduct(input.Ean, input.Description, quantity, productvalue);
            await _productAggregateRepository.Save();
        }

        public async Task AddCategory(AddCategoryInput input)
        {
            var aggregate = _productAggregateRepository.GetById(ProductId.Create(input.ProductId)).Result;

            if (aggregate == null)
            {
                throw new ArgumentException("Product does not exists");
            }

            aggregate.AddCategoryId(CategoryId.Create(input.CategoryId));
            await _productAggregateRepository.Save();
        }

        public async Task RemoveCategory(RemoveCategoryInput input)
        {
            var aggregate = _productAggregateRepository.GetById(ProductId.Create(input.ProductId)).Result;

            if (aggregate == null)
            {
                throw new ArgumentException("Product does not exists");
            }

            aggregate.RemoveCategory(CategoryId.Create(input.CategoryId));
            await _productAggregateRepository.Save();
        }

        public async Task<(IEnumerable<ProductViewOut>, PaginationMetadata)> GetAllProducts(string? categoryFilter = null, int pageNumber = 1, int pageSize = 20)
        {
            var (collection, paginatonMetaData) = await _productQuery.GetAll(categoryFilter,pageNumber, pageSize);

            return (_mapper.Map<IEnumerable<ProductViewOut>>(collection), paginatonMetaData); 
        }

        public async Task<ProductViewOut> GetProductById(int id)
        {
            var product = await _productQuery.GetProductById(id);

            return (_mapper.Map<ProductViewOut>(product));
        }

        public async Task RefreshPrices(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Refreshing prices");

            var outDatedAggregates = await _productAggregateRepository.GetOutdatedProducts();

            if(outDatedAggregates.Count() == 0)
            {
                return;
            }

            foreach (var product in outDatedAggregates)
            {
                var newPrice = new MonetaryValue(product.ProductPrice.CurrencyCode, product.ProductPrice.Price * 0.8m);

                product.ChangeProductPrice(newPrice);
            }

            await _productAggregateRepository.Save();
        }
    }
}
