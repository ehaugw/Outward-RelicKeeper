using HarmonyLib;
using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;

namespace RelicKeeper
{
    using CharacterExtensions;

    public class RelicPassives
    {
        public static bool CanCastTormentWithDrawnWeapon(Character character)
        {
            return RelicBehavior.HasRelicEquippedOrOnBackpack(character, IDs.woodooCharmID) && SkillRequirements.SafeHasSkillKnowledge(character, IDs.arcaneInfluenceID);
        }
    }

    //[HarmonyPatch(typeof(CharacterEquipment), "GetTotalManaUseModifier")]
    //public class CharacterEquipment_GetTotalManaUseModifier
    //{
    //    [HarmonyPostfix]
    //    public static void Postfix(ref float __result, Character ___m_character)
    //    {
    //        if (___m_character is Character character && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID))
    //        {
    //            __result -= 0.05f * RelicBehavior.GetEquippedRelics(character).Count;
    //        }
    //    }
    //}

    [HarmonyPatch(typeof(Item), "GetCastSheathRequired")]
    public class Item_GetCastSheathRequired
    {
        [HarmonyPostfix]
        public static void Postfix(Item __instance, ref int __result)
        {
            if ((__instance.ItemID == IDs.tormentID) && RelicPassives.CanCastTormentWithDrawnWeapon(__instance?.OwnerCharacter))
            {
                __result = 0;
            }
        }
    }
}