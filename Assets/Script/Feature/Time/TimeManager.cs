using Group8.TrashDash.Item.Audio;
using Group8.TrashDash.Score;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Group8.TrashDash.TimeManager
{
    public class TimeManager : MonoBehaviour
    {
        public Action OnDayTime;
        public Action OnNightTime;
        public Action OnWaveTick;

        const float timePerDay = 24 * 60 * 60;

        [Header("Level")]
        [SerializeField] private float stageDuration = 180f; //3 menit
        [SerializeField] private float startingHour = 7.5f;
        [SerializeField] private float endHour = 18f;

        private float currentTime = 0f;
        private float currentDuration = 0f;
        private TimeSpan timeSpan;

        public float CurrentTime { get => currentTime; }
        public float TimePerDay { get => timePerDay; }

        [Header("UI")]
        [SerializeField] private PanelUIManager panelUIManager;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private int updateUIMinute = 10;

        [Header("Indicator")]
        [SerializeField] private float dayTime = 6f;
        [SerializeField] private float nightTime = 18f;
        [SerializeField] private int lastSecondsCoundown = 5;

        public bool isNightTime => (timeSpan.Hours >= nightTime) || (timeSpan.Hours <= dayTime);
        private bool prevTimeCheck;

        // AI Spawn
        [Header("AI Spawn")]
        public int WavePerHour = 1;
        [SerializeField] private int lastWaveSpawnTime = 19;

        private Coroutine lastCountCoroutine;

        private bool[] callOnce;

        private void Start()
        {
            currentTime = startingHour * 3600;
            timeSpan = TimeSpan.FromSeconds(currentTime);
            panelUIManager.OnTimeUpdate(currentTime);

            prevTimeCheck = !isNightTime;
            callOnce = new bool[2] {true, true};
        }

        private void Update()
        {
            if (currentDuration >= 1f)
            {
                currentDuration = -1f;
                panelUIManager.GameEnd();
                scoreManager.GameEnd();
                return;
            }

            if (currentDuration == -1f) return;

            currentDuration += Time.deltaTime / stageDuration;
            currentTime = Mathf.Lerp(startingHour * 3600, endHour * 3600, currentDuration);

            timeSpan = TimeSpan.FromSeconds(currentTime);
            if (timeSpan.Minutes % updateUIMinute == 0)
            {
                if (callOnce[0])
                    panelUIManager.OnTimeUpdate(currentTime);

                callOnce[0] = false;
            }
            else callOnce[0] = true;

            if (timeSpan.Minutes % (60 / WavePerHour) == 0)
            {
                if (callOnce[1] && timeSpan.Hours <= lastWaveSpawnTime)
                    OnWaveTick?.Invoke();

                callOnce[1] = false;
            }
            else callOnce[1] = true;

            if (currentDuration >= (stageDuration - lastSecondsCoundown) / stageDuration && lastCountCoroutine == null)
            {
                lastCountCoroutine = StartCoroutine(StartCountdown(lastSecondsCoundown));
            }

            if (prevTimeCheck != isNightTime)
            {
                prevTimeCheck = isNightTime;

                if (isNightTime) OnNightTime?.Invoke();
                else OnDayTime?.Invoke();
            }
        }

        private IEnumerator StartCountdown(int num)
        {
            int count = num;
            while (count > 0)
            {
                audioSource.Play();
                yield return new WaitForSeconds(1);
                count--;
            }
        }
    }
}