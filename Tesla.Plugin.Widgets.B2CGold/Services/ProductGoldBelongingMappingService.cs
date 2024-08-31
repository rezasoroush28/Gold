using Nop.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class ProductGoldBelongingMappingService : IProductGoldBelongingMappingService
    {
        #region Fields

        private readonly IRepository<ProductGoldBelongingMapping> _productGoldBelongingMappingRepository;

        #endregion

        #region Ctor
        public ProductGoldBelongingMappingService(IRepository<ProductGoldBelongingMapping> productGoldBelongingMappingRepository)
        {
            _productGoldBelongingMappingRepository = productGoldBelongingMappingRepository;
        } 

        #endregion

        #region Methods

        public void DeleteProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping)
        {
            _productGoldBelongingMappingRepository.Delete(ProductGoldBelongingMapping);
        }

        public List<ProductGoldBelongingMapping> GetAllProductGoldBelongingMapping()
        {
            return _productGoldBelongingMappingRepository.TableNoTracking.Select(c => c).ToList();
        }

        public List<ProductGoldBelongingMapping> GetProductGoldBelongingMappingByProductId(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public ProductGoldBelongingMapping GetProductGoldBelongingMappingById(int id)
        {
            return _productGoldBelongingMappingRepository.Table.SingleOrDefault(c => c.Id == id);
        }

        public void InsertProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping)
        {
            _productGoldBelongingMappingRepository.Insert(ProductGoldBelongingMapping);
        }

        public void UpdateProductGoldBelongingMapping(ProductGoldBelongingMapping ProductGoldBelongingMapping)
        {
            _productGoldBelongingMappingRepository.Update(ProductGoldBelongingMapping);
        }

        #endregion
    }

}
