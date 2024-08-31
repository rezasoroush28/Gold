using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Services.Events;

using System.Collections.Generic;
using System.Linq;

using Tesla.Plugin.Widgets.B2CGold;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldBelongingService : IGoldBelongingService
    {
        #region Fields

        private readonly IRepository<GoldBelonging> _goldBelongingRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public GoldBelongingService(IRepository<GoldBelonging> goldBelongingRepository,
            IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext)
        {
            _goldBelongingRepository = goldBelongingRepository;
            _eventPublisher = eventPublisher;
            _staticCacheManager = staticCacheManager;
            _workContext = workContext;
        }

        #endregion

        #region Methods

        public decimal GetGoldProductBelongingPrice(decimal goldProductWeight, decimal totalCalulatedWeight, GoldProductBelongingCalculation goldProductBelonging, decimal goldRealtimePrice)
        {
            if (goldProductBelonging != null)
            {
                switch (goldProductBelonging.GoldBelongingCalculationType)
                {
                    case GoldBelongingCalculationType.SolidPrice:
                        return goldProductBelonging.Value;

                    case GoldBelongingCalculationType.BaseProductGoldPrice:
                        return (goldProductWeight * goldRealtimePrice) * (goldProductBelonging.Value / 100);

                    case GoldBelongingCalculationType.GoldFinalPrice:
                        return (totalCalulatedWeight * goldRealtimePrice) * (goldProductBelonging.Value / 100);
                }
            }

            return 0;
        }

        public void DeleteGoldBelonging(GoldBelonging goldBelonging)
        {
            _goldBelongingRepository.Delete(goldBelonging);
        }

        public List<GoldBelonging> GetAllGoldBelongings()
        {
            return _goldBelongingRepository.TableNoTracking.Select(c => c).ToList();
        }

        public GoldBelonging GetGoldBelongingById(int belongingId)
        {
            return _goldBelongingRepository.TableNoTracking.SingleOrDefault(c => c.Id == belongingId);
        }

        public void InsertGoldBelonging(GoldBelonging goldBelonging)
        {
            _goldBelongingRepository.Insert(goldBelonging);
        }

        public void UpdateGoldBelonging(GoldBelonging goldBelonging)
        {
            _goldBelongingRepository.Update(goldBelonging);
        }

        public bool CheckName(GoldBelonging goldBelonging)
        {
            var names = _goldBelongingRepository.Table.Where(g => g.Id != goldBelonging.Id).Select(g => g.Name).ToList();
            var condition = names.Contains(goldBelonging.Name);
            if (!condition)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
