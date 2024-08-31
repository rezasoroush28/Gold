using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;
using System.Collections.Generic;
using System.Linq;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldProductBelongingCalculationService : IGoldProductBelongingCalculationService
    {
        #region Fields

        private readonly IRepository<GoldProductBelongingCalculation> _goldProductBelongingRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public GoldProductBelongingCalculationService(IRepository<GoldProductBelongingCalculation> goldProductBelongingRepository,
            IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _goldProductBelongingRepository = goldProductBelongingRepository;
            _eventPublisher = eventPublisher;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }


        #endregion

        #region Methods
        public GoldProductBelongingCalculation GetGoldProductBelongingCalculationByProductId(int id)
        {
            return _goldProductBelongingRepository.Table.FirstOrDefault(c => c.ProductId == id);
        }

        public void InsertGoldProductBelongingCalculation(GoldProductBelongingCalculation goldProductBelonging)
        {
            _goldProductBelongingRepository.Insert(goldProductBelonging);
        }

        public void UpdateGoldProductBelongingCalculation(GoldProductBelongingCalculation goldProductBelonging)
        {
            _goldProductBelongingRepository.Update(goldProductBelonging);
        }

        #endregion

        #region Utilities


        #endregion

    }
}
