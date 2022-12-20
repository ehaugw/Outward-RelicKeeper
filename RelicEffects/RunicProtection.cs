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
    public static class RunicProtection
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var effectsContainer = RelicCondition.Apply(skill, requiredItem, manaCost: 5, durabilityCost: 1, cooldown: 2);

            var addStatusEffect = effectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.runicProtectionID);
        }
    }
}
