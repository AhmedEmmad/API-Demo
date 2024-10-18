using Microsoft.IdentityModel.Tokens;

namespace Store.Repository.Specification.Product
{
    public class ProductWithSpecifications : BaseSpecification<Store.Data.Entities.Product>
    {
        public ProductWithSpecifications(ProductSpecification spcs)
             : base(product => (!spcs.BrandId.HasValue || product.BrandId == spcs.BrandId.Value) &&
                               (!spcs.TypeId.HasValue || product.TypeId == spcs.TypeId.Value) &&
                               (string.IsNullOrEmpty(spcs.Search) || product.Name.Trim().ToLower().Contains(spcs.Search))
                   )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            AddOrderBy(x => x.Name);

            ApplyPagination(spcs.PageSize * (spcs.PageIndex - 1), spcs.PageSize);

            if (!string.IsNullOrEmpty(spcs.Sort))
            {
                switch (spcs.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithSpecifications(int? id) : base(product => product.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type); 
        }
    }
}
