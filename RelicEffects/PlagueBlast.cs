﻿using EffectSourceConditions;
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
    public static class PlagueBlast
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Creates a cloud of spores that inflicts Plague on all nearby creatures.",
                manaCost: 14, durabilityCost: 85, cooldown: 100, relicLevel: 1, castModifier: Character.SpellCastModifier.Immobilized, mobileCastMovementMult: 0, castType: Character.SpellCastType.GongStrike
            );

            var damageBlast = new SL_ShootBlast()
            {
                CastPosition = Shooter.CastPositionType.Local,
                LocalPositionAdd = new Vector3(0, 0, 0),

                TargetType = Shooter.TargetTypes.Any,

                BaseBlast = SL_ShootBlast.BlastPrefabs.EliteAshGiantAsh,
                Radius = 7,
                BlastLifespan = 5,
                RefreshTime = 0.5f,
                InstantiatedAmount = 5,
                Interruptible = false,
                HitOnShoot = true,
                IgnoreShooter = false,
                ParentToShootTransform = false,
                ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
                DontPlayHitSound = true,
                EffectBehaviour = EditBehaviours.Destroy,
                Delay = 0,
                BlastEffects = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        }
                    }
                },
            }.ApplyToTransform(relicCondition.EffectsContainer) as ShootBlast;


            var damageBlastEffect = damageBlast.BaseBlast.transform.Find("Effects");
            damageBlastEffect.gameObject.SetActive(true);

            var addStatus = damageBlastEffect.gameObject.AddComponent<AddStatusEffectBuildUp>();
            addStatus.Status = ResourcesPrefabManager.Instance.GetStatusEffectPrefab(IDs.plagueNameID);
            addStatus.BuildUpValue = 20;

        }
    }
}
