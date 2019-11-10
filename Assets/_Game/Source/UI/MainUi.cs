using System;
using _Game.Source.CS;
using _Game.Source.Microservices;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.UI
{
    public class MainUi : MonoBehaviour
    {
        public Text CoinText;

        public void UpdateView()
        {
            double numCoins = Micros.GetSaveMicro().GetCoins();
            CoinText.text = "Coins: " + S.CoinStr(numCoins);
        }
    }
}
