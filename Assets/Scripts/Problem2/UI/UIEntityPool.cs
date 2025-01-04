namespace Gimica.ProblemTwo
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "UIEntityPool", menuName = "ScriptableObjects/UI/UIEntityPool", order = 99)]
    public class UIEntityPool : ScriptableObject
    {
        public List<UIEntity> Pool => _uiPool;
        [SerializeField] private List<UIEntity> _uiPool = new();
    }
}