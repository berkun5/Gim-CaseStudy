namespace Gimica.ProblemTwo
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class GameplayWindow : UIEntity
    {
        [SerializeField] private RectTransform _currencyCounterViewContainer;
        [SerializeField] private CurrencyCounterView _currencyCounterViewPrefab;

        private IGameplayWindowViewModel _viewModel;
        private readonly List<CurrencyCounterView> _instantiatedCurrencyViews = new();

        public void Init(IGameplayWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeCurrencyCounters();
        }

        private void InitializeCurrencyCounters()
        {
            var viewModels = _viewModel.AllCurrencyCounterViewModels;
            var activeViewCount = 0;

            foreach (var viewModel in viewModels)
            {
                if (activeViewCount >= _instantiatedCurrencyViews.Count)
                {
                    // new view
                    var newView = Instantiate(_currencyCounterViewPrefab, _currencyCounterViewContainer);
                    newView.gameObject.SetActive(false);
                    _instantiatedCurrencyViews.Add(newView);
                }

                // pool
                _instantiatedCurrencyViews[activeViewCount].Init(viewModel);
                _instantiatedCurrencyViews[activeViewCount].gameObject.SetActive(true);
                activeViewCount++;
            }

            // deactivate any remaining views
            for (var i = activeViewCount; i < _instantiatedCurrencyViews.Count; i++)
            {
                if (_instantiatedCurrencyViews[i].gameObject.activeSelf)
                {
                    _instantiatedCurrencyViews[i].gameObject.SetActive(false);
                }
            }
        }
    }
}