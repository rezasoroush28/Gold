using Nop.Core.Domain.Orders;
using Nop.Web.Models.Order;

using Tesla.Plugin.Widgets.B2CGold.Models;
using Tesla.Plugin.Widgets.B2CGold.Models.GoldProposalValue;

namespace Tesla.Plugin.Widgets.B2CGold.Factories
{
    public interface IGoldOrderDetailsModelFactory
    {
        OrderDetailsGoldModel PrepareOrderDetailsGoldModel(OrderDetailsModel orderDetailsModel,OrderDetailsGoldModel model,Order OrderDomain);
    }
}