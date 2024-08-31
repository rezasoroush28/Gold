using System;
using System.Linq;

using Nop.Core;
using Nop.Core.Data;
using Nop.Core.ResponseModel;
using Nop.Services.Events;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models.GoldProposalValue;
using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Services;

namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    /// <summary>
    /// GoldProposalValue service
    /// </summary>
    public partial class GoldProposalValueService : IGoldProposalValueService
    {
        #region Fields

        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<GoldProposalValue> _goldProposalValueRepository;

        #endregion

        #region Ctor

        public GoldProposalValueService(IEventPublisher eventPublisher,
            IRepository<GoldProposalValue> goldProposalValueRepository)
        {
            _eventPublisher = eventPublisher;
            _goldProposalValueRepository = goldProposalValueRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="goldProposalValueName">GoldProposalValue name</param>
        /// <param name="storeId">Store identifier; 0 if you want to get all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<GoldProposalValue> GetAllGoldProposalValue(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _goldProposalValueRepository.Table;
            return new PagedList<GoldProposalValue>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets a GoldProposalValue
        /// </summary>
        /// <param name="goldProposalValueId">GoldProposalValue identifier</param>
        /// <returns>GoldProposalValue</returns>
        public virtual GoldProposalValue GetGoldProposalValueById(int goldProposalValueId)
        {
            if (goldProposalValueId == 0)
                return null;

            return _goldProposalValueRepository.GetById(goldProposalValueId);
            //var key = string.Format(NopCatalogDefaults.CategoriesByIdCacheKey, GoldProposalValueId);
            //return _cacheManager.Get(key, () => _goldProposalValueRepository.GetById(GoldProposalValueId));
        }


        /// <summary>
        /// Inserts GoldProposalValue
        /// </summary>
        /// <param name="goldProposalValue">GoldProposalValue</param>
        public virtual GeneralResponseModel InsertGoldProposalValue(GoldProposalValue goldProposalValue)
        {
           
            _goldProposalValueRepository.Insert(goldProposalValue);

            _eventPublisher.EntityInserted(goldProposalValue);

            return (new GeneralResponseModel { Success = true });
        }

        /// <summary>
        /// Updates the GoldProposalValue
        /// </summary>
        /// <param name="GoldProposalValue">GoldProposalValue</param>
        public virtual GeneralResponseModel UpdateGoldProposalValue(GoldProposalValue goldProposalValue)
        {
            if (IsDuplicate(goldProposalValue))
                return (new GeneralResponseModel { Success = false, Error = "goldProposalValue is duplicate", });

            _goldProposalValueRepository.Update(goldProposalValue);

            _eventPublisher.EntityInserted(goldProposalValue);

            return (new GeneralResponseModel { Success = true });
        }

        /// <summary>
        /// Delete GoldProposalValue
        /// </summary>
        /// <param name="goldProposalValue">GoldProposalValue</param>
        public virtual void DeleteGoldProposalValue(GoldProposalValue goldProposalValue)
        {
            if (goldProposalValue == null)
                throw new ArgumentNullException(nameof(goldProposalValue));

            _goldProposalValueRepository.Delete(goldProposalValue);
            //event notification
            _eventPublisher.EntityDeleted(goldProposalValue);
        }

        #endregion

        #region Utilities

        private bool IsDuplicate(GoldProposalValue goldProposalValue)
        {
            return _goldProposalValueRepository.Table.Any(i => i.ValueTitle == goldProposalValue.ValueTitle && i.Id != goldProposalValue.Id);
        }

        #endregion
    }

}