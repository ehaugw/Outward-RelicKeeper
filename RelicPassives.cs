using HarmonyLib;
using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;

namespace RelicKeeper
{
    using CharacterExtensions;
    using UnityEngine;
    using static MapMagic.ObjectPool;

    public class RelicPassives
    {
        public const float RelicProtectionEfficiency = 0.2f;

        public static bool HasArcaneInfluence(Character character)
        {
            return SkillRequirements.SafeHasSkillKnowledge(character, IDs.arcaneInfluenceID);
        }
        public static bool CanCastTormentWithDrawnWeapon(Character character)
        {
            return RelicBehavior.HasRelicEquippedOrOnBackpack(character, IDs.woodooCharmID) && HasArcaneInfluence(character);
        }

        public static Equipment CanProtectDamageWithRelic(Character character)
        {
            if (HasArcaneInfluence(character))
            {
                var basicRelic = RelicBehavior.HasRelicEquippedOrOnBackpack(character, IDs.basicRelicID);
                if (basicRelic)
                {
                    return basicRelic;
                }
                return RelicBehavior.HasRelicEquippedOrOnBackpack(character, IDs.gildedRelicID);
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