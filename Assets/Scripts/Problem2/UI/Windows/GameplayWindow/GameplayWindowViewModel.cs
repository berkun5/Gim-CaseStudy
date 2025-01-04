namespace Gimica.ProblemTwo
{
    using System;
    using System.Collections.Generic;

    public class GameplayWindowViewModel : IGameplayWindowViewModel
    {
        HashSet<ICurrencyCounterViewModel> IGameplayWindowViewModel.AllCurrencyCounterViewModels => 
            _allCurrencyCounterViewModels;
        
        private readonly HashSet<ICurrencyCounterViewModel> _allCurrencyCounterViewModels = new();
        
        public GameplayWindowViewModel(CurrencyData currencyData, CurrencyModel currencyModel)
        {
            foreach (var type in currencyData.GameplayWindowCurrencies)
            {
                _allCurrencyCounterViewModels.Add(new CurrencyCounterViewModel(type, currencyData, currencyModel));
            }
        }

        void IDisposable.Dispose()
        {
            // do nothing
            foreach (var currencyViewModel in _allCurrencyCounterViewModels)
            {
                currencyViewModel.Dispose();
            }
        }
        
    }
}