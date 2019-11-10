using System.Collections.Generic;
using _Game.Source.CS;
using UnityEngine;

namespace _Game.Source.SO
{
    [CreateAssetMenu(fileName = "AudioDict", menuName = "Create Audio Dictionary")]
    public class AudioDict : ScriptableObject
    {
        public ClipName[] ClipNames;
        public AudioClip[] AudioClips;

        private static Dictionary<ClipName, AudioClip> _clipName2AudioClipDict;

#if UNITY_EDITOR
        private void Awake()
        {
            Debug.Assert(ClipNames.Length > 0);
            Debug.Assert(AudioClips.Length > 0);
            Debug.Assert(ClipNames.Length == AudioClips.Length);
        }
#endif

        public Dictionary<ClipName, AudioClip> GetAudioDict()
        {
            if (_clipName2AudioClipDict == null)
            {
                InitDict();
            }

            return _clipName2AudioClipDict;
        }

        private void InitDict()
        {
            int numClips = ClipNames.Length;
            _clipName2AudioClipDict = new Dictionary<ClipName, AudioClip>(numClips);

            for (int i = 0; i < numClips; i++)
            {
                _clipName2AudioClipDict.Add(ClipNames[i], AudioClips[i]);
            }
        }
    }
}