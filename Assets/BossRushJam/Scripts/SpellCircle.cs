using UnityEngine;

namespace BossRushJam.Scripts
{
    public class SpellCircle : MonoBehaviour
    {
        [SerializeField] private float effectDuration = 0.2f;
        [SerializeField] private int sides = 6;

        private float _stepAngle;
        private int _stepCount;

        private void Start()
        {
            _stepAngle = 360f / sides;
        }


        public void MoveRight()
        {
            _stepCount++;
            transform.Rotate(0,0,-_stepAngle);
        }

        public void MoveLeft()
        {
            _stepCount--;
            transform.Rotate(0,0,_stepAngle);
        }

        public void BeatMissed()
        {
            LeanTween.color(gameObject, Color.red, effectDuration * 0.5f).setOnComplete(() =>
            {
                var color =  new Color(1, 1, 1, 0.7f);;
               
                LeanTween.color(gameObject, color, effectDuration * 0.5f);
            });
        }

    }
}