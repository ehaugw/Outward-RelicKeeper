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
    public static class AmplifiedRunicProtection
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 30, durabilityCost: 10, cooldown: 2);

            var addStatusEffect = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.runicProtectionAmplifiedID);
        }
    }
}
