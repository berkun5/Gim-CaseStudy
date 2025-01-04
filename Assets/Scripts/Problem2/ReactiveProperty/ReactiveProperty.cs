namespace Gimica.ProblemTwo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    // i've created this class ages ago, it's simply an event that reacts on a single property changes, awesome for UI.
    // it has additional features like triggering the event on subscription and reading the value of the property directly
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T _value;
        private event Action<T> OnValueChanged;

        public ReactiveProperty()
        {
            Value = default;
        }
        
        public ReactiveProperty(T initialValue)
        {
            _value = initialValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                {
                    return;
                }
                
                _value = value;
                OnValueChanged?.Invoke(value);
            }
        }

        void IReactiveProperty<T>.Subscribe(Action<T> subscriber, bool triggerUponSubscription)
        {
            if (OnValueChanged != null && OnValueChanged.GetInvocationList().Contains(subscriber))
            {
                return;
            }
            
            OnValueChanged += subscriber;
            
            if (triggerUponSubscription)
            {
                subscriber?.Invoke(_value);
            }
        }

        void IReactiveProperty<T>.Unsubscribe(Action<T> subscriber)
        {
            OnValueChanged -= subscriber;
        }
    }
}