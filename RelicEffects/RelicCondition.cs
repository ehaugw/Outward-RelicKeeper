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
    public static class RelicCondition
    {
        public static Transform Apply(Skill skill, int requiredItem, float manaCost=0, float cooldown=2, float staminaCost=0, float healthCost=0, float durabilityCost=0)
        {
            
            var effectsContainer = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EffectSourceConditions.EFFECTS_CONTAINER, true, true, skill.transform).transform;
            var requirementTransform = TinyGameObjectManager.GetOrMake(effectsContainer.transform, EffectSourceConditions.EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionEquipment>();
            skillReq.RequiredItemID = requiredItem;

            var dynamicSkillStat = requirementTransform.gameObject.AddComponent<DynamicSkillStat>();
            dynamicSkillStat.ManaCost = manaCost;
            dynamicSkillStat.HealthCost = healthCost;
            dynamicSkillStat.StaminaCost= staminaCost;
            dynamicSkillStat.Cooldown = cooldown;
            dynamicSkillStat.DurabilityCost= durabilityCost;

            return effectsContainer;
        }
    }
}
