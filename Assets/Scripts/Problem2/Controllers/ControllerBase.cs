namespace Gimica.ProblemTwo
{
    using System;
    
    public abstract class ControllerBase : IDisposable, IUpdateListener
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
        /// won't trigger until all models are init
        /// </summary>
        protected virtual void OnInit() { }
        
        protected virtual void OnDispose() { }
        
        /// <summary>
        /// won't start until init
        /// </summary>
        protected virtual void OnUpdate() { }
    }
}