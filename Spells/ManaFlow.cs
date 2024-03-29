﻿using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public class ManaFlow
    {
        public static float ImbueDuration = 10;

        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Mana Flow",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.manaFlowID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "ManaFlow",
                Description = "Equipped relics grant you mana cost reduction.",
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

    [HarmonyPatch(typeof(CharacterEquipment), "GetTotalManaUseModifier")]
    public class CharacterEquipment_GetTotalManaUseModifier
    {
        [HarmonyPostfix]
        public static void Postfix(ref float __result, Character ___m_character)
        {
            if (___m_character is Character character && SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID))
            {
                __result -= 0.05f * RelicBehavior.GetEquippedRelics(character).Count;
            }
        }
    }
}
