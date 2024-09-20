using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SideLoader;

namespace RelicKeeper
{
    using EffectSourceConditions;

    public class RelicKeeperSkillTree
    {
        public static SkillSchool RelicKeeperSkillSchool;

        public static void SetupSkillTree(ref SkillSchool skillTreeInstance)
        {
            SL_SkillTree myskilltree;

            myskilltree = new SL_SkillTree()
            {
                Name = "Relic Keeper",
                
                SkillRows = new List<SL_SkillRow>() {
                    new SL_SkillRow() { RowIndex = 1, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 1, SilverCost = 50, SkillID = IDs.useRelicID,                        Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 50, SkillID = IDs.unleashID,                         Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                            new SL_SkillSlot() { ColumnIndex = 3, SilverCost = 50, SkillID = IDs.manaFlowID,                        Breakthrough = false,   RequiredSkillSlot = Vector2.zero  },
                    } },

                    new SL_SkillRow() { RowIndex = 2, Slots = new List<SL_BaseSkillSlot>() {
                    } },

                    new SL_SkillRow() { RowIndex = 3, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 500, SkillID = IDs.relicLoreID,                      Breakthrough = true,  RequiredSkillSlot = Vector2.zero, },
                    } },

                    new SL_SkillRow() {
                        RowIndex = 4, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlotFork()
                            {
                                ColumnIndex = 2,
                                RequiredSkillSlot = Vector2.zero,
                                Choice1 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.mythicLoreID,       RequiredSkillSlot = new Vector2(3, 2)},
                                Choice2 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.useRelic2ID,        RequiredSkillSlot = new Vector2(3, 2)},
                            }
                        }
                    },

                    new SL_SkillRow() {
                        RowIndex = 5, Slots = new List<SL_BaseSkillSlot>() {
                            new SL_SkillSlotFork()
                            {
                                ColumnIndex = 2,
                                RequiredSkillSlot = Vector2.zero,
                                Choice1 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.arcaneInfluenceID,  RequiredSkillSlot = new Vector2(3, 2)},
                                Choice2 = new SL_SkillSlot() { ColumnIndex = 2, SilverCost = 600, SkillID = IDs.overchannelID,      RequiredSkillSlot = new Vector2(3, 2)},
                            }
                        }
                    }
                }
            };

            skillTreeInstance = myskilltree.CreateBaseSchool();
            myskilltree.ApplyRows();

            RelicKeeperSkillSchool = skillTreeInstance;
        }
    }
}
