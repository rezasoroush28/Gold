using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IProductGoldBelongingMappingService
    {
        List<ProductGoldBelongingMapping> GetAllProductGoldBelongingMapping();
        void InsertProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping);
        void DeleteProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping);
        void UpdateProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping);
        List<ProductGoldBelongingMapping> GetProductGoldBelongingMappingByProductId(int ingredientId);
        ProductGoldBelongingMapping GetProductGoldBelongingMappingById(int id);
    }
}