using Store.Service.Services.Product.Dtos;

namespace Store.Service.Services.Product
{
    public class PaginatedResultDto<T>
    {
        private int pageIndex;
        private int pageSize;
        private int count;
        private IReadOnlyList<ProductDetailsDto> mappedProducts;

        public PaginatedResultDto(int pageIndex, int pageSize, int count, IReadOnlyList<ProductDetailsDto> mappedProducts)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.count = count;
            this.mappedProducts = mappedProducts;
        }
    }
}