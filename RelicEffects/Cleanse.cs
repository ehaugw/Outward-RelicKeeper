using EffectSourceConditions;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public static class Cleanse
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 10, durabilityCost: 0, cooldown: 0, castType: Character.SpellCastType.Cleanse);

            var addBleed = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addBleed.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.extremeBleedNameID);

            var removeCorruption = relicCondition.EffectsContainer.gameObject.AddComponent<AffectCorruption>();
            removeCorruption.AffectQuantity = -300; //goes to 1000

            var decayDamage = relicCondition.EffectsContainer.gameObject.AddComponent<PunctualDamage>();
            decayDamage.Damages = new DamageType[] { new DamageType(DamageType.Types.Decay, 65) };
        }
    }
}
