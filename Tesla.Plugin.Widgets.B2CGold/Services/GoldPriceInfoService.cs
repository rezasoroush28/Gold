using Nop.Core.Caching;
using Nop.Core;
using Nop.Services.Events;
using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Nop.Core.Data;
using System.Linq;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public class GoldPriceInfoService : IGoldPriceInfoService
    {
        #region Fields

        private readonly IRepository<GoldPriceInfo> _goldInfoRepository;

        #endregion

        #region Ctor

        public GoldPriceInfoService(IRepository<GoldPriceInfo> goldInfoRepository)
        {
            _goldInfoRepository = goldInfoRepository;
        }

        #endregion

        #region Methods

        public List<GoldPriceInfo> GetAllGoldPriceInfo()
        {
            return _goldInfoRepository.Table.ToList();
        }

        public void DeleteGoldPriceInfo(GoldPriceInfo productGoldInfo)
        {
            _goldInfoRepository.Delete(productGoldInfo);
        }

        public GoldPriceInfo GetGoldPriceInfoById(int id)
        {
            return _goldInfoRepository.Table.FirstOrDefault(x => id == x.Id);
        }

        public void InsertGoldPriceInfo(GoldPriceInfo productGoldInfo)
        {
            _goldInfoRepository.Insert(productGoldInfo);
        }

        public void UpdateGoldPriceInfo(GoldPriceInfo productGoldInfo)
        {
            _goldInfoRepository.Update(productGoldInfo);
        }

        public GoldPriceInfo GetGoldPriceInfoByProductId(int id)
        {
            var info =  _goldInfoRepository.TableNoTracking.FirstOrDefault<GoldPriceInfo>(x => x.ProductId == id);
            return info;
        }

        #endregion

    }
}