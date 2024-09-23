﻿using CharacterExtensions;
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
    public static class RelicBehavior
    {
        public static Equipment HasRelicEquippedOrOnBackpack(Character character, int RequiredItemID = 0, int RequiredEnchantID = 0)
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
                        return slot as Equipment;
                    }
                }
            }
            return null;
        }
    }
}
