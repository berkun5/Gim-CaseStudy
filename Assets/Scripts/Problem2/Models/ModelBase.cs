namespace Gimica.ProblemTwo
{
    using System;
    
    public abstract class ModelBase : IDisposable, IUpdateListener
    {
        private bool _isInitialized;

        void IDisposable.Dispose()
        {
            OnDispose();
        }

        void IUpdateListener.Update()
        {
            if (!_isInitialized)
            {
                return;
            }
            
            OnUpdate();
        }
        
        public void Init()
        {
            _isInitialized = true;
            OnInit();
        }
        
       /// <summary>
       /// all models are constructed and this model's data is loaded.
       /// </summary>
        protected virtual void OnInit() { }
       
        protected virtual void OnDispose() { }

       /// <summary>
       /// won't trigger until init
       /// </summary>
        protected virtual void OnUpdate() { }
    }
}

