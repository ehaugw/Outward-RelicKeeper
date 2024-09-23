﻿using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace RelicKeeper
{
    public static class AddChaosImbueOffhand
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 5, durabilityCost: 10, cooldown: 30, castType: Character.SpellCastType.Bubble, relicLevel: 1, mobileCastMovementMult: 0, castModifier: Character.SpellCastModifier.Immobilized);
            
            var addImbue = relicCondition.EffectsContainer.gameObject.AddComponent<ImbueWeapon>();
            addImbue.ImbuedEffect = ResourcesPrefabManager.Instance.GetEffectPreset(IDs.chaosImbueID) as ImbueEffectPreset;
            addImbue.AffectSlot = Weapon.WeaponSlot.OffHand;
            addImbue.SetLifespanImbue(30);
        }
    }
}