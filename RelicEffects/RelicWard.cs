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
using RelicCondition;


namespace RelicKeeper
{
    public static class RelicWard
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Envelops you in Mana and, for an instant, you become immune to Damage.",
                manaCost: 7, durabilityCost: 8, cooldown: 20, castType: Character.SpellCastType.Bubble
            );

            var addStatusEffect = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.manaWardID);

            new SL_PlayTimedVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXForceBubble,
            }.ApplyToTransform(relicCondition.ActivationEffectsContainer);
        }
    }
}
