using UnityEngine;
using UnityEngine.Events;
using System;   

public class EventManager : MonoBehaviour
{
    public static Action onCollectAction;
    public static Action<GameObject> onCollectActionGameobject;
    public static Action<GameObject> onCollisionSection;
}
