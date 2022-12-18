namespace RelicKeeper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using SideLoader;
    using InstanceIDs;

    class ChannelRelicEffect : Effect
    {
        bool buffsWereReceived = false;
        const float DELAY = 3;
        
        protected override void ActivateLocally(Character _affectedCharacter, object[] _infos)
        {
            //if ((_affectedCharacter?.Animator?.velocity != null) && (_affectedCharacter.Animator.velocity.sqrMagnitude > 0.1) && this.m_parentStatusEffect.Age > 1)
            bool cleanse = false;
            
            if (this.m_parentStatusEffect is StatusEffect parent)
            {
                if (parent.Age > DELAY+0.5 && !buffsWereReceived)
                {
                    
                }

                if ((_affectedCharacter?.AnimMoveSqMagnitude ?? 0) > 0.1 && parent.Age > 1f)
                {
                    cleanse = true;
                }
            } else
            {
                cleanse = true;
            }
            if (cleanse)
            {
                _affectedCharacter.StatusEffectMngr?.RemoveStatusWithIdentifierName(RelicKeeper.Instance.channelRelicStatusEffectInstance.IdentifierName);
                RPCManager.Instance.photonView.RPC("CharacterForceCancelRPC", PhotonTargets.All, new object[] { _affectedCharacter.UID.ToString(), true, true});
            }
        }
    }
}
