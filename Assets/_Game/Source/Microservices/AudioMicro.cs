using _Game.Source.CS;
using _Game.Source.SO;
using UnityEngine;

namespace _Game.Source.Microservices
{
    public class AudioMicro : IMicro
    {
        private AudioDict _audDict;
        private readonly AudioSource _audSource;
        
        public AudioMicro()
        {
            var audObject = new GameObject("AudioMicro");
            _audSource = audObject.AddComponent<AudioSource>();
        }
        
        public void Init()
        {
            _audDict = Resources.Load("AudioDict", typeof(ScriptableObject)) as AudioDict;
        }

        public void Play(ClipName name)
        {
            var audioDict = _audDict.GetAudioDict();
            _audSource.PlayOneShot(audioDict[name]);
        }
    }
}