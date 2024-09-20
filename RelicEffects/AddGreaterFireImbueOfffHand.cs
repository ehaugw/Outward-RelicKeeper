using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace RelicKeeper
{
    public static class AddGreaterFireImbueOfffHand
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 14, durabilityCost: 50, cooldown: 200, castType: Character.SpellCastType.Bubble, relicLevel: 1);
            
            var addImbue = relicCondition.EffectsContainer.gameObject.AddComponent<ImbueWeapon>();
            addImbue.ImbuedEffect = ResourcesPrefabManager.Instance.GetEffectPreset(IDs.fireVarnishImbueID) as ImbueEffectPreset;
            addImbue.AffectSlot = Weapon.WeaponSlot.OffHand;
        }
    }
}
