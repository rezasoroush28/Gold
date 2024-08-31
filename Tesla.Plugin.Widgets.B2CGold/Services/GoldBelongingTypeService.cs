using MoreLinq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using System.Collections.Generic;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldBelongingTypeService : IGoldBelongingTypeService
    {
        #region Fields

        private readonly IRepository<GoldBelongingType> _goldBelongingTypeRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public GoldBelongingTypeService(IRepository<GoldBelongingType> goldBelongingTypeRepository,
            IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _goldBelongingTypeRepository = goldBelongingTypeRepository;
            _eventPublisher = eventPublisher;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }
       
        #endregion

        #region Methods

        public void DeleteBelongingType(GoldBelongingType goldBelongingType)
        {
            _goldBelongingTypeRepository.Delete(goldBelongingType);
        }

        public void InsertBelongingType(GoldBelongingType goldBelongingType)
        {
            _goldBelongingTypeRepository.Insert(goldBelongingType);
        }

        public void UpdateBelongingType(GoldBelongingType goldBelongingType)
        {
            _goldBelongingTypeRepository.Update(goldBelongingType);
        }

        public List<GoldBelongingType> GetAllGoldBelongingTypes()
        {
            return _goldBelongingTypeRepository.TableNoTracking.Select(x => x).ToList();
        }

        public bool CheckName(GoldBelongingType goldBelongingType)
        {
            var names = _goldBelongingTypeRepository.Table.Where(g => g.Id != goldBelongingType.Id).Select(g => g.Name).ToList();
            var condition = names.Contains(goldBelongingType.Name);
            if (!condition)
            {
                return true;
            }
            return false;
        }

        public GoldBelongingType GetBelongingTypeById(int id)
        {
            return _goldBelongingTypeRepository.Table.SingleOrDefault(c => c.Id == id);
        }

        #endregion
    }
}
