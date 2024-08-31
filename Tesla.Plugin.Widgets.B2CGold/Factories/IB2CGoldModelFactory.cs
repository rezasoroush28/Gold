using Nop.Core.Domain.Catalog;

using System.Collections.Generic;
using System.Threading.Tasks;

using Tesla.Plugin.Widgets.B2CGold.Areas.Admin.Models;
using Tesla.Plugin.Widgets.B2CGold.Models;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public interface IB2CGoldModelFactory
    {
        Task<B2CGoldSettingsModel> PrepareB2CGoldSettingsModel();
        List<ProductGoldBelongingMappingModel> PrepareB2CGoldBelongingMappingModelsByProductId(int productId);
        ProductGoldBelongingsModel PrepareB2CProductGoldBelongingModelsByProductId(int productId);
        List<GoldIngredientAdminModel> PrepareB2CGoldIngredientModelsByProductId(int id);
        List<GoldIngredientSpecificationAdminModel> PrepareSpecificationModelsByIngredienttId(int id);
        List<GoldIngredientModel> PrepareB2CGoldIngredientViewModelsByProductId(int id);
    }
}
