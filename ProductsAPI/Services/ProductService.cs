using AutoMapper;
using ProductsAPI.DTOs;
using ProductsAPI.Models;
using ProductsAPI.Repository;

namespace ProductsAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategories(int id)
        {
            var product = await _repository.GetByCategories(id);

            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var product = await _repository.GetAll();

            return _mapper.Map<IEnumerable<ProductDTO>>(product);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _repository.Get(id);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task AddProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);

            await _repository.Create(product);

            productDTO.Id = product.Id;

        }

        public async Task DeleteProduct(int id)
        {
            var product = await _repository.Get(id);

            await _repository.Delete(product);
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);

            await _repository.Update(product);
        }
    }
}
