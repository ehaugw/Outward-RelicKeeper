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
    public static class RevealSoul
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Release a soul from the lantern.", 
                manaCost: 5, durabilityCost: 50, cooldown: 300, castType: Character.SpellCastType.GongStrike, relicLevel: 1, castModifier: Character.SpellCastModifier.Immobilized, mobileCastMovementMult: 0
            );

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
