namespace Gimica.ProblemTwo
{
    using System;
    using UnityEngine;
    
    public interface ICurrencyCounterViewModel : IDisposable
    {
        IReactiveProperty<string> CurrencyLabel { get; }
        Color CurrencyTextColor { get; }
        Sprite CurrencyIcon { get; }

        void HandleAddButtonPressed();
    }
}