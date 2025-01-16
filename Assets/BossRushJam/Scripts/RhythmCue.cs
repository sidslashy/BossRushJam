using UnityEngine;

namespace BossRushJam.Scripts
{
    public class RhythmCue : MonoBehaviour
    {
        [SerializeField] private float scaleFactor = 1.2f;
        [SerializeField] private float duration = 0.2f;

        private Vector3 _initialScale;

        public void Start()
        {
            _initialScale = gameObject.transform.localScale;
        }
        
        
        public void PlayBounce()
        {
            LeanTween.scale(gameObject, _initialScale * scaleFactor, duration*0.5f).setOnComplete(() =>
            {
                LeanTween.scale(gameObject, _initialScale, duration * 0.5f);
            });
        }
        
    }
}