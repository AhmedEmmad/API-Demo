using AutoMapper;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.Product;
using Store.Service.Services.Product.Dtos;

namespace Store.Service.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllBrandsAsync()
        {
            var brands = await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync();

            //IEnumerable<BrandTypeDetailsDto> mappedBrands = brands.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt
            //});

            var mappedBrands = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(brands);

            return mappedBrands;
        }

        public async Task<PaginatedResultDto<ProductDetailsDto>> GetAllProductsAsync(ProductSpecification spcs)
        {
            var specs = new ProductWithSpecifications(spcs);
            var products = await _unitOfWork.Repository<Store.Data.Entities.Product, int>().GetAllWithSpecificationAsync(specs);

            //var mappedProducts = products.Select(x => new ProductDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    Description = x.Description,
            //    Price = x.Price,
            //    PictureUrl = x.PictureUrl,
            //    BrandName = x.Brand.Name,
            //    TypeName = x.Type.Name,
            //    CreatedAt = x.CreatedAt
            //});

            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDetailsDto>>(products);

            return new PaginatedResultDto<ProductDetailsDto>(spcs.PageIndex, spcs.PageSize, products.Count, mappedProducts);
        }

        public async Task<IReadOnlyList<BrandTypeDetailsDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();

            //var mappedTypes = types.Select(x => new BrandTypeDetailsDto
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    CreatedAt = x.CreatedAt
            //});

            var mappedTypes = _mapper.Map<IReadOnlyList<BrandTypeDetailsDto>>(types);

            return mappedTypes;
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int? productId)
        {
            if (productId is null)
            {
                throw new Exception("Product ID is null");
            }

            var specs = new ProductWithSpecifications(productId);

            var product = await _unitOfWork.Repository<Store.Data.Entities.Product, int>().GetWithSpecificationByIdAsync(specs);

            if (product is null)
            {
                throw new Exception("Product not found");
            }

            //var mappedProduct = new ProductDetailsDto
            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    Description = product.Description,
            //    CreatedAt = product.CreatedAt,
            //    Price = product.Price,
            //    PictureUrl = product.PictureUrl,
            //    BrandName = product.Brand.Name,
            //    TypeName = product.Type.Name
            //};

            var mappedProduct = _mapper.Map<ProductDetailsDto>(product);

            return mappedProduct;
        }
    }
}
