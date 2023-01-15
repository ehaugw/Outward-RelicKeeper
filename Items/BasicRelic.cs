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
    using UnityEngine;

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
                    MaxDurability = 20,
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
                    Rotation = new Vector3(-90, 0, 0),
                    Position = new Vector3(0, -0.093f, 0),
                },
            };
            myitem.ApplyTemplate();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Equipment;
            item.BagCategorySlot = Item.BagCategorySlotType.Lantern;
            item.IKType = Equipment.IKMode.None;

            return item as Item;
        }
    }
}