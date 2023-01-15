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
            var effectsContainer = RelicCondition.Apply(skill, requiredItem, manaCost: 7, durabilityCost: 2, cooldown: 15);

            var addStatusEffect = effectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addStatusEffect.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.manaWardID);
        }
    }
}
