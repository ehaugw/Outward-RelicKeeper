using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public class ManaFlow
    {
        public const float ManaCostReduction = 0.3f;
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Mana Flow",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.manaFlowID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "ManaFlow",
                Description = "Decreases the mana cost of Use Relic.",
                IsUsable = false,
                CastType = Character.SpellCastType.NONE,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1f,

                Cooldown = 0,
                StaminaCost = 0,
                ManaCost = 0,
            };
            myitem.ApplyTemplate();
            Skill skill = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID) as Skill;
            return skill;
        }
    }
}
