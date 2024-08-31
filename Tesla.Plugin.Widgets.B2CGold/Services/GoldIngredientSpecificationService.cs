using Nop.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldIngredientSpecificationService : IGoldIngredientSpecificationService
    {
        #region Field

        private readonly IRepository<GoldIngredientSpecification> _goldIngredientSpecificationRepository;

        #endregion

        #region Methods

        public GoldIngredientSpecificationService(IRepository<GoldIngredientSpecification> goldIngredientSpecificationRepository)
        {
            _goldIngredientSpecificationRepository = goldIngredientSpecificationRepository;
        }

        public void DeleteGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification)
        {
            _goldIngredientSpecificationRepository.Delete(goldIngredientSpecification);
        }

        public List<GoldIngredientSpecification> GetAllGoldIngredientSpecificationByIngrdientId(int id)
        {
            return _goldIngredientSpecificationRepository.TableNoTracking.Where(c => c.GoldIngredientId == id).ToList();
        }

        public GoldIngredientSpecification GetGoldIngredientSpecificationById(int id)
        {
            return _goldIngredientSpecificationRepository.TableNoTracking.SingleOrDefault(c => c.Id == id);
        }

        public List<GoldIngredientSpecification> GetIngredientSpecificationsByIngredientId(int ingredientId)
        {
            var specifications = _goldIngredientSpecificationRepository.TableNoTracking.Where(c => c.GoldIngredientId == ingredientId).ToList();
            return specifications;
        }

        public void InsertGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification)
        {
            _goldIngredientSpecificationRepository.Insert(goldIngredientSpecification);
        }

        public void UpdateGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification)
        {
            _goldIngredientSpecificationRepository.Update(goldIngredientSpecification);
        }

        #endregion
    }
    
}
