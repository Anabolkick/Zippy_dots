using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnScoreChangedEvent = new UnityEvent();
    public static UnityEvent OnLoseEvent = new UnityEvent();
    public static UnityEvent OnSoundModeChangedEvent = new UnityEvent();
    public static UnityEvent OnDirectionChangedEvent = new UnityEvent();
}
