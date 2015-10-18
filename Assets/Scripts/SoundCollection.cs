using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class SoundCollection : MonoBehaviour
    {
        public AudioClip[] AudioClips;
        [SerializeField] private float[] _probabilities;
    
        private float[] _normalizedProbabilities;

        private void GenerateNormalizedProbabilities()
        {
            float totalProbability = 0;
            for (int i = 0; i < _probabilities.Length; i++)
            {
                totalProbability += _probabilities[i];
            }
            _normalizedProbabilities = new float[_probabilities.Length];
            for (int i = 0; i < _probabilities.Length; i++)
            {
                _normalizedProbabilities[i] = _probabilities[i] / totalProbability;
            }
        }

        public AudioClip GetRandomClip()
        {
            if (_normalizedProbabilities == null || _normalizedProbabilities.Length == 0)
            {
                GenerateNormalizedProbabilities();
            }

            float r = Random.value;
            int index = -1;
            float t = 0;
            for (int i = 0; i < _normalizedProbabilities.Length; i++)
            {
                if (t + _normalizedProbabilities[i] > r)
                {
                    index = i;
                    break;
                }
                t += _normalizedProbabilities[i];
            }
            if (index == -1) return AudioClips.Last();

            return AudioClips[index];
        }

        public void PlayRandomSound(AudioSource audioSource)
        {
            AudioClip randomClip = GetRandomClip();
            audioSource.PlayOneShot(randomClip, 2f);
        }
    }
}