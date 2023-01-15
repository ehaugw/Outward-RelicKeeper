using System.Collections.Generic;
using TinyHelper;

namespace RelicKeeper
{
    using InstanceIDs;
    using SideLoader;
    using EffectSourceConditions;

    public class AlphaTuanosaurTrinket
    {
        public const string SubfolderName = "AlphaTuanosaurTrinket";
        public const string ItemName = "Alpha Tuanosaur Trinket";

        public static Item MakeItem()
        {

            var myitem = new SL_Item()
            {
                Name = ItemName,
                Target_ItemID = IDs.basicRelicID,
                New_ItemID = IDs.alphaTuanosaurTrinketID,
                EffectBehaviour = EditBehaviours.Destroy,
                Description = "A trinket made from a tail bone of an apex predator. Causes the cooldown of Wrathful Smite to reset if used on a prone target.",
                StatsHolder = new SL_ItemStats()
                {
                    MaxDurability = 100,
                    BaseValue = 300
                },
                BehaviorOnNoDurability = Item.BehaviorOnNoDurabilityType.Destroy,
                RepairedInRest = false,

                Tags = TinyTagManager.GetOrMakeTags(new string[]
                {
                    IDs.TrinketTag,
                    IDs.HandsFreeTag,
                    IDs.RelicTag,
                    IDs.ItemTag,
                }),
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = SubfolderName,
                ItemVisuals = new SL_ItemVisual()
                {
                    Prefab_Name = "basic_relic_Prefab",
                    Prefab_AssetBundle = "basic_relic",
                    Prefab_SLPack = RelicKeeper.ModFolderName,
                }
            };
            myitem.ApplyTemplate();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Equipment;
            item.IKType = Equipment.IKMode.None;

            var skill = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.wrathfulSmiteID);

            var hitEffects = TinyGameObjectManager.MakeFreshObject("HitEffects", true, true, skill.transform);
            var cooldownChanger = hitEffects.AddComponent<CooldownChangeEffect>();
            cooldownChanger.HitKnockbackCooldown = 0;

            var requirementTransform = TinyGameObjectManager.GetOrMake(hitEffects.transform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionEquipment>();
            skillReq.RequiredItemID = IDs.alphaTuanosaurTrinketID;


            return item as Item;
        }

        public static Item MakeRecipe()
        {
            string newUID = RelicKeeper.GUID + "." + SubfolderName.ToLower() + "recipe";
            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Survival,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.alphaTuanosaurTrinketID },
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.linenClothID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.palladiumScrapsID },
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.alphaTuanosaurTailID}
                },
                UID = newUID,
            }.ApplyTemplate();

            var myitem = new SL_RecipeItem()
            {
                Name = "Crafting: " + ItemName,
                Target_ItemID = IDs.arbitrarySurvivalRecipeID,
                New_ItemID = IDs.alphaTuanosaurTrinketRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.ApplyTemplate();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            return item;
        }
    }
}
