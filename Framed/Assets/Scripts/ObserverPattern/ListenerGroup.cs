using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ObserverPattern
{
    public class ListenerGroup
    {
        List<Action<object>> actions = new();

        public void BroadCast(object value)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i](value);
            }
        }

        public void Attach(Action<object> action)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] == action)
                    return;
            }
        }

        public void Detach(Action<object> action)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] == action)
                {
                    actions.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
