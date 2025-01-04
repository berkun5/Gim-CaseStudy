namespace Gimica.ProblemTwo
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class CurrencyModel : ModelBase
    {
        public event Action<CurrencyType, int> CurrencyAmountChanged;
        private readonly Dictionary<CurrencyType, int> _currencyTypeAmountPair = new();
        
        public CurrencyModel()
        {
            foreach (CurrencyType currencyType in Enum.GetValues(typeof(CurrencyType)))
            {
                // 0 is usually the remote data and it is filled after the data loaded,
                // but in this simplified version of my framework we don't store data
                _currencyTypeAmountPair[currencyType] = 0;
            }
        }

        public void AddCurrency(CurrencyType type, int amount)
        {
            _currencyTypeAmountPair[type] += amount;
            CurrencyAmountChanged?.Invoke(type, _currencyTypeAmountPair[type]);
        }

        // this can return bool and renamed to TryRemove, that checks if the amount can be deducted or not
        public void RemoveCurrency(CurrencyType type, int amount)
        {
            var current = _currencyTypeAmountPair[type];
            var result = current - amount;
            
            // cap to 0
            _currencyTypeAmountPair[type] = Mathf.Max(0, result);
            CurrencyAmountChanged?.Invoke(type, _currencyTypeAmountPair[type]);
        }

        public int GetCurrencyAmount(CurrencyType type)
        {
            return _currencyTypeAmountPair[type];
        }
    }
}