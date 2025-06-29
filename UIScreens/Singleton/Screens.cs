using System;
using System.Collections.Generic;
using System.Reflection;
using MiniUI.Utilities;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniUI.UIScreens
{
    public class Screens : Singleton<Screens>
    {
        #region Private Fields
        
        private readonly List<UIScreen> _screens = new List<UIScreen>();
        private readonly Dictionary<Type, UIScreen> _screenByType = new Dictionary<Type, UIScreen>();
        
        #endregion
        
        #region Screens Logic
        
        internal UIScreen TryAdd(Type type)
        {
            if (_screenByType.TryGetValue(type, out var value))
            {
                return value;
            }

            var instance = (UIScreen)ScriptableObject.CreateInstance(type);

            _screens.Add(instance);
            _screenByType.Add(type, instance);

            InjectHierarchy(type);

            return instance;

            void InjectHierarchy(Type type)
            {
                var screenBaseType = typeof(UIScreen);

                foreach (var field in type.GetFields(BindingFlags.Public |
                                                     BindingFlags.NonPublic |
                                                     BindingFlags.Instance))
                {
                    if (field.IsStatic)
                    {
                        continue;
                    }

                    if (field.FieldType.IsSubclassOf(screenBaseType))
                    {
                        field.SetValue(instance, TryAdd(field.FieldType));
                    }
                }
            }
        }
        
        internal void Update(VisualElement root)
        {
            foreach (var screen in _screens)
            {
                screen.TryUpdateScreenData(root);
            }
        }
        
        #endregion
        
        #region Public API
        
        public UIScreen Get(Type type)
        {
            return _screenByType[type];
        }
        
        public UIScreen Get<S>() where S : UIScreen
        {
            return _screenByType[typeof(S)];
        }
        
        #endregion
    }
}