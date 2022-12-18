using InstanceIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RelicKeeper
{
    public class RPCManager : Photon.MonoBehaviour
    {
        public static RPCManager Instance;

        internal void Start()
        {
            Instance = this;

            var view = this.gameObject.AddComponent<PhotonView>();
            view.viewID = IDs.RelicKeeperRPCPhotonID;
            Debug.Log("Registered RelicKeeperRPC with ViewID " + this.photonView.viewID);
        }

        [PunRPC]
        public void CharacterForceCancelRPC(string characterGUID, bool bool1, bool bool2)
        {
            Character character = CharacterManager.Instance.GetCharacter(characterGUID);
            character.ForceCancel(bool1, bool2);
        }
    }
}
