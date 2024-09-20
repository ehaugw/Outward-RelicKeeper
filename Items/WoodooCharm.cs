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

    public class WoodooCharm
    {
        public const string SubfolderName = "WoodooCharm";
        public const string ItemName = "Woodoo Charm";

        public static Item MakeItem()
        {

            var myitem = new SL_Item()
            {
                Name = ItemName,
                Target_ItemID = IDs.arbitraryTrinketID,
                New_ItemID = IDs.woodooCharmID,
                EffectBehaviour = EditBehaviours.Destroy,
                Description = "Woodoo Charm for Woodooing uncharming creatures.",
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
                    Prefab_Name = "woodoo_charm_Prefab",
                    Prefab_AssetBundle = "woodoo_charm",
                    Prefab_SLPack = RelicKeeper.ModFolderName,
                    Rotation = new Vector3(-90, 0, 90),
                    Position = new Vector3(0, -0.093f, 0),

                    
                },
            };
            myitem.ApplyTemplate();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Equipment;
            item.BagCategorySlot = Item.BagCategorySlotType.Lantern;
            item.IKType = Equipment.IKMode.Lantern;
            //item.LitStatus = Item.Lit.Unlightable;

            return item as Item;
        }
    }
}
