namespace Gimica.ProblemTwo
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    
    public class CurrencyCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currencyLabel;
        [SerializeField] private Image _currencyImage;
        [SerializeField] private Button _addCurrencyButton;
        
        private ICurrencyCounterViewModel _viewModel;

        private void OnEnable()
        {
            _viewModel?.CurrencyLabel.Subscribe(OnCurrencyLabelChanged);
            _addCurrencyButton.onClick.AddListener(OnAddCurrencyButtonPressed);
        }

        private void OnDisable()
        {
            _viewModel?.CurrencyLabel.Unsubscribe(OnCurrencyLabelChanged);
            _addCurrencyButton.onClick.RemoveListener(OnAddCurrencyButtonPressed);

        }

        public void Init(ICurrencyCounterViewModel viewModel)
        {
            _viewModel = viewModel;

            _currencyImage.sprite = viewModel.CurrencyIcon;
            _currencyLabel.color = viewModel.CurrencyTextColor;
        }
        
        private void OnCurrencyLabelChanged(string newLabel)
        {
            _currencyLabel.text = newLabel;
        }
        
        private void OnAddCurrencyButtonPressed()
        {
            _viewModel?.HandleAddButtonPressed();
        }
    }
}