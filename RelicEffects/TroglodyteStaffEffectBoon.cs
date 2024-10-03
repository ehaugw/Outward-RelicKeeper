using SideLoader;
using UnityEngine;
using RelicCondition;
using System.Collections.Generic;

namespace RelicKeeper
{
    public static class TroglodyteStaffEffectBoon
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
                skill, requiredItem, "Heal and protect your allies, while infusing their weapons with mana.",
                manaCost: 50, durabilityCost: 50, cooldown: 30, castType: Character.SpellCastType.Fast, relicLevel: 2
            );

            var projectile = new SL_ShootProjectile()
            {
                Delay = 0,
                SyncType = Effect.SyncTypes.OwnerSync,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                CastPosition = Shooter.CastPositionType.Local,
                LocalPositionAdd = new Vector3(0, 2f, 0),
                NoAim = false,
                TargetType = Shooter.TargetTypes.Allies,
                TransformName = "ShooterTransform",
                BaseProjectile = SL_ShootProjectile.ProjectilePrefabs.TrogArchMageBoon,
                ProjectileShots = new SL_ShootProjectile.SL_ProjectileShot[] { projectileShot, projectileShot, projectileShot, projectileShot, projectileShot, projectileShot, },
                
                InstantiatedAmount = 8,
                Lifespan = 10,
                LateShootTime = 0,
                Unblockable = false,

                EffectsOnlyIfHitCharacter = false,
                EndMode = Projectile.EndLifeMode.Normal,
                DisableOnHit = false,
                IgnoreShooterCollision = false,
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
