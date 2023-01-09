using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
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
        public static void Postfix(CharacterEquipment __instance, ref float __result)
        {
            if (SideLoader.At.GetField<CharacterEquipment>(__instance, "m_character") is Character character && SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID))
            {
                if (character.LeftHandEquipment?.HasTag(TinyTagManager.GetOrMakeTag(IDs.RelicTag)) ?? false)
                {
                    if (__result > 1.10f)
                    {
                        __result -= 0.10f;
                    }
                    else if (__result < 1.05f)
                    {
                        __result -= 0.05f;
                    }
                    else
                    {
                        __result = 1;
                    }
                }
            }
        }
    }

    //[HarmonyPatch(typeof(Weapon), "AddImbueEffect")]
    //public class Weapon_AddImbueEffect
    //{
    //    [HarmonyPrefix]
    //    public static void Prefix(Weapon __instance, ref ImbueEffectPreset _effect)
    //    {
    //        var skillKnowledge = __instance?.OwnerCharacter?.Inventory?.SkillKnowledge;
    //        if (_effect.PresetID == IDs.divineLightImbueID && skillKnowledge != null && skillKnowledge.IsItemLearned(IDs.divineFavourID))
    //        {
    //            _effect = (ImbueEffectPreset)ResourcesPrefabManager.Instance.GetEffectPreset(IDs.radiantLightImbueID);
    //        }
    //    }
    //}
}
