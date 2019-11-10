using System;
using _Game.Source.CS;
using _Game.Source.Microservices;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Gameplay
{
    public class PlotController : MonoBehaviour
    {
        [SerializeField] private UiButtons _uiButtons;
        
        private int _plotId;
        private Game _game;

        public void Init(Game game, PlotInfo info)
        {
            _game = game;
            _plotId = info.Id;

            _uiButtons.BuyPlotButton.GetComponentInChildren<Text>().text = 
                "BUY " + info.Name + " PLOT (Price: " + S.CoinStr(info.PlotInitialCost) + ")";
            
            AssignButtons();

            var sellButtonText = _uiButtons.SellButton.GetComponentInChildren<Text>();
            sellButtonText.text = "SELL " + info.Name;
        }

        private void AssignButtons()
        {
            _uiButtons.BuyPlotButton.onClick.AddListener(() =>
            {
                _game.BuyPlot(_plotId);
            });
            
            _uiButtons.ExtendPlotButton.onClick.AddListener(() =>
            {
                bool success = _game.ExtendPlot(_plotId);
                if (success)
                {
                    Micros.GetAudioMicro().Play(ClipName.BuyClip);
                }
            });
            
            _uiButtons.ExtendWarehouseButton.onClick.AddListener(() =>
            {
                bool success = _game.ExtendWarehouse(_plotId);
                if (success)
                {
                    Micros.GetAudioMicro().Play(ClipName.BuyClip);
                }
            });
            
            _uiButtons.SellButton.onClick.AddListener(() =>
            {
                _game.Sell(_plotId);
                Micros.GetAudioMicro().Play(ClipName.SellClip);
            });
        }

        private void OnDestroy()
        {
            _uiButtons.BuyPlotButton.onClick.RemoveAllListeners();
            _uiButtons.ExtendPlotButton.onClick.RemoveAllListeners();
            _uiButtons.ExtendWarehouseButton.onClick.RemoveAllListeners();
            _uiButtons.SellButton.onClick.RemoveAllListeners();
        }

        [Serializable]
        private struct UiButtons
        {
            public Button BuyPlotButton;
            public Button ExtendPlotButton;
            public Button ExtendWarehouseButton;
            public Button SellButton;
        }
    }
}
