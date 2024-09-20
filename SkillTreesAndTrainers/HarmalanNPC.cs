using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace RelicKeeper
{
    using SynchronizedWorldObjects;
    using System.Linq;
    using TinyHelper;

    public class HarmalanNPC : SynchronizedNPC
    {
        public static void Init()
        {
            var syncedNPC = new HarmalanNPC(
                identifierName: "Harmalan",
                rpcListenerID: IDs.NPCID_Harmalan,
                defaultEquipment: new int[] { IDs.krypteiaHoodID, IDs.krypteiaBootsID, IDs.krypteiaArmorID, IDs.wolfSwordID },
                visualData: new SL_Character.VisualData()
                {
                    Gender = Character.Gender.Male,
                    HeadVariationIndex = (int)SL_Character.Ethnicities.Asian,
                    HairStyleIndex = 0,
                    HairColorIndex = 0,
                }
            );

            syncedNPC.AddToScene(new SynchronizedNPCScene(
                scene: "Berg",
                position: new Vector3(1213.116f, -13.7223f, 1375.415f),
                rotation: new Vector3(0, 284f, 0)
            ));
        }

        public HarmalanNPC(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
            base(identifierName, rpcListenerID, defaultEquipment: defaultEquipment, moddedEquipment: moddedEquipment, scale: scale, faction: faction, visualData: visualData)
        { }

        //override public object SetupClientSide(int rpcListenerID, string instanceUID, int sceneViewID, int recursionCount, string rpcMeta)
        //{
        //    //Character player = GameObject.FindObjectsOfType<Character>().Where(x => x.IsLocalPlayer).First();
        //    //Character instanceCharacter = base.SetupClientSide(rpcListenerID, instanceUID, sceneViewID, recursionCount, rpcMeta) as Character;
        //    //if (instanceCharacter == null) return null;

        //    //GameObject instanceGameObject = instanceCharacter.gameObject;
        //    //var trainerTemplate = TinyDialogueManager.AssignTrainerTemplate(instanceGameObject.transform);
        //    //var actor = TinyDialogueManager.SetDialogueActorName(trainerTemplate, IdentifierName);
        //    //var trainerComp = TinyDialogueManager.SetTrainerSkillTree(trainerTemplate, RelicKeeperSkillTree.RelicKeeperSkillSchool.UID);
        //    //var graph = TinyDialogueManager.GetDialogueGraph(trainerTemplate);
        //    //TinyDialogueManager.SetActorReference(graph, actor);

        //    //var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Have you came to honor the memories of our ancestors?");
        //    //var characterIntroduction = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "That is classified. All I can tell you is that I am Ignacio.");

        //    //var openTrainer = TinyDialogueManager.MakeTrainDialogueAction(graph, trainerComp);

        //    //var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] {
        //    //    "You mean the people responsible for my blood dept? Well, no... Who are you anyways?",
        //    //    "I am here to receive training."
        //    //});

        //    //graph.allNodes.Clear();
        //    //graph.allNodes.Add(rootStatement);
        //    //graph.allNodes.Add(introMultipleChoice);
        //    //graph.allNodes.Add(characterIntroduction);
        //    //graph.allNodes.Add(openTrainer);

        //    //graph.primeNode = rootStatement;
        //    //graph.ConnectNodes(rootStatement, introMultipleChoice);
        //    //graph.ConnectNodes(introMultipleChoice, characterIntroduction, 0);
        //    //graph.ConnectNodes(introMultipleChoice, openTrainer, 1);
        //    //graph.ConnectNodes(characterIntroduction, rootStatement);

        //    //var obj = instanceGameObject.transform.parent.gameObject;
        //    //obj.SetActive(true);

        //    return instanceCharacter;
        //}
    }
}