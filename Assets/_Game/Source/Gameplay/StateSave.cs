using _Game.Source.Microservices;
using UnityEngine;

namespace _Game.Source.Gameplay
{
    public class StateSave : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            Micros.GetSaveMicro().Save();
        }
    }
}
