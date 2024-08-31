using System;

using Nop.Core;
using Nop.Core.Data;
using Nop.Services.Events;

using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    /// <summary>
    /// GoldContactInfo service
    /// </summary>
    public partial class GoldContactInfoService : IGoldContactInfoService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<GoldContactInfo> _goldContactInfoRepository;

        #endregion

        #region Ctor

        public GoldContactInfoService(IEventPublisher eventPublisher,
            IRepository<GoldContactInfo> GoldContactInfoRepository)
        {
            _eventPublisher = eventPublisher;
            _goldContactInfoRepository = GoldContactInfoRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="goldContactInfoName">GoldContactInfo name</param>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<GoldContactInfo> GetAllGoldContactInfo(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _goldContactInfoRepository.Table;
            return new PagedList<GoldContactInfo>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets a GoldContactInfo
        /// </summary>
        /// <param name="goldContactInfoId">GoldContactInfo identifier</param>
        /// <returns>GoldContactInfo</returns>
        public virtual GoldContactInfo GetGoldContactInfoById(int goldContactInfoId)
        {
            if (goldContactInfoId == 0)
                return null;

            return _goldContactInfoRepository.GetById(goldContactInfoId);
            //var key = string.Format(NopCatalogDefaults.CategoriesByIdCacheKey, GoldContactInfoId);
            //return _cacheManager.Get(key, () => _goldContactInfoRepository.GetById(GoldContactInfoId));
        }


        /// <summary>
        /// Inserts GoldContactInfo
        /// </summary>
        /// <param name="goldContactInfo">GoldContactInfo</param>
        public virtual void InsertGoldContactInfo(GoldContactInfo goldContactInfo)
        {
            if (goldContactInfo == null)
                throw new ArgumentNullException(nameof(goldContactInfo));

            _goldContactInfoRepository.Insert(goldContactInfo);

            //cache
            //_cacheManager.RemoveByPrefix(NopCatalogDefaults.CategoriesPrefixCacheKey);
            //_staticCacheManager.RemoveByPrefix(NopCatalogDefaults.CategoriesPrefixCacheKey);
            //_cacheManager.RemoveByPrefix(NopCatalogDefaults.ProductCategoriesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityInserted(goldContactInfo);
        }

        /// <summary>
        /// Updates the GoldContactInfo
        /// </summary>
        /// <param name="GoldContactInfo">GoldContactInfo</param>
        public virtual void UpdateGoldContactInfo(GoldContactInfo goldContactInfo)
        {
            if (goldContactInfo == null)
                throw new ArgumentNullException(nameof(goldContactInfo));

            _goldContactInfoRepository.Update(goldContactInfo);

            //cache
            //_cacheManager.RemoveByPrefix(NopCatalogDefaults.CategoriesPrefixCacheKey);
            //_staticCacheManager.RemoveByPrefix(NopCatalogDefaults.CategoriesPrefixCacheKey);
            //_cacheManager.RemoveByPrefix(NopCatalogDefaults.ProductCategoriesPrefixCacheKey);

            //event notification
            _eventPublisher.EntityUpdated(goldContactInfo);
        }

        /// <summary>
        /// Delete GoldContactInfo
        /// </summary>
        /// <param name="goldContactInfo">GoldContactInfo</param>
        public virtual void DeleteGoldContactInfo(GoldContactInfo goldContactInfo)
        {
            if (goldContactInfo == null)
                throw new ArgumentNullException(nameof(goldContactInfo));

            _goldContactInfoRepository.Delete(goldContactInfo);
            //event notification
            _eventPublisher.EntityDeleted(goldContactInfo);
        }

        #endregion
    }

}