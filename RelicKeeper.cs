namespace RelicKeeper
{
    using BepInEx;
    using SideLoader;
    using UnityEngine;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class RelicKeeper : BaseUnityPlugin
    {
        public static RelicKeeper Instance;
        public const string GUID = "com.ehaugw.relickeeper";
        public const string VERSION = "1.0.0";
        public const string NAME = "Relic Keeper";
        public const string ModFolderName = "RelicKeeper";

        public StatusEffect channelRelicStatusEffectInstance;

        public Skill useRelicInstance;
        public Skill channelRelicInstance;
        internal void Awake()
        {
            //var harmony = new Harmony(GUID);
            //harmony.PatchAll();

            SL.OnPacksLoaded += OnPacksLoaded;
        }
        private void OnPacksLoaded()
        {
            Instance = this;
            
            var rpcGameObject = new GameObject("RelicKeeperRPC");
            DontDestroyOnLoad(rpcGameObject);
            rpcGameObject.AddComponent<RPCManager>();

            channelRelicStatusEffectInstance = EffectInitializer.MakeChannelRelicPrefab();

            useRelicInstance = UseRelic.Init();
            channelRelicInstance = ChannelRelic.Init();
        }
    }
}