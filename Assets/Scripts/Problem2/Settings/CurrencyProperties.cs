namespace Gimica.ProblemTwo
{
    using UnityEngine;

    [System.Serializable]
    public class CurrencyProperties
    {
        public CurrencyType Type => _currencyType;
        public Color Color => _currencyColor;
        public Sprite Icon => _currencyIcon;

        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private Color _currencyColor;
        [SerializeField] private Sprite _currencyIcon;
    }
}