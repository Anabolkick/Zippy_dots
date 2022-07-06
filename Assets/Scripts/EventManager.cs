using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnScoreIncreasedEvent = new UnityEvent();
    public static UnityEvent OnLoseEvent = new UnityEvent();
}
