using SideLoader;
using UnityEngine;
using RelicCondition;
using System.Collections.Generic;

namespace RelicKeeper
{
    public static class TroglodyteStaffEffectHex
    {
        private static SL_ShootProjectile.SL_ProjectileShot projectileShot = new SL_ShootProjectile.SL_ProjectileShot()
        {
            LocalDirectionOffset = new Vector3(0, 1, 0),
            LockDirection = new Vector3(-999, -999, -999),
            MustShoot = false,
            NoBaseDir = true,
            RandomLocalDirectionAdd = new Vector3(0, 0, 0),
        };

        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, "Confuse and slow nearby enemies.",
                manaCost: 10, durabilityCost: 10, cooldown: 30, castType: Character.SpellCastType.Fast
            );

            var projectile = new SL_ShootProjectile()
            {
                Delay = 0,
                SyncType = Effect.SyncTypes.OwnerSync,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                CastPosition = Shooter.CastPositionType.Local,
                LocalPositionAdd = new Vector3(0, 2f, 0),
                NoAim = false,
                TargetType = Shooter.TargetTypes.Enemies,
                TransformName = "ShooterTransform",
                BaseProjectile = SL_ShootProjectile.ProjectilePrefabs.TrogArchMageHexProjectile,
                ProjectileShots = new SL_ShootProjectile.SL_ProjectileShot[] { projectileShot, projectileShot, projectileShot, projectileShot, projectileShot, projectileShot, },
                
                InstantiatedAmount = 8,
                Lifespan = 10,
                LateShootTime = 0,
                Unblockable = false,

                EffectsOnlyIfHitCharacter = false,
                EndMode = Projectile.EndLifeMode.Normal,
                DisableOnHit = false,
                IgnoreShooterCollision = true,
                TargetingMode = ShootProjectile.TargetMode.FindEnemies,
                TargetCountPerProjectile= 1,
                TargetRange = 20,
                AutoTarget = false,
                AutoTargetMaxAngle = 360,
                AutoTargetRange = 5,
                ProjectileForce = 3,
                ImpactSoundMaterial = EquipmentSoundMaterials.Fire,
                TrailEnabled = true,
                TrailTime = 0.075f,
                EffectBehaviour = EditBehaviours.NONE,
            }.ApplyToTransform(relicCondition.EffectsContainer) as ShootProjectile;
        }
    }
}
