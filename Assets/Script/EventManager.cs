using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public delegate void OnCollect();
    public static event OnCollect OnCollectEvent;

    public static UnityAction<> smh;
}
