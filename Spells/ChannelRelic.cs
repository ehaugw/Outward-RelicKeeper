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
    public class ChannelRelic
    {
        public static Skill Init()
        {
            var myitem = new SL_AttackSkill()
            {
                Name = "Channel Relic",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.sparkID,
                New_ItemID = IDs.channelRelicID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "ChannelRelic",
                Description = "Channel unknown powers from within a relic.",
                CastType = Character.SpellCastType.EnterInnBed,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,
                CastSheatheRequired = 1,

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "ActivationEffects",
                        Effects = new SL_Effect[] {
                            new SL_AddStatusEffectBuildUp() {StatusEffect = "ChannelingRelic", Buildup=100, Delay = 0},
                        }
                    }
                },
                Cooldown = 0,
                StaminaCost = 0,
                HealthCost = 0,
                ManaCost = 0,
            };

            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;

            return skill;
        }
    }
}
