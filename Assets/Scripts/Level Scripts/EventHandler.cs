using UnityEngine.Events;

namespace Events
{
    public static class EventHandler
    {
        public static UnityEvent<int> OnEvilLevelChanged = new UnityEvent<int>();
    }
}