using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.TimeManager
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private float stageDuration = 180f; //3 menit
        [SerializeField] private float startingHour = 7.5f;
        [SerializeField] private float endHour = 18f;

        private float currentTime = 0f;
        private float currentDuration = 0f;
        private TimeSpan timeSpan;

        [Header("UI")]
        [SerializeField] private PanelUIManager panelUIManager;
        [SerializeField] private int updateUIMinute = 10;

        private void Start()
        {
            currentTime = startingHour * 3600;
        }

        private void Update()
        {
            if (currentDuration >= 1f)
            {
                panelUIManager.GameEnd();
                return;
            }

            currentDuration += Time.deltaTime / stageDuration;
            currentTime = Mathf.Lerp(startingHour * 3600, endHour * 3600, currentDuration);

            timeSpan = TimeSpan.FromSeconds(currentTime);
            if (timeSpan.Minutes % updateUIMinute == 0)
            {
                panelUIManager.OnTimeUpdate(currentTime);
            }
        }
    }
}