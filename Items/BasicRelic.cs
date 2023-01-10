using HarmonyLib;
using InstanceIDs;
using System.Collections.Generic;
using TinyHelper;

namespace RelicKeeper
{
    using InstanceIDs;
    using SideLoader;
    using EffectSourceConditions;
    using System;

    public class BasicRelic
    {
        public const string SubfolderName = "BasicRelic";
        public const string ItemName = "Basic Relic";

        public static Item MakeItem()
        {

            var myitem = new SL_Item()
            {
                Name = ItemName,
                Target_ItemID = IDs.arbitraryTrinketID,
                New_ItemID = IDs.basicRelicID,
                EffectBehaviour = EditBehaviours.Destroy,
                Description = "",
                StatsHolder = new SL_ItemStats()
                {
                    MaxDurability = 100,
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

            return item as Item;
        }
    }
}