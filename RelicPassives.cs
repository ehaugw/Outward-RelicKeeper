using HarmonyLib;
using InstanceIDs;
using System;

namespace RelicKeeper
{
    using UnityEngine;
    using static MapMagic.ObjectPool;
    using RelicCondition;

    public class RelicPassives
    {
        public const float RelicProtectionEfficiency = 0.2f;

        public static bool CanCastTormentWithDrawnWeapon(Character character)
        {
            return RelicCondition.HasRelicEquippedOrOnBackpack(character, IDs.woodooCharmID) && RelicCondition.HasArcaneInfluence(character);
        }

        public static Equipment CanProtectDamageWithRelic(Character character)
        {
            if (RelicCondition.HasArcaneInfluence(character))
            {
                var basicRelic = RelicCondition.HasRelicEquippedOrOnBackpack(character, IDs.basicRelicID);
                if (basicRelic)
                {
                    return basicRelic;
                }
                return RelicCondition.HasRelicEquippedOrOnBackpack(character, IDs.gildedRelicID);
            }
            return null;
        }
    }

    //[HarmonyPatch(typeof(CharacterEquipment), "GetTotalManaUseModifier")]
    //public class CharacterEquipment_GetTotalManaUseModifier
    //{
    //    [HarmonyPostfix]
    //    public static void Postfix(ref float __result, Character ___m_character)
    //    {
    //        if (___m_character is Character character && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID))
    //        {
    //            __result -= 0.05f * RelicBehavior.GetEquippedRelics(character).Count;
    //        }
    //    }
    //}

    [HarmonyPatch(typeof(Item), "GetCastSheathRequired")]
    public class Item_GetCastSheathRequired
    {
        [HarmonyPostfix]
        public static void Postfix(Item __instance, ref int __result)
        {
            if ((__instance.ItemID == IDs.tormentID) && RelicPassives.CanCastTormentWithDrawnWeapon(__instance?.OwnerCharacter))
            {
                __result = 0;
            }
        }
    }

    [HarmonyPatch(typeof(Character), "ReceiveHit", new Type[] { typeof(UnityEngine.Object), typeof(DamageList), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Character), typeof(float), typeof(bool) })]
    public class Character_ReceiveHit_RelicProtection
    {
        [HarmonyPriority(500)] //higher priority means it goes first
        [HarmonyPrefix]
        public static void Prefix(Character __instance, ref DamageList __result, UnityEngine.Object _damageSource, ref DamageList _damage, Vector3 _hitDir, Vector3 _hitPoint, float _angle, float _angleDir, Character _dealerChar, float _knockBack, bool _hitInventory)
        {
            if (!(_damageSource is StatusEffect) && RelicPassives.CanProtectDamageWithRelic(__instance) is Equipment protectiveRelic && protectiveRelic.CurrentDurability > 0)
            {
                protectiveRelic.ReduceDurability(_damage.TotalDamage * RelicPassives.RelicProtectionEfficiency);
                _damage *= (1 - RelicPassives.RelicProtectionEfficiency);
                return;
            }
        }
    }
}