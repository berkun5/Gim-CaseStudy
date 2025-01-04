namespace Gimica.ProblemTwo
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "LocalDataCollection", menuName = "ScriptableObjects/Data/LocalDataCollection", order = 99)]
    public class LocalDataCollection : ScriptableObject
    {
        public CurrencyData CurrencyData => _currencyData;
        
        [SerializeField] private CurrencyData _currencyData;
    }
}