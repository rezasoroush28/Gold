using System.Collections.Generic;
using System.Threading.Tasks;

using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldIngredientService
    {
        void InsertGoldIngredient(GoldIngredient goldIngredient);
        void DeleteGoldIngredient(GoldIngredient goldIngredient);
        void UpdateGoldIngredient(GoldIngredient goldIngredient);
        GoldIngredient GetGoldIngredientById(int id);
        List<GoldIngredient> GetGoldIngredientByProductId(int productId);
        List<GoldIngredient> GetAllGoldIngredients();
        
    }
}