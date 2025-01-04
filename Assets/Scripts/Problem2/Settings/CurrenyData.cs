namespace Gimica.ProblemTwo
{
    using UnityEngine;
    using System.Collections.Generic;
    
    [CreateAssetMenu(fileName = "CurrencyData", menuName = "ScriptableObjects/Data/CurrencyData", order = 99)]
    public class CurrencyData : ScriptableObject, ISerializationCallbackReceiver
    {
        public int AddCurrencyAmount => _addCurrencyAmount;
        public IEnumerable<CurrencyType> GameplayWindowCurrencies => _gameplayWindowCurrencies;
        
        [SerializeField, Range(0, 100), Space(10)] private int _addCurrencyAmount = 10;
        [SerializeField, Space(10)] private List<CurrencyType> _gameplayWindowCurrencies;
        [SerializeField] private List<CurrencyProperties> _allCurrencyProperties;
        
        private readonly Dictionary<CurrencyType, CurrencyProperties> _currencyTypePropertyPair = new();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // do nothing
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _currencyTypePropertyPair.Clear();
            foreach (var property in _allCurrencyProperties)
            {
                _currencyTypePropertyPair.Add(property.Type, property);
            }
        }
        
        public CurrencyProperties GetProperties(CurrencyType requestedType)
        {
            if (!_currencyTypePropertyPair.TryGetValue(requestedType, out var value))
            {
                Debug.LogError($"Currency {requestedType} doesn't exist in the local CurrencyData. Please fix.");
            }

            return value;
        }
    }
}