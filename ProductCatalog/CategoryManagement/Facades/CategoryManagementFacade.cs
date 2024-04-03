using AutoMapper;
using ProductCatalog.ApiModel.CategoryManagement.WriteModel;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.Core.CategoryManagement.Factories;
using ProductCatalog.Core.CategoryManagement.Repositories;
using ProductCatalog.Persistence.CategoryManagement.Queries;
using ProductCatalog.Persistence.Utils;

namespace ProductCatalog.API.CategoryManagement.Facades
{
    public class CategoryManagementFacade : ICategoryManagementFacade
    {
        private readonly ICategoryAggregateRepository _categoryAggregateRepository;
        private readonly ICategoryAggregateFactory _categoryAggregateFactory;
        private readonly ICategoryQuery _categoryQuery;
        private readonly IMapper _mapper;

        public CategoryManagementFacade(
                ICategoryAggregateRepository categoryAggregateRepository, 
                ICategoryAggregateFactory categoryAggregateFactory,
                ICategoryQuery categoryQuery,
                IMapper mapper
            )
        {
            _categoryAggregateRepository = categoryAggregateRepository;
            _categoryAggregateFactory = categoryAggregateFactory;
            _categoryQuery = categoryQuery;
            _mapper = mapper;
        }

        public async Task<int> CreateCategoryFrom(CreateCategoryInput categoryInput)
        {
            var aggregate = _categoryAggregateFactory.CreateCategoryAggregateFrom(categoryInput.CategoryName);
            await _categoryAggregateRepository.Add(aggregate);
            await _categoryAggregateRepository.Save();

            return aggregate.Id.Value;
        }

        public async Task<CategoryViewOut> GetCategoryById(int id)
        {
            var category = await _categoryQuery.GetByCategoryId(id);

            return _mapper.Map<CategoryViewOut>(category); 
        }

        public async Task<(IEnumerable<CategoryViewOut>, PaginationMetadata)> GetAllCategories(int pageNumber = 1, int pageSize = 20)
        {

            var (collection, paginatonMetaData) = await _categoryQuery.GetAll(pageNumber, pageSize);

            return (_mapper.Map<IEnumerable<CategoryViewOut>>(collection), paginatonMetaData);
             
        }
    }
}
