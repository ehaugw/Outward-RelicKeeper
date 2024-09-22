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
    using UnityEngine;

    class EffectInitializer
    {
        public static ImbueEffectPreset MakeChaosImbue()
        {
            //var requireDivineFavour = false;

            ImbueEffectPreset effectPreset = TinyEffectManager.MakeImbuePreset(
                imbueID: IDs.chaosImbueID,
                name: "Chaos Imbue",
                description: "Weapon deals some fire damage, causes Burning buildup, and causes corrution buildup on the wearer on each hit.",
                //iconFileName: SwampWitch.ModFolderName + @"\SideLoader\Texture2D\chaosImbueIcon.png",
                visualEffectID: IDs.fireVarnishImbueID
            );

            Transform effectTransform;

            effectTransform = TinyGameObjectManager.MakeFreshObject("Effects", true, true, effectPreset.transform).transform;
            TinyEffectManager.MakeWeaponDamage(effectTransform, 0, 0.25f, DamageType.Types.Fire, 0);
            TinyEffectManager.MakeStatusEffectBuildup(effectTransform, IDs.burningNameID, 49);

            effectTransform = TinyGameObjectManager.MakeFreshObject("HitEffects", true, true, effectPreset.transform).transform;

            var corruptionAdder = effectTransform.gameObject.AddComponent<TinyHelper.AffectCorruption>();
            corruptionAdder.AffectOwner = true;
            corruptionAdder.AffectQuantity = 5f; // out of 1k
            corruptionAdder.IsRaw = false;
            corruptionAdder.IsImbueEffect = true;
            var fx = effectPreset.ImbueFX;

            fx = Object.Instantiate(fx);
            fx.gameObject.SetActive(false);
            Object.DontDestroyOnLoad(fx);
            effectPreset.ImbueFX = fx;

            var main = fx.Find("FireParticlesLargeCore").GetComponent<ParticleSystem>().main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0.3f, 0.4f, 0.5f), new Color(0.5f, 0.0f, 0.1f, 0.5f));

            BaseDamageModifiers.BaseDamageModifiers.WeaponDamageModifiers += delegate (Weapon weapon, DamageList original, ref DamageList result)
            {
                if (weapon?.HasImbuePreset(IDs.chaosImbueID) ?? false)
                {
                    float corruiption = (weapon?.OwnerCharacter?.Stats as PlayerCharacterStats)?.Corruption ?? 0;
                    result.Add(new DamageList(DamageType.Types.Fire, (1000 - corruiption) / 100)); // corruption goes from 0 to 1000
                }
            };

            return effectPreset;
        }

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
