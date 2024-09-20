using HarmonyLib;
using InstanceIDs;
using SideLoader;
using System.Collections.Generic;
using System.Linq;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public class ArcaneInfluence
    {
        public static Skill Init()
        {
            var myitem = new SL_Skill()
            {
                Name = "Arcane Influence",
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.arbitraryPassiveSkillID,
                New_ItemID = IDs.arcaneInfluenceID,
                SLPackName = RelicKeeper.ModFolderName,
                SubfolderName = "ArcaneInfluence",
                Description = "You may benefit from passive relic effects.",
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
