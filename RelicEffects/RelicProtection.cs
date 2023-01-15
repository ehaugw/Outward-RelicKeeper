using EffectSourceConditions;
using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public static class RelicProtection
    {
        public static List<int> RelicProtectionTargetIDs = new List<int>();
        public const float efficiency = 0.2f;

        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 14, durabilityCost: 1, cooldown: 2);
            var addStatusEffect = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = RelicKeeper.Instance.relicProtectionEffectInstance;
            addStatusEffect.BaseChancesToContract = 100;
            RelicProtectionTargetIDs.Add(requiredItem);
        }
    }

    [HarmonyPatch(typeof(Character), "ReceiveHit", new Type[] { typeof(UnityEngine.Object), typeof(DamageList), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Character), typeof(float), typeof(bool) })]
    public class Character_ReceiveHit_RelicProtection
    {
        [HarmonyPriority(500)] //higher priority means it goes first
        [HarmonyPrefix]
        public static void Prefix(Character __instance, ref DamageList __result, UnityEngine.Object _damageSource, ref DamageList _damage, Vector3 _hitDir, Vector3 _hitPoint, float _angle, float _angleDir, Character _dealerChar, float _knockBack, bool _hitInventory)
        {
            if ((__instance?.StatusEffectMngr?.HasStatusEffect(RelicKeeper.Instance.relicProtectionEffectInstance.IdentifierName) ?? false) && !(_damageSource is StatusEffect))
            {
                foreach (var itemID in RelicProtection.RelicProtectionTargetIDs)
                {
                    if ((__instance?.Inventory?.GetOwnedItems(itemID)?.FirstOrDefault() ?? __instance?.Inventory?.GetEquippedItem(EquipmentSlot.EquipmentSlotIDs.LeftHand)) is Item item && item.CurrentDurability > 0)
                    {
                        item.ReduceDurability(_damage.TotalDamage * RelicProtection.efficiency * 0.1f);
                        _damage *= (1 - RelicProtection.efficiency);
                        return;
                    }
                }
                __instance.StatusEffectMngr.CleanseStatusEffect(RelicKeeper.Instance.relicProtectionEffectInstance.IdentifierName);
            }
        }
    }
}
