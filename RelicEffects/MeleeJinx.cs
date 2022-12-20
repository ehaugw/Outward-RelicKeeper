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
    public static class MeleeJinx
    {
        public static void Apply(Skill skill, int requiredItem)
        {
            var effectsContainer = RelicCondition.Apply(skill, requiredItem, manaCost: 7, durabilityCost: 1);

            var damageBlast = new SL_ShootBlast()
            {
                CastPosition = Shooter.CastPositionType.Local,
                LocalPositionAdd = new Vector3(0, 1.25f, .5f),

                TargetType = Shooter.TargetTypes.Enemies,

                BaseBlast = SL_ShootBlast.BlastPrefabs.ForcePush,
                Radius = 7,
                BlastLifespan = 1,
                RefreshTime = -1,
                InstantiatedAmount = 5,
                Interruptible = false,
                HitOnShoot = true,
                IgnoreShooter = true,
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
            }.ApplyToTransform(effectsContainer) as ShootBlast;


            var damageBlastEffect = damageBlast.BaseBlast.transform.Find("Effects");
            damageBlastEffect.gameObject.SetActive(true);

            var damage = damageBlastEffect.gameObject.AddComponent<PunctualDamage>();
            damage.Delay = 0f;
            damage.Damages = new DamageType[] { /*new DamageType(DamageType.Types.Physical, 40)*/ };
            damage.Knockback = 40;
            damage.UseOnce = true;

            var addRandomEffect = damageBlastEffect.gameObject.AddComponent<AddStatusEffectRandom>();
            
            addRandomEffect.Statuses = new[] 
            {
                IDs.doomNameID, IDs.hauntedNameID, IDs.curseNameID, IDs.scorchedNameID, IDs.chillNameID
            }.Select(s => ResourcesPrefabManager.Instance.GetStatusEffectPrefab(s)).ToArray();
            addRandomEffect.ForceID = -1;

            var projectile = SL_ShootProjectile.GetProjectilePrefab(SL_ShootProjectile.ProjectilePrefabs.JinxProjectile);

            var vfxSystems = new List<VFXSystem>();

            foreach (var transformNameShort in new String[] {"Doom", "Haunt", "Curse", "Scorch", "Chill"})
            {
                var transform = UnityEngine.Object.Instantiate(projectile.transform.Find("VFX" + transformNameShort));
                transform.parent = damageBlastEffect;
                transform.gameObject.SetActive(true);
                TinyGameObjectManager.RecursiveDontDestroyOnLoad(transform);
                vfxSystems.Add(transform.GetComponent<VFXSystem>());
            }

            addRandomEffect.VfxSystems = vfxSystems.ToArray();

        }
    }
}
