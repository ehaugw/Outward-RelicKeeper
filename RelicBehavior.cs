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
        public static List<Item> GetEquippedRelics(Character character)
        {
            var result = character.Inventory.GetOwnedItems(TinyTagManager.GetOrMakeTag(IDs.RelicTag)).Where(x => x.DisplayedOnBag).ToList();
            if (character.LeftHandEquipment?.HasTag(TinyTagManager.GetOrMakeTag(IDs.RelicTag)) ?? false)
            {
                result.Add(character.LeftHandEquipment);
            }
            return result;
        }
    }
}
