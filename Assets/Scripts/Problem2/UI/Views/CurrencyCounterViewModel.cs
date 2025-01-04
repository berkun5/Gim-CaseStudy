namespace Gimica.ProblemTwo
{
    using System;
    using UnityEngine;
    
    public class CurrencyCounterViewModel : ICurrencyCounterViewModel
    {
        IReactiveProperty<string> ICurrencyCounterViewModel.CurrencyLabel => _currencyLabel;
        Color ICurrencyCounterViewModel.CurrencyTextColor => _currencyTextColor;
        Sprite ICurrencyCounterViewModel.CurrencyIcon => _currencyIcon;

        private readonly CurrencyType _currencyType;
        private readonly Color _currencyTextColor;
        private readonly Sprite _currencyIcon;
        private readonly CurrencyModel _currencyModel;
        private readonly CurrencyData _currencyData;
        
        private readonly ReactiveProperty<string> _currencyLabel = new();
        
        public CurrencyCounterViewModel(CurrencyType currencyType, CurrencyData currencyData, CurrencyModel currencyModel)
        {
            _currencyModel = currencyModel;
            _currencyType = currencyType;
            _currencyData = currencyData;
            
            var properties = currencyData.GetProperties(currencyType);
            _currencyTextColor = properties.Color;
            _currencyIcon = properties.Icon;

            _currencyLabel.Value = _currencyModel.GetCurrencyAmount(_currencyType).ToString("N0");
            _currencyModel.CurrencyAmountChanged += OnCurrencyAmountChanged;
        }

        void IDisposable.Dispose()
        {
            _currencyModel.CurrencyAmountChanged -= OnCurrencyAmountChanged;
        }

        void ICurrencyCounterViewModel.HandleAddButtonPressed()
        {
            _currencyModel.AddCurrency(_currencyType, _currencyData.AddCurrencyAmount);
        }
        
        private void OnCurrencyAmountChanged(CurrencyType currencyType, int amount)
        {
            if (currencyType != _currencyType)
            {
                return;
            }
            
            _currencyLabel.Value = amount.ToString("N0");
        }
    }
}