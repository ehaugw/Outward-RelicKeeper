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
    public static class BasicHeal
    {
        const float HEAL_RATIO = 1.0f;
        const float HEAL_AMOUNT = 20f;
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Heals the user.",
                manaCost: HEAL_AMOUNT/HEAL_RATIO, durabilityCost: 2
            );

            var affectHealth = relicCondition.EffectsContainer.gameObject.AddComponent<AffectHealth>();
            affectHealth.AffectQuantity = HEAL_AMOUNT;
        }
    }
}
