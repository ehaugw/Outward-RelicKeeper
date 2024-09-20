using EffectSourceConditions;
using HarmonyLib;
using InstanceIDs;
using SideLoader;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public static class DivineIntervention
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 50, durabilityCost: 50, cooldown: 2, relicLevel: 2);

            var addStatusEffect = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = RelicKeeper.Instance.divineInterventionStatusEffectInstance;
        }
    }

    [HarmonyPatch(typeof(Character), "ReceiveHit", new Type[] { typeof(UnityEngine.Object), typeof(DamageList), typeof(Vector3), typeof(Vector3), typeof(float), typeof(float), typeof(Character), typeof(float), typeof(bool) })]
    public class Character_ReceiveHit
    {
        [HarmonyPrefix]
        public static void Prefix(Character __instance, ref DamageList __result, UnityEngine.Object _damageSource, ref DamageList _damage, Vector3 _hitDir, Vector3 _hitPoint, float _angle, float _angleDir, Character _dealerChar, float _knockBack, bool _hitInventory)
        {
            if (__instance.StatusEffectMngr?.HasStatusEffect(RelicKeeper.Instance.divineInterventionStatusEffectInstance.IdentifierName) ?? false)
            {
                DamageList damageList = _damage.Clone();
                StatusEffect statusEffect = _damageSource as StatusEffect;

                __instance.Stats.GetMitigatedDamage(null, ref damageList, statusEffect != null);
                if (damageList.TotalDamage >= __instance.Health)
                {
                    _damage.Clear();
                    //__instance.StatusEffectMngr.RemoveStatusWithIdentifierName(RelicKeeper.Instance.divineInterventionStatusEffectInstance.IdentifierName);
                    var divineIntervention = __instance.StatusEffectMngr.GetStatusEffectOfName(RelicKeeper.Instance.divineInterventionStatusEffectInstance.IdentifierName);
                    if (!__instance.StatusEffectMngr.HasStatusEffect(IDs.manaWardID))
                    {
                        var status = __instance.StatusEffectMngr.AddStatusEffect(IDs.manaWardID);
                    }
                    divineIntervention.IncreaseAge((int)divineIntervention.RemainingLifespan - 2);
                    divineIntervention.IsHidden = true;
                    divineIntervention.DisplayInHud = false;

                    //__instance.StatusEffectMngr.onStatusActivated?.Invoke(status);
                    //status.Activate(__instance);
                }
            }
        }
    }
}
