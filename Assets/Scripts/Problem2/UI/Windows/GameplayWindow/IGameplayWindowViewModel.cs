namespace Gimica.ProblemTwo
{
    using System;
    using System.Collections.Generic;
    
    public interface IGameplayWindowViewModel : IDisposable
    {
        HashSet<ICurrencyCounterViewModel> AllCurrencyCounterViewModels { get; }
    }
}