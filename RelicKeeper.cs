namespace RelicKeeper
{
    using TinyHelper;
    using EffectSourceConditions;
    using BepInEx;
    using SideLoader;
    using UnityEngine;
    using HarmonyLib;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInDependency(SL.GUID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency(EffectSourceConditions.GUID, EffectSourceConditions.VERSION)]
    [BepInDependency(TinyHelper.GUID, TinyHelper.VERSION)]
    
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

        public Item obsidianAmuletInstance;
        public Item alphaTuanosaurTrinketInstance;
        public Item talismanOfRecoveryInstance;
        public Item woodooCharmInstance;
        public Item basicRelicInstance;
        public Item talismanOfRecoveryRecipeInstance;
        public Item obsidianAmuletRecipeInstance;
        public Item alphaTuanosaurTrinketRecipeInstance;


        public Skill useRelicInstance;
        public Skill unleashInstance;
        public Skill channelRelicInstance;
        public Skill manaFlowInstance;

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            SL.OnPacksLoaded += OnPacksLoaded;
            //SL.OnSceneLoaded += OnSceneLoadedEquipment;
        }
        private void OnPacksLoaded()
        {
            Instance = this;
            
            var rpcGameObject = new GameObject("RelicKeeperRPC");
            DontDestroyOnLoad(rpcGameObject);
            rpcGameObject.AddComponent<RPCManager>();

            //RELICS
            basicRelicInstance = BasicRelic.MakeItem();
            obsidianAmuletInstance = ObsidianAmulet.MakeItem();
            alphaTuanosaurTrinketInstance = AlphaTuanosaurTrinket.MakeItem();
            talismanOfRecoveryInstance = GoldLichTalisman.MakeItem();
            woodooCharmInstance = WoodooCharm.MakeItem();

            //RELIC RECIPES
            talismanOfRecoveryRecipeInstance = GoldLichTalisman.MakeRecipe();
            alphaTuanosaurTrinketRecipeInstance = AlphaTuanosaurTrinket.MakeRecipe();
            obsidianAmuletRecipeInstance = ObsidianAmulet.MakeRecipe();

            //EFFECTS
            channelRelicStatusEffectInstance = EffectInitializer.MakeChannelRelicPrefab();
            divineInterventionStatusEffectInstance = EffectInitializer.MakeDivineInterventionPrefab();
            relicProtectionEffectInstance = EffectInitializer.MakeRelicProtectionPrefab();

            //RELIC SPELLS
            useRelicInstance = UseRelic.Init();
            channelRelicInstance = ChannelRelic.Init();
            unleashInstance= Unleash.Init();
            manaFlowInstance= ManaFlow.Init();
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