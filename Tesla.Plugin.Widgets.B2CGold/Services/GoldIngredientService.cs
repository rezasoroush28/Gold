using Microsoft.EntityFrameworkCore;

using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldIngredientService : IGoldIngredientService
    {
        #region Fields

        private readonly IRepository<GoldIngredient> _goldIngredientRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public GoldIngredientService(IRepository<GoldIngredient> GoldIngredientRepository,
            IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _goldIngredientRepository = GoldIngredientRepository;
            _eventPublisher = eventPublisher;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        public void DeleteGoldIngredient(GoldIngredient goldIngredient)
        {
            _goldIngredientRepository.Delete(goldIngredient);
        }

        public List<GoldIngredient> GetAllGoldIngredients()
        {
            return _goldIngredientRepository.TableNoTracking.Select(c => c).ToList();
        }

        public GoldIngredient GetGoldIngredientById(int id)
        {
            return _goldIngredientRepository.TableNoTracking.SingleOrDefault(c => c.Id == id);
        }

        public List<GoldIngredient> GetGoldIngredientByProductId(int productId)
        {
            var entity = _goldIngredientRepository.Table.Where(i => i.ProductId == productId).ToList();
            return entity;
        }

        public void InsertGoldIngredient(GoldIngredient goldIngredient)
        {
            _goldIngredientRepository.Insert(goldIngredient);
        }

        public void UpdateGoldIngredient(GoldIngredient goldIngredient)
        {
            _goldIngredientRepository.Update(goldIngredient);
        }

        #endregion
    }
}
