using InstanceIDs;
using SideLoader;
using UnityEngine;

namespace RelicKeeper
{
    using NodeCanvas.Framework;
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
                defaultEquipment: new int[] { IDs.whiteWideHatID, IDs.whiteArcaneRobeID, IDs.desertBootsID, IDs.basicRelicID, IDs.lightMenderBackpackID},
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
                position: new Vector3(-649.0438f, -1575.157f, 741.9805f),
                rotation: new Vector3(0, 26.5f, 0)
            ));
        }

        public HarmalanNPC(string identifierName, int rpcListenerID, int[] defaultEquipment = null, int[] moddedEquipment = null, Vector3? scale = null, Character.Factions? faction = null, SL_Character.VisualData visualData = null) : 
            base(identifierName, rpcListenerID, defaultEquipment: defaultEquipment, moddedEquipment: moddedEquipment, scale: scale, faction: faction, visualData: visualData)
        { }

        override public object SetupClientSide(int rpcListenerID, string instanceUID, int sceneViewID, int recursionCount, string rpcMeta)
        {
            Character instanceCharacter = base.SetupClientSide(rpcListenerID, instanceUID, sceneViewID, recursionCount, rpcMeta) as Character;
            if (instanceCharacter == null) return null;

            GameObject instanceGameObject = instanceCharacter.gameObject;
            var trainerTemplate = TinyDialogueManager.AssignTrainerTemplate(instanceGameObject.transform);
            var actor = TinyDialogueManager.SetDialogueActorName(trainerTemplate, IdentifierName);
            var trainerComp = TinyDialogueManager.SetTrainerSkillTree(trainerTemplate, RelicKeeper.RelicKeeperSkillTreeInstance.UID);
            var graph = TinyDialogueManager.GetDialogueGraph(trainerTemplate);
            TinyDialogueManager.SetActorReference(graph, actor);
            graph.allNodes.Clear();

            //Actions
            var openTrainer = TinyDialogueManager.MakeTrainDialogueAction(graph, trainerComp);

            //NPC statements
            var rootStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "What do you want, peasant?");
            var everyoneKnowsMeStatement = TinyDialogueManager.MakeStatementNode(graph, IdentifierName, "Hah! Like you don't know... Everyone knows me, I'm a living legend known as \"The Juggernaut\"!");

            //Player statements
            var requestTrainingText = "I wish to become a legend like you!";
            var whoAreYouText = "Who are you?";

            //Player choices
            var introMultipleChoice = TinyDialogueManager.MakeMultipleChoiceNode(graph, new string[] { whoAreYouText, requestTrainingText, });

            graph.primeNode = rootStatement;

            ////inject compliment about killing wendigo if first time talking
            TinyDialogueManager.ChainNodes(graph, new Node[] { rootStatement, introMultipleChoice });
            TinyDialogueManager.ConnectMultipleChoices(graph, introMultipleChoice, new Node[] { everyoneKnowsMeStatement, openTrainer });

            var obj = instanceGameObject.transform.parent.gameObject;
            obj.SetActive(true);

            return instanceCharacter;
        }
    }
}