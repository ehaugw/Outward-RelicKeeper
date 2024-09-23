using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TinyHelper;
using EffectSourceConditions;

namespace RelicKeeper
{
    public class Unleash
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Unleash",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.sparkID,
                New_ItemID = IDs.unleashID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "Unleash",
                Description = "Unleash unknown powers from within a relic.",
                CastType = Character.SpellCastType.Fast,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
                CastSheatheRequired = 0,

                Cooldown = 300,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            //Set the correct animation
            new SL_PlaySoundEffect()
            {
                Follow = true,
                OverrideCategory = EffectSynchronizer.EffectCategories.None,
                Delay = 0.05f,
                MinPitch = 1,
                MaxPitch = 1,
                SyncType = Effect.SyncTypes.OwnerSync,
                Sounds = new List<GlobalAudioManager.Sounds>() { GlobalAudioManager.Sounds.SFX_SKILL_Spark }
            }.ApplyToTransform(TinyGameObjectManager.GetOrMake(skill.transform, "ActivationEffects", true, true));

            EffectSourceConditionChecker.AddToSkill(skill);
            EquipSkillDurabilityCondition.AddToSkillNotBroken(skill, EquipmentSlot.EquipmentSlotIDs.LeftHand);

            //DONE
            RelicWard.Apply(skill, IDs.basicRelicID);
            FireSigil.Apply(skill, IDs.redLadysDaggerID);
            PlagueBlast.Apply(skill, IDs.mushroomShieldID);
            Cleanse.Apply(skill, IDs.lanternOfSouldID);
            CurseOfVulnerabilityBlast.Apply(skill, IDs.woodooCharmID);
            
            //IN PROGRESS
            //DivineIntervention.Apply(skill, IDs.goldLichTalismanID);
            //RelicProtection.Apply(skill, IDs.alphaTuanosaurTrinketID);

            return skill;
        }
    }
}
