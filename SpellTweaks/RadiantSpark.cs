using TinyHelper;
using InstanceIDs;

namespace CrusadersEquipment
{
    using ImpendingDoom;
    using EffectSourceConditions;

    public class RadiantSpark
    {
        public static void Init()
        {
            var spark = ResourcesPrefabManager.Instance.GetItemPrefab(IDs.sparkID) as Skill;
            var damagingBlast = spark.transform.Find("Effects").gameObject.GetComponents<ShootBlast>()[0].BaseBlast;

            var extraEffects = TinyGameObjectManager.MakeFreshObject(IDs.EFFECTS_CONTAINER, true, true, damagingBlast.transform).transform;
            var addThenSpread = extraEffects.gameObject.AddComponent<AddThenSpreadStatus>();
            addThenSpread.Status = ImpendingDoomMod.Instance.impendingDoomInstance;
            addThenSpread.Range = ImpendingDoom.RANGE * 0.5f;

            var requirementTransform = TinyGameObjectManager.GetOrMake(addThenSpread.transform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            var relicReq = requirementTransform.gameObject.AddComponent<SourceConditionEquipment>();
            relicReq.RequiredItemID = IDs.lightMenderLexiconID;

            var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionSkill>();
            skillReq.RequiredSkillID = IDs.arcaneInfluenceID;

            //need two stacks because one is consumed
            //var requirementTransform = TinyGameObjectManager.GetOrMake(addThenSpread.transform, EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            //var statusReq = requirementTransform.gameObject.AddComponent<SourceConditionStatusEffect>();
            //statusReq.RequiredStatusEffect = Crusader.Instance.burstOfDivinityInstance;
        }
    }
}
