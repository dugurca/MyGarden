using System;
using System.Collections.Generic;

namespace _Game.Source.Microservices
{
    public sealed class Micros
    {
        private static readonly Dictionary<Type, IMicro> MicrosDict = new Dictionary<Type, IMicro>();

        public static void Init()
        {
            MicrosDict.Add(typeof(SaveMicro), new SaveMicro());
            MicrosDict.Add(typeof(AudioMicro), new AudioMicro());

            foreach (var micro in MicrosDict)
            {
                micro.Value.Init();
            }
        }

        private static T GetMicro<T>()
        {
            return (T) MicrosDict[typeof(T)];
        }

        public static SaveMicro GetSaveMicro()
        {
            return GetMicro<SaveMicro>();
        }

        public static AudioMicro GetAudioMicro()
        {
            return GetMicro<AudioMicro>();
        }
    }
}