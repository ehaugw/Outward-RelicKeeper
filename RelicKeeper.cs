namespace RelicKeeper
{
    using TinyHelper;
    using EffectSourceConditions;
    using BepInEx;
    using SideLoader;
    using UnityEngine;
    using HarmonyLib;
    using System.IO;
    using SynchronizedWorldObjects;
    using InstanceIDs;
    using RelicCondition;
    using CrusadersEquipment;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(EffectSourceConditions.GUID, EffectSourceConditions.VERSION)]
    [BepInDependency(RelicCondition.GUID, RelicCondition.VERSION)]
    [BepInDependency(TinyHelper.GUID, TinyHelper.VERSION)]
    [BepInDependency(SynchronizedWorldObjects.GUID, SynchronizedWorldObjects.VERSION)]

    public class RelicKeeper : BaseUnityPlugin
    {
        public static RelicKeeper Instance;
        public const string GUID = "com.ehaugw.relickeeper";
        public const string VERSION = "1.1.2";
        public const string NAME = "Relic Keeper";
        public static string ModFolderName = Directory.GetParent(typeof(RelicKeeper).Assembly.Location).Name.ToString();

        public static SkillSchool RelicKeeperSkillTreeInstance;

        internal void Awake()
        {
            Instance = this;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            HarmalanNPC.Init();

            SL.OnPacksLoaded += OnPacksLoaded;
            DynamicSkillStat.ManaCostModifiers += delegate (Skill skill, float original, ref float result)
            {
                if (new int[] {IDs.useRelicID, IDs.useRelic2ID }.Contains(skill.ItemID) && SkillRequirements.SafeHasSkillKnowledge(skill?.OwnerCharacter, IDs.manaFlowID))
                {
                    result -= original * ManaFlow.ManaCostReduction;
                }
            };
        }

        private void OnPacksLoaded()
        {
            //RELICS
            BasicRelic.MakeItem();
            ObsidianAmulet.MakeItem();
            //AlphaTuanosaurTrinket.MakeItem();
            //GoldLichTalisman.MakeItem();
            WoodooCharm.MakeItem();
            GildedRelic.MakeItem();

            //RELIC RECIPES
            //GoldLichTalisman.MakeRecipe();
            //AlphaTuanosaurTrinket.MakeRecipe();
            //ObsidianAmulet.MakeRecipe();
            WoodooCharm.MakeRecipe();
            BasicRelic.MakeRecipe();
            GildedRelic.MakeRecipe();

            //EFFECTS
            EffectInitializer.MakeDivineInterventionPrefab();
            EffectInitializer.MakeCurseOfVulnrability();
            EffectInitializer.MakeChaosImbue();

            //RELIC SPELLS
            UseRelic.Init(IDs.useRelicID);
            UseRelic.Init(IDs.useRelic2ID);
            Unleash.Init();
            ManaFlow.Init();
            RelicLore.Init();
            MythicLore.Init();
            RelicFundamentals.Init();
            ArcaneInfluence.Init();

            //SPELL TWEAKS
            RadiantSpark.Init();

            RelicKeeperSkillTree.SetupSkillTree(ref RelicKeeperSkillTreeInstance);
        }
    }
}
