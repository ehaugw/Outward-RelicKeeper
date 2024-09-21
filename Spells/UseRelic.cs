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
    public class UseRelic
    {
        public static Skill Init(int id)
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Use Relic",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.sparkID,
                New_ItemID = id,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "UseRelic",
                Description = "Use unknown powers from within a relic.",
                CastType = Character.SpellCastType.LanternThrowLeft,
                CastModifier = Character.SpellCastModifier.Mobile,
                CastLocomotionEnabled = true,
                MobileCastMovementMult = 0.3f,
                CastSheatheRequired = 0,

                Cooldown = 2,
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

            // DONE
            RelicPush.Apply(skill, IDs.basicRelicID);
            AddGreaterFireImbueOfffHand.Apply(skill, IDs.redLadysDaggerID);
            MeleeJinx.Apply(skill, IDs.woodooCharmID);

            //IN PROGRESS
            //BasicHeal.Apply(skill, IDs.goldLichTalismanID);
            //RunicProtection.Apply(skill, IDs.alphaTuanosaurTrinketID);
            //TameBeast.Apply(skill, IDs.obsidianAmuletID);

            return skill;
        }
    }
}
