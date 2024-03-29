﻿using System.Collections.Generic;
using TinyHelper;
using InstanceIDs;
using SideLoader;
using UnityEngine;
using System.Linq;
using EffectSourceConditions;

namespace RelicKeeper
{
    using EffectSourceConditions;

    class EffectInitializer
    {
        public static StatusEffect MakeRelicProtectionPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                displayName:            "Calixa's Relic",
                effectName:             "RelicProtection",
                familyName:             "RelicProtection",
                description:            "You are protected by Calixa's Relic.",
                lifespan:               300,
                refreshRate:            0.25f,
                stackBehavior:          StatusEffectFamily.StackBehaviors.Override,
                targetStatusName:       "Runic Protection",
                isMalusEffect:          false,
                modGUID:                RelicKeeper.GUID);

            return statusEffect;
        }

        public static StatusEffect MakeChannelRelicPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                effectName: "ChannelingRelic",
                familyName: "ChannelingRelic",
                description: "You are channeling a relic's powers",
                lifespan: -1,
                refreshRate: 0.25f,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Mana Ratio Recovery 3",
                isMalusEffect: false,
                modGUID: RelicKeeper.GUID);

            var effectSignature = statusEffect.StatusEffectSignature;
            var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<ChannelRelicEffect>();
            effectComponent.UseOnce = false;
            effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = true;
            statusEffect.DisplayInHud = false;

            return statusEffect;
        }

        public static StatusEffect MakeDivineInterventionPrefab()
        {
            var statusEffect = TinyEffectManager.MakeStatusEffectPrefab(
                displayName: "Divine Intervention",
                effectName: "DivineIntervention",
                familyName: "DivineIntervention",
                description: "You will be protected when receiving an otherwise lethal hit.",
                lifespan: 1200,
                refreshRate: -1,
                stackBehavior: StatusEffectFamily.StackBehaviors.Override,
                targetStatusName: "Mana Ratio Recovery 3",
                isMalusEffect: false,
                modGUID: RelicKeeper.GUID);

            //var effectSignature = statusEffect.StatusEffectSignature;
            //var effectComponent = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectSignature.transform).AddComponent<ChannelRelicEffect>();
            //effectComponent.UseOnce = false;
            //effectSignature.Effects = new List<Effect>() { effectComponent };

            statusEffect.IsHidden = false;
            statusEffect.DisplayInHud = true;

            return statusEffect;
        }
    }
}
