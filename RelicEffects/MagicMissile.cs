using SideLoader;
using UnityEngine;
using RelicCondition;
using System.Collections.Generic;
using InstanceIDs;

namespace RelicKeeper
{
    public static class MagicMissile
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var relicCondition = RelicConditionBuilder.Apply(
                skill, requiredItem, ".",
                manaCost: 14, durabilityCost: 10, cooldown: 5, castType: Character.SpellCastType.Fast, relicLevel: 1
            );

            for (int i = 0; i < 5; i++)
            {
                new SL_ShootProjectile()
                {
                    Delay = 0,
                    SyncType = Effect.SyncTypes.OwnerSync,
                    OverrideCategory = EffectSynchronizer.EffectCategories.None,
                    CastPosition = Shooter.CastPositionType.Local,
                    LocalPositionAdd = new Vector3(0, 2f, 0),
                    NoAim = false,
                    TargetType = Shooter.TargetTypes.Enemies,
                    TransformName = "ShooterTransform",
                    BaseProjectile = SL_ShootProjectile.ProjectilePrefabs.TrogBoonHealProjectile,
                    ProjectileShots = new SL_ShootProjectile.SL_ProjectileShot[]
                    {
                        new SL_ShootProjectile.SL_ProjectileShot()
                        {
                            LocalDirectionOffset = new Vector3(0, 0, 1),
                            LockDirection = new Vector3(-999, -999, -999),
                            MustShoot = false,
                            NoBaseDir = true,
                            RandomLocalDirectionAdd = new Vector3(2,2,0),
                            //RandomLocalDirectionAdd = new Vector3(Mathf.Cos(Mathf.PI / i) * 3, Mathf.Sin(Mathf.PI / i) * 3, 0),
                        },
                    },

                    InstantiatedAmount = 2,
                    Lifespan = 10,
                    LateShootTime = 0,
                    Unblockable = false,

                    EffectsOnlyIfHitCharacter = false,
                    EndMode = Projectile.EndLifeMode.Normal,
                    DisableOnHit = false,
                    IgnoreShooterCollision = false,
                    TargetingMode = ShootProjectile.TargetMode.OwnerTarget,
                    TargetCountPerProjectile = 1,
                    TargetRange = 20,
                    AutoTarget = true,
                    AutoTargetMaxAngle = 360,
                    AutoTargetRange = 10,
                    ProjectileForce = 10,
                    ImpactSoundMaterial = EquipmentSoundMaterials.Fire,
                    TrailEnabled = true,
                    TrailTime = 0.075f,
                    EffectBehaviour = EditBehaviours.Destroy,
                    ProjectileEffects = new SL_EffectTransform[]
                    {
                    new SL_EffectTransform() {
                        TransformName = "HitEffects",
                        Effects = new SL_Effect[] {
                            new SL_PunctualDamage() {
                                Damage = new List<SL_Damage>{new SL_Damage() { Damage = 10, Type = DamageType.Types.Ethereal } }
                            }
                        }
                    }
                    }
                }.ApplyToTransform(relicCondition.EffectsContainer);
            }
        }
    }
}
