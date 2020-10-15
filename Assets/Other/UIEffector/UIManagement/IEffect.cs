using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UIManagement
{
    public interface IEffecter
    {
        void Play(Interactable.InteractionState interactionState, bool instant, bool resetEarlier);
    }
}
