using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Group8.TrashDash.TimeManager
{
    public class ToggleWhenTime : MonoBehaviour
    {
        private TimeManager timeManager;

        public UnityEvent OnDayTime;
        public UnityEvent OnNightTime;

        private void Awake()
        {
            timeManager = FindObjectOfType<TimeManager>();
        }

        private void OnEnable()
        {
            timeManager.OnDayTime += () => OnDayTime.Invoke();
            timeManager.OnNightTime += () => OnNightTime.Invoke();
        }

        private void OnDestroy()
        {
            timeManager.OnDayTime -= () => OnDayTime.Invoke();
            timeManager.OnNightTime -= () => OnNightTime.Invoke();
        }
    }
}