using System.Collections.Generic;
using Tesla.Plugin.Widgets.B2CGold.Domain;

namespace Tesla.Plugin.Widgets.Gold.Services
{
    public interface IGoldIngredientSpecificationService
    {
        List<GoldIngredientSpecification> GetAllGoldIngredientSpecificationByIngrdientId(int id);

        void InsertGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification);

        void DeleteGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification);

        void UpdateGoldIngredientSpecification(GoldIngredientSpecification goldIngredientSpecification);

        List<GoldIngredientSpecification> GetIngredientSpecificationsByIngredientId(int ingredientId);

        GoldIngredientSpecification GetGoldIngredientSpecificationById(int id);
    }
}