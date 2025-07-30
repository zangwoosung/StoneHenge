using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace ImprovedTimers {
    public static class TimerManager {
        static readonly List<Timer> timers = new();

        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeregisterTimer(Timer timer) => timers.Remove(timer);

        static void UpdateTimers() {
            foreach (var timer in new List<Timer>(timers)) {
                timer.Tick(Time.deltaTime);
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void InsertTimerSystemIntoPlayerLoop() {
            var playerLoop = PlayerLoop.GetDefaultPlayerLoop();

            for (int i = 0; i < playerLoop.subSystemList.Length; i++) {
                if (playerLoop.subSystemList[i].type == typeof(Update)) {
                    var updateSubsystems = playerLoop.subSystemList[i].subSystemList;
                    var newSubsystemList = new List<PlayerLoopSystem>(updateSubsystems);

                    var timerLoopSystem = new PlayerLoopSystem {
                        type = typeof(TimerManager),
                        updateDelegate = TimerManager.UpdateTimers
                    };

                    newSubsystemList.Insert(0, timerLoopSystem);
                    playerLoop.subSystemList[i].subSystemList = newSubsystemList.ToArray();
                    break;
                }
            }

            PlayerLoop.SetPlayerLoop(playerLoop);
        }
    }

    public abstract class Timer : IDisposable {
        protected float initialTime;
        public float Time { get; set; }
        public bool IsRunning { get; protected set; }

        public float Progress => Time / initialTime;

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value) {
            initialTime = value;
            IsRunning = false;
        }

        public void Start() {
            Time = initialTime;
            if (!IsRunning) {
                IsRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (IsRunning) {
                IsRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop.Invoke();
            }
        }

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public abstract void Tick(float deltaTime);

        bool isDisposed;

        ~Timer() {
            Dispose(false);
        }

        // Call Dispose to ensure deregistration of the timer from the TimerManager
        // when the consumer is done with the timer or being destroyed
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (isDisposed) return;

            if (disposing) {
                if (IsRunning) {
                    TimerManager.DeregisterTimer(this);
                    IsRunning = false;
                }
            }

            isDisposed = true;
        }
    }

    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime) {
            if (IsRunning && Time > 0) {
                Time -= deltaTime;
            }

            if (IsRunning && Time <= 0) {
                Stop();
            }
        }

        public bool IsFinished => Time <= 0;

        public void Reset() => Time = initialTime;

        public void Reset(float newTime) {
            initialTime = newTime;
            Reset();
        }
    }
}