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
            return ((character?.Inventory?.Equipment?.ItemEquippedCount(IDs.woodooCharmID) ?? 0) > 0) && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID);
        }

        public static bool HasRelicEquippedOrOnBackpack(Character character, int RequiredItemID = 0, int RequiredEnchantID = 0)
        {
            if (character?.Inventory?.Equipment is CharacterEquipment characterEquipment)
            {
                var potentialEquipment = characterEquipment.EquipmentSlots.Where(s => s != null && s.EquippedItem != null).Select(s => s.EquippedItem).ToList();

                foreach (var slot in character.EquippedOnBag().Union(potentialEquipment))
                {
                    var matchingItem = RequiredItemID == 0 || slot.ItemID == RequiredItemID;
                    var matchingEnchant = RequiredEnchantID == 0 || (slot is Equipment equipmentslot && equipmentslot.ActiveEnchantmentIDs.Contains(RequiredEnchantID));

                    if (matchingItem && matchingEnchant)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(CharacterEquipment), "GetTotalManaUseModifier")]
    public class CharacterEquipment_GetTotalManaUseModifier
    {
        [HarmonyPostfix]
        public static void Postfix(ref float __result, Character ___m_character)
        {
            if (___m_character is Character character && TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, IDs.manaFlowID))
            {
                __result -= 0.05f * RelicBehavior.GetEquippedRelics(character).Count;
            }
        }
    }

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