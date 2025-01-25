using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BossRushJam.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float beatInputBuffer = 0.3f;

        private float _lastBeatTime;

        private Action _lastAction;
        private float _lastActionTriggerTime;
        private bool _isBeatChecked;


        [SerializeField] private UnityEvent moveLeft;
        [SerializeField] private UnityEvent moveRight;
        [SerializeField] private UnityEvent beatMissed;
        
        public void OnBeatTick()
        {
            _isBeatChecked = false;
            _lastBeatTime = Time.time;

            // If the player hits input before the beat.
            if (CheckBeatElapsedTime())
            {
                _lastAction?.Invoke();
                _lastAction = null;
                _isBeatChecked = true;
            }
            
        }

        private void Update()
        {
            if (!_isBeatChecked)
            {
                if (CheckBeatElapsedTime())
                {
                    _lastAction?.Invoke();
                    _lastAction = null;
                }
                else
                {
                    //Debug.Log($"Time Elapsed: {Mathf.Abs(_lastBeatTime - _lastActionTriggerTime)}");
                    Debug.Log("Beat Missed");
                    beatMissed.Invoke();
                    _lastAction?.Invoke();
                    _lastAction = null;
                }

                _isBeatChecked = true;
            }
        }

        private bool CheckBeatElapsedTime()
        {
            return Mathf.Abs(_lastBeatTime - _lastActionTriggerTime) < beatInputBuffer;
        }
        

        public void OnAttack()
        {
            CheckAndPerformAction(PerformAttack);
        }
        public void OnRight()
        {
            CheckAndPerformAction(PerformRight);
        }
        public void OnLeft()
        {
            CheckAndPerformAction(PerformLeft);
        }

        private void CheckAndPerformAction(Action action)
        {
            if (_lastAction != null)
            {
                //_lastAction?.Invoke();
                _lastAction = null;
                Debug.Log("Beat Missed");
                beatMissed.Invoke();
            }
            else
            {
                _lastAction = action;
                _lastActionTriggerTime = Time.time;
            }


        }
        
        private void PerformAttack()
        {
            Debug.Log("Attack Pressed!");
        }

        private void PerformRight()
        {
            Debug.Log("Right Pressed!");
            moveRight.Invoke();
        }

        private void PerformLeft()
        {
            Debug.Log("Left Pressed!");
            moveLeft.Invoke();
        }
    }
}