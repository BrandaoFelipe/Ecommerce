using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;
using ProductsAPI.Repository;

namespace ProductsAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var category = await _repository.GetAll();

            return _mapper.Map<IEnumerable<CategoryDTO>>(category);
        }

        public async Task<CategoryDTO> GetCategoryById(int id)
        {
            var category = await _repository.Get(c => c.CategoryId == id);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _repository.Create(category);

            categoryDTO.CategoryId = category.CategoryId;
            
        }        

        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _repository.Update(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _repository.Get(c => c.CategoryId == id);
            
            await _repository.Delete(category);

        }

    }
}
