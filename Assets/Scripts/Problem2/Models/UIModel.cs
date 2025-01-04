namespace Gimica.ProblemTwo
{
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public class UIModel : ModelBase
    {
        private readonly UIEntityPool _uiEntityPool;
        private readonly Canvas _gameplayCanvas;
        private readonly Dictionary<Type, UIEntity> _activeEntities = new();
        private readonly Dictionary<Type, UIEntity> _inactiveEntities = new();

        // simplified ui model that handles showing hiding and pooling UIEntities.
        // this version handles a single window per type, on a single canvas.
        public UIModel(UIEntityPool uiEntityPool, Canvas gameplayCanvas)
        {
            _uiEntityPool = uiEntityPool;
            _gameplayCanvas = gameplayCanvas;
        }

        public T Show<T>(Func<T, IDisposable> onCreated = null) where T : UIEntity
        {
            var entityType = typeof(T);
            
            if (_activeEntities.TryGetValue(entityType, out var activeEntity))
            {
                Debug.LogWarning($"UIEntity {entityType} is already active and showing.");
                return (T)activeEntity;
            }
            
            // pool an inactive entity
            if (_inactiveEntities.Remove(entityType, out var pooledEntity))
            {
                SetEntityTransformProperties(pooledEntity.transform);
                pooledEntity.gameObject.SetActive(true);
                
                // add entity to active
                _activeEntities[entityType] = pooledEntity;
                onCreated?.Invoke((T)pooledEntity);
                return (T)pooledEntity;
            }

            // create new entity
            var entity = GetEntity<T>();
            if (entity == null)
            {
                Debug.LogError($"Failed to show UIEntity {entityType}. Entity not found in the pool.");
                return null;
            }

            SetEntityTransformProperties(entity.transform);
            entity.gameObject.SetActive(true);
            
            // add entity to active
            _activeEntities[entityType] = entity;
            onCreated?.Invoke(entity);
            return entity;
        }

        public void Hide<T>() where T : UIEntity
        {
            var entityType = typeof(T);

            if (!_activeEntities.TryGetValue(entityType, out var entity) || !entity.gameObject.activeSelf)
            {
                Debug.LogWarning($"UIEntity {entityType} is not Showing or does not exist.");
                return;
            }

            entity.gameObject.SetActive(false);
            _activeEntities.Remove(entityType);
            
            // add entity to inactive
            _inactiveEntities[entityType] = entity;
        }

        private T GetEntity<T>() where T : UIEntity
        {
            foreach (var prefab in _uiEntityPool.Pool)
            {
                if (prefab.GetComponent<T>() != null)
                {
                    var newEntity = UnityEngine.Object.Instantiate(prefab);
                    newEntity.gameObject.SetActive(false);
                    return newEntity.GetComponent<T>();
                }
            }

            return null;
        }

        private void SetEntityTransformProperties(Transform entityTransform)
        {
            entityTransform.SetParent(_gameplayCanvas.transform);
            entityTransform.localPosition = Vector3.zero;
            entityTransform.localScale = Vector3.one;

            if (entityTransform is RectTransform rectTransform)
            {
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
            }
        }
    }
}
