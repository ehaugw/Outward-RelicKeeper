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

namespace RelicKeeper
{
    public static class RelicPush
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicCondition.Apply(skill, requiredItem, manaCost: 7, durabilityCost: 1, cooldown: 8);

            var damageBlast = new SL_ShootBlast()
            {
                CastPosition = Shooter.CastPositionType.Local,
                LocalPositionAdd = new Vector3(0, 1.25f, .7f),

                TargetType = Shooter.TargetTypes.Enemies,

                BaseBlast = SL_ShootBlast.BlastPrefabs.ForcePush,
                Radius = 1.2f,
                BlastLifespan = 1,
                RefreshTime = -1,
                InstantiatedAmount = 5,
                Interruptible = false,
                HitOnShoot = true,
                IgnoreShooter = true,
                ParentToShootTransform = false,
                ImpactSoundMaterial = EquipmentSoundMaterials.NONE,
                DontPlayHitSound = true,
                EffectBehaviour = EditBehaviours.NONE,
                Delay = 0,
            }.ApplyToTransform(relicCondition.EffectsContainer) as ShootBlast;
        }
    }
}
