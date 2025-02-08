using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ObserverPattern
{
    public class ListenerManager : BaseManager<ListenerManager>
    {
        private Dictionary<EventId, ListenerGroup> listeners = new();

        public void BroadCast(EventId eventId, Action<object> action)
        {
            if (CommonCondition(eventId))
            {
                listeners[eventId].BroadCast(action);
            }
        }

        public void Register(EventId eventId, Action<object> action)
        {
            if (!listeners.ContainsKey(eventId))
                listeners.Add(eventId, new ListenerGroup());
            if (listeners != null)
                listeners[eventId].Attach(action);
        }

        public void Unregister(EventId eventId, Action<object> action)
        {
            if (CommonCondition(eventId))
            {
                listeners[eventId].Detach(action);
            }
        }

        public void UnregisterAll(Action<object> action)
        {
            foreach (var id in listeners.Keys)
            {
                Unregister(id, action);
            }
        }

        private bool CommonCondition(EventId eventId)
        {
            return listeners.ContainsKey(eventId) && listeners[eventId] != null;
        }
    }
}
