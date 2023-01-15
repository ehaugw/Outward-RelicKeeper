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
                Target_ItemID = IDs.basicRelicID,
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
                    Prefab_Name = "basic_relic_Prefab",
                    Prefab_AssetBundle = "basic_relic",
                    Prefab_SLPack = RelicKeeper.ModFolderName,
                    Rotation = new Vector3(90, 0, 0),
                    Position = new Vector3(0, -0.093f, 0),

                    
                },
            };
            myitem.ApplyTemplate();
            var item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Equipment;
            item.BagCategorySlot = Item.BagCategorySlotType.Lantern;
            item.IKType = Equipment.IKMode.None;
            //item.LitStatus = Item.Lit.Unlightable;

            return item as Item;
        }
    }
}

[HarmonyPatch(typeof(Item), "GetCastSheathRequired")]
public class Item_GetCastSheathRequired
{
    [HarmonyPostfix]
    public static void Postfix(Item __instance, ref int __result)
    {
        if ((__instance.ItemID == IDs.tormentID /*|| __instance.ItemID == IDs.ruptureID*/) && (__instance.OwnerCharacter?.Inventory?.Equipment?.ItemEquippedCount(IDs.woodooCharmID) ?? 0) > 0)
        {
            __result = 0;
        }
    }
}
