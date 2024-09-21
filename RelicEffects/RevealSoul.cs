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
    public static class RevealSoul
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 14, durabilityCost: 50, cooldown: 300, castType: Character.SpellCastType.GongStrike, relicLevel: 2, castModifier: Character.SpellCastModifier.Immobilized, mobileCastMovementMult: 0);

            var summon = relicCondition.EffectsContainer.gameObject.AddComponent<Summon>();
            summon.PositionType = Summon.SummonPositionTypes.InFrontOfTarget;
            summon.InstantiationMode = Summon.InstantiationManagement.CreateNew;
            summon.SummonedPrefab = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.soulPlacedOnGroundID).transform;

            //new SL_PlayTimedVFX()
            //{
            //    VFXPrefab = SL_PlayVFX.VFXPrefabs.VFXForceBubble,
            //}.ApplyToTransform(relicCondition.ActivationEffectsContainer);
        }
    }
}
