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

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(EffectSourceConditions.GUID, EffectSourceConditions.VERSION)]
    [BepInDependency(TinyHelper.GUID, TinyHelper.VERSION)]
    [BepInDependency(SynchronizedWorldObjects.GUID, SynchronizedWorldObjects.VERSION)]

    public class RelicKeeper : BaseUnityPlugin
    {
        public static RelicKeeper Instance;
        public const string GUID = "com.ehaugw.relickeeper";
        public const string VERSION = "1.0.0";
        public const string NAME = "Relic Keeper";
        public static string ModFolderName = Directory.GetParent(typeof(RelicKeeper).Assembly.Location).Name.ToString();

        public StatusEffect channelRelicStatusEffectInstance;
        public StatusEffect divineInterventionStatusEffectInstance;
        public StatusEffect relicProtectionEffectInstance;

        public static SkillSchool RelicKeeperSkillTreeInstance;

        internal void Awake()
        {
            Instance = this;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            HarmalanNPC.Init();

            SL.OnPacksLoaded += OnPacksLoaded;
            //SL.OnSceneLoaded += OnSceneLoadedEquipment;
        }
        private void OnPacksLoaded()
        {
            //RELICS
            BasicRelic.MakeItem();
            ObsidianAmulet.MakeItem();
            AlphaTuanosaurTrinket.MakeItem();
            GoldLichTalisman.MakeItem();
            WoodooCharm.MakeItem();

            //RELIC RECIPES
            GoldLichTalisman.MakeRecipe();
            AlphaTuanosaurTrinket.MakeRecipe();
            ObsidianAmulet.MakeRecipe();

            //EFFECTS
            channelRelicStatusEffectInstance = EffectInitializer.MakeChannelRelicPrefab();
            divineInterventionStatusEffectInstance = EffectInitializer.MakeDivineInterventionPrefab();
            relicProtectionEffectInstance = EffectInitializer.MakeRelicProtectionPrefab();

            //RELIC SPELLS
            UseRelic.Init(IDs.useRelicID);
            UseRelic.Init(IDs.useRelic2ID);
            Unleash.Init();
            ManaFlow.Init();
            RelicLore.Init();
            MythicLore.Init();
            Overchannel.Init();
            ArcaneInfluence.Init();

            RelicKeeperSkillTree.SetupSkillTree(ref RelicKeeperSkillTreeInstance);
        }

        //private void OnSceneLoadedEquipment()
        //{
        //    foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "HumanSNPC_Blacksmith" && (x.GetComponentInChildren<Merchant>()?.ShopName ?? "") == "Vyzyrinthrix the Blacksmith"))
        //    {
        //        if (obj.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipeTableBlacksmith)
        //        {
        //            if (SideLoader.At.GetField<GuaranteedDrop>(recipeTableBlacksmith, "m_itemDrops") is List<BasicItemDrop> drops)
        //            {
        //                foreach (Item item in new Item[] { })
        //                {
        //                    //Used to say DroppedItem = item
        //                    drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
        //                }
        //            }
        //        }
        //    }

        //    if (GameObject.Find("UNPC_LaineAberforthA")?.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipeTableLaine)
        //    {
        //        if (SideLoader.At.GetField<GuaranteedDrop>(recipeTableLaine, "m_itemDrops") is List<BasicItemDrop> drops)
        //        {
        //            foreach (Item item in new Item[] { })
        //            {
        //                //Used to say DroppedItem = item
        //                drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
        //            }
        //        }
        //    }

        //    foreach (var transform in new GameObject[] { GameObject.Find("UNPC_MathiasA (1)"), GameObject.Find("UNPC_MathiasA (2)") })
        //    {
        //        if (transform?.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipeTableMathias)
        //        {
        //            if (SideLoader.At.GetField<GuaranteedDrop>(recipeTableMathias, "m_itemDrops") is List<BasicItemDrop> drops)
        //            {
        //                foreach (Item item in new Item[] { talismanOfRecoveryInstance, alphaTuanosaurTrinketInstance, obsidianAmuletInstance })
        //                {
        //                    //Used to say DroppedItem = item
        //                    drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
        //                }
        //            }
        //        }
        //    }
        //}
    }
}