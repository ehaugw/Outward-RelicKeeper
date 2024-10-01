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
using RelicCondition;


namespace RelicKeeper
{
    public static class Cleanse
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Requires 50% Corruption and a Mana Stone. Creates a Dark Stone, removes 30% Corruption, suffer Extreme Bleeding and 65 Decay Decay damage.",
                manaCost: 10, durabilityCost: 0, cooldown: 0, castType: Character.SpellCastType.Cleanse, relicLevel: 2
            );

            foreach (var transform in new Transform[] { relicCondition.ActivationEffectsContainer, relicCondition.EffectsContainer })
            {
                var requirementTransform = TinyGameObjectManager.GetOrMake(transform, EffectSourceConditions.EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
                var itemReq = requirementTransform.gameObject.AddComponent<SourceConditionItemInInventory>();
                itemReq.RequiredItemID = IDs.manaStoneID;
                itemReq.Amount = 1;

                var corruptionReq = requirementTransform.gameObject.AddComponent<SourceConditionCorruption>();
                corruptionReq.Corruption = 300;
            }

            var addBleed = relicCondition.EffectsContainer.gameObject.AddComponent<AddStatusEffect>();
            addBleed.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.extremeBleedNameID);

            var removeCorruption = relicCondition.EffectsContainer.gameObject.AddComponent<AffectCorruption>();
            removeCorruption.AffectQuantity = -300; //goes to 1000

            var decayDamage = relicCondition.EffectsContainer.gameObject.AddComponent<PunctualDamage>();
            decayDamage.Damages = new DamageType[] { new DamageType(DamageType.Types.Decay, 65) };

            var addDarkStone = relicCondition.EffectsContainer.gameObject.AddComponent<CreateItemEffect>();
            addDarkStone.ItemToCreate = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.darkStoneID);

            var removeManaStone = relicCondition.EffectsContainer.gameObject.AddComponent<RemoveItemFromInventory>();
            removeManaStone.ItemID = IDs.manaStoneID;

        }
    }
}
