using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public class MythicLore
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Mythic Lore",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.mythicLoreID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "MythicLore",
                Description = "Enables you to Use and Unleash mythic relic effects.",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,

                Cooldown = 0,
                StaminaCost = 0,
                ManaCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            return skill;
        }
    }
}
