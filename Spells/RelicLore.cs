﻿using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    using RelicCondition;

    public class RelicLore
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = RelicCondition.RelicLoreLevel1Name,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.relicLoreID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "RelicLore",
                Description = "Enables you to Use and Unleash stronger relic effects.",
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
