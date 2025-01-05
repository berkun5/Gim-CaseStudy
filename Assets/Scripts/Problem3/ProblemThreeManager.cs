namespace Gimica.ProblemThree
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Text;
    using TMPro;
    using UnityEngine.UI;
    
    public class ProblemThreeManager : MonoBehaviour
    {
        private const int MaxAllowedPrice = 10000;
        private const int MaxAllowedStockPriceCount = 100000;
        
        [SerializeField] private TextMeshProUGUI _profitInformationText;
        [SerializeField] private Button _calculateMaxProfitButton;
        
        [Header("Runtime Modification is Allowed")]
        [SerializeField, Range(0, MaxAllowedPrice)] private int[] _stockPrices = {7, 1, 5, 3, 6, 4};
        
        private readonly StringBuilder _stockPricesBuilder = new();
        
        private void OnEnable()
        {
            _calculateMaxProfitButton.onClick.AddListener(OnCalculateButtonClicked);
        }

        private void OnDisable()
        {
            _calculateMaxProfitButton.onClick.RemoveListener(OnCalculateButtonClicked);
        }

        private void OnCalculateButtonClicked()
        {
            _stockPricesBuilder.Append("Stock Prices: ");
            
            for (var i = 0; i < _stockPrices.Length; i++)
            {
                _stockPricesBuilder.Append(_stockPrices[i]);
                
                if (i < _stockPrices.Length - 1)
                {
                    _stockPricesBuilder.Append(", ");
                }
            }

            _stockPricesBuilder.Append($"\nMax Profit: {MaxProfit(_stockPrices)}");
            _profitInformationText.text = _stockPricesBuilder.ToString();
            _stockPricesBuilder.Clear();
        }

        private int MaxProfit(IReadOnlyList<int> prices)
        {
            var maxProfit = 0;
            
            // need at least 2 array elements to work with (can not buy and sell at the same day)
            if (prices == null || prices.Count <= 1 || prices.Count > MaxAllowedStockPriceCount)
            {
                Debug.LogError("Invalid input: prices array must have between 2 and 100,000 elements.");
                return maxProfit;
            }
            
            var lowerPriceSoFar = prices[0] > MaxAllowedPrice ? MaxAllowedPrice : prices[0];
            
            for (var i = 1; i < prices.Count; i++)
            {
                // cap to max (this is generally not needed as user can not enter more than max, but extra caution)
                var iterationPrice = prices[i] > MaxAllowedPrice ? MaxAllowedPrice : prices[i];
                
                // update minPrice if the current price is lower than the previous iteration
                if (iterationPrice < lowerPriceSoFar)
                {
                    lowerPriceSoFar = iterationPrice;
                }
                
                // calculate profit if we sell in this iteration
                var profit = iterationPrice - lowerPriceSoFar;

                // update maxProfit if the calculated profit is higher than the current maxProfit
                if (profit > maxProfit)
                {
                    maxProfit = profit;
                }
            }

            return maxProfit;
        }
        
    }
}