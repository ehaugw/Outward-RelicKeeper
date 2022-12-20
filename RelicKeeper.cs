namespace RelicKeeper
{
    using TinyHelper;
    using EffectSourceConditions;
    using BepInEx;
    using SideLoader;
    using UnityEngine;
    using HarmonyLib;

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
        public const string ModFolderName = "RelicKeeper";

        public StatusEffect channelRelicStatusEffectInstance;
        public StatusEffect divineInterventionStatusEffectInstance;

        public Skill useRelicInstance;
        public Skill unleashInstance;
        public Skill channelRelicInstance;
        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            SL.OnPacksLoaded += OnPacksLoaded;
        }
        private void OnPacksLoaded()
        {
            Instance = this;
            
            var rpcGameObject = new GameObject("RelicKeeperRPC");
            DontDestroyOnLoad(rpcGameObject);
            rpcGameObject.AddComponent<RPCManager>();

            channelRelicStatusEffectInstance = EffectInitializer.MakeChannelRelicPrefab();
            divineInterventionStatusEffectInstance = EffectInitializer.MakeDivineInterventionPrefab();

            useRelicInstance = UseRelic.Init();
            channelRelicInstance = ChannelRelic.Init();
            unleashInstance= Unleash.Init();
        }
    }
}