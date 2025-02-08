using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ObserverPattern
{
    public class EventReceiver : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI m_CountText;

        private int m_Count = 0;

        private void Start() { }
    }
}
