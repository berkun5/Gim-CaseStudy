namespace Gimica.ProblemTwo
{ 
    using System;
    
    public interface IReactiveProperty<out T>
    {
        T Value { get; }
        void Subscribe(Action<T> subscriber, bool triggerUponSubscription = true);
        void Unsubscribe(Action<T> subscriber);
    }
}