namespace Gimica.ProblemTwo
{
    using UnityEngine;
    
    public class StartupUIController : ControllerBase
    {
        private readonly CurrencyData _currencyData;
        private readonly UIModel _uiModel;
        private readonly CurrencyModel _currencyModel;
        
        // responsible for initial UI that is displayed by default without a user interaction
        public StartupUIController(UIModel uiModel, CurrencyModel currencyModel, LocalDataCollection localDataCollection)
        {
            _uiModel = uiModel;
            _currencyModel = currencyModel;
            _currencyData = localDataCollection.CurrencyData;
        }

        protected override void OnInit()
        {
            base.OnInit();
            ShowGameplayWindow();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ShowGameplayWindow();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _uiModel.Hide<GameplayWindow>();
            }
        }
        

        private void ShowGameplayWindow()
        {
            _uiModel.Show<GameplayWindow>(window =>
            {
                var viewModel = new GameplayWindowViewModel(_currencyData, _currencyModel);
                window.Init(viewModel);
                return viewModel;
            });
        }
    }
}