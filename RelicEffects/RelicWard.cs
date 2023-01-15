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
    public static class RelicWard
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 7, durabilityCost: 2, cooldown: 15, castType: Character.SpellCastType.Bubble);

            var addStatusEffect = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.manaWardID);

            new SL_PlayTimedVFX()
            {
                VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXForceBubble,
            }.ApplyToTransform(relicCondition.ActivationEffectsContainer);
        }
    }
}
