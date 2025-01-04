namespace Gimica.ProblemTwo
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;
    
    public class ProblemTwoManager : MonoBehaviour
    {
        [SerializeField] private LocalDataCollection _localDataCollection;
        [SerializeField] private UIEntityPool _uiEntityPool;
        [SerializeField] private  Canvas _gameplayCanvas;
        
        private readonly HashSet<ModelBase> _models = new();
        private readonly HashSet<ControllerBase> _controllers = new();

        // simplified version of the entire execution order of the game/app,
        //i usually add IEnumerator or Async methods to buffer load data per model from the remote/local server.
        private void Awake()
        {
            // models hold the bussiness logic
            CreateModels();
            InitModels();
            
            // controllers are bridges between the user interaction and models
            CreateControllers();
            InitControllers();
        }

        private void OnDestroy()
        {
            DisposeModels();
            DisposeControllers();
        }

        private void Update()
        {
            UpdateModels();
            UpdateControllers();
        }

        private void CreateModels()
        {
            // add and populate models and pass dependencies
            //when we have many models, this method ensures that we won't have a circular dependency,
            //as you create the models in a linear way, you can only pass the previously created models to the next ones
            //for example; in a creation sequence: 'DataModel, > InputModel > UIModel', the DataModel can not have
            //dependencies for InputModel and UIModel because they are not created yet. This is a very simplified version
            //of my DI framework
            _models.Add(new UIModel(_uiEntityPool, _gameplayCanvas));
            _models.Add(new CurrencyModel());
        }

        private void CreateControllers()
        {
            _controllers.Add(new StartupUIController(GetModel<UIModel>(), GetModel<CurrencyModel>(),
                _localDataCollection));
        }

        private void InitModels()
        {
            foreach (var model in _models)
            {
                model.Init();
            }
        }

        private void InitControllers()
        {
            foreach (var controller in _controllers)
            {
                controller.Init();
            }
        }
        
        private void DisposeModels()
        {
            foreach (IDisposable model in _models)
            {
                model.Dispose();
            }
        }

        private void DisposeControllers()
        {
            foreach (IDisposable controller in _controllers)
            {
                controller.Dispose();
            }
        }
        
        private void UpdateModels()
        {
            foreach (IUpdateListener model in _models)
            {
                model.Update();
            }
        }

        private void UpdateControllers()
        {
            foreach (IUpdateListener controller in _controllers)
            {
                controller.Update();
            }
        }
        
        private T GetModel<T>() where T : ModelBase
        {
            foreach (var existingModel in _models)
            {
                if (existingModel.GetType() == typeof(T))
                {
                    return (T)existingModel;
                }
            }
            
            return null;
        }
    }
}