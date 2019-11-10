using System;
using _Game.Source.CS;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Source.Gameplay
{
    public class PlotView : MonoBehaviour
    {
        public Text PlotSize;
        public Text PlotPrice;
        public Text WarehouseCapacity;
        public Text WarehousePrice;
        public RectTransform StorageRatio;
        public Button PlotBuyButton;

        private float _panelWidth;

        private void Awake()
        {
            var parentRect = GetComponent<RectTransform>().rect;
            _panelWidth = parentRect.width;
        }

        public void Init(PlotInfo plotInfo)
        {
            if (plotInfo.IsOwned)
            {
                PlotBuyButton.gameObject.SetActive(false);
            }
        }

        public void UpdateStorageView(PlotInfo plotInfo)
        {
            float xSize = plotInfo.WarehouseStorageRatio * _panelWidth;
            StorageRatio.sizeDelta = new Vector2(xSize, 250f);
        }

        public void UpdateView(PlotInfo plotInfo)
        {
            PlotSize.text = "Size: " + S.CoinStr(plotInfo.PlotSize);
            PlotPrice.text = "Price: " + S.CoinStr(plotInfo.PlotExtendPrice);
            WarehouseCapacity.text = "Capacity: " + S.CoinStr(plotInfo.WarehouseCapacity);
            WarehousePrice.text = "Price: " + S.CoinStr(plotInfo.WarehouseCapacityExtendPrice);
        }
    }
}
