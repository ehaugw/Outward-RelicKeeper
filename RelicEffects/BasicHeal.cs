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
        static float HEAL_RATIO = 1.0f;
        static float HEAL_AMOUNT = 20f;
        public static void Apply(Skill skill, int requiredItem)
        {
            var effectsContainer = RelicCondition.Apply(skill, requiredItem, manaCost: HEAL_AMOUNT/HEAL_RATIO, durabilityCost: 3);

            var affectHealth = effectsContainer.gameObject.AddComponent<AffectHealth>();
            affectHealth.AffectQuantity = HEAL_AMOUNT;
        }
    }
}
