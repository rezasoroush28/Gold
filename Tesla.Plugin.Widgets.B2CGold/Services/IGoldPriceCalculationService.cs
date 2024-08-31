using Nop.Core.Domain.Catalog;
using System.Collections.Generic;

using Tesla.Plugin.Widgets.B2CGold.Domain;
using Tesla.Plugin.Widgets.B2CGold.Models;

namespace Tesla.Plugin.Widgets.B2CGold.CalculationFormula
{
    public interface IGoldPriceCalculationService
    {
        GoldCalulatedInfoModel GetGoldCalulatedInfoModel(decimal goldProductWeight, GoldPriceInfo goldPriceInfo, GoldProductBelongingCalculation goldProductBelonging, decimal goldRealTimePrice , bool availablePreOrder = false);
        decimal GetProductBelongingPrice(decimal tottalWeight, int productId, decimal weightToPriceTransformer);
        decimal GetGoldWeight(Product product, IList<ProductAttributeValue> attributeValues);
    }
}