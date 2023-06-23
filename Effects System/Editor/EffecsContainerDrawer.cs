using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace EffectsSystem
{
    [CustomPropertyDrawer(typeof(EffectsContainer))]
    public class EffectsContainerDrawer: PropertyDrawer
    {
        private IEnumerable<KeyValuePair<Type, string>> _modelTypes = GetModelTypes();
        private float _buttonHeight = 20f;
        private float _margin = 5f;
        private int _columns = 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            position.y += EditorGUI.GetPropertyHeight(property) + _margin;

            if(property.isExpanded)
            {
                int curColumn = 0;
                int curRow = 0;
                float columnWidth = (position.width / Mathf.Min(_modelTypes.Count(), _columns)) - 5;

                Rect rowPosition = position;

                foreach(var modelType in _modelTypes)
                {
                    //New Row
                    if(++curColumn > _columns)
                    {
                        curRow++;
                        curColumn = 1;
                        rowPosition.y += _buttonHeight + _margin;
                        rowPosition.x = position.x;

                        int rowColumns = Mathf.Min(_modelTypes.Count() - (curRow * _columns), _columns);
                        columnWidth = (position.width / rowColumns) - _margin;
                    }

                    var newPosition = new Rect(rowPosition.x + (_margin / 2),
                        rowPosition.y, columnWidth, _buttonHeight);

                    rowPosition.x += columnWidth + _margin;

                    if(GUI.Button(newPosition, "Add " + modelType.Value))
                    {
                        (property.managedReferenceValue as EffectsContainer).Effects.Add(
                            (Activator.CreateInstance(modelType.Key) as Effect));
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(property.isExpanded == false)
                return EditorGUI.GetPropertyHeight(property);

            return EditorGUI.GetPropertyHeight(property) + _margin + ((_margin + _buttonHeight) * Mathf.CeilToInt((float)_modelTypes.Count() / _columns));
        }

        private static IEnumerable<KeyValuePair<Type, string>> GetModelTypes()
        {
            var modelTypes = new Dictionary<Type, string>();

            foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var effectTypes = assembly.GetTypes().Where(type =>
                type.IsAbstract == false &&
                type.BaseType == typeof(Effect)
                );

                foreach(var effectType in effectTypes)
                {
                    string typeTitle = effectType.Name;

                    var modelAttribute = effectType.GetCustomAttribute<EffectAttribute>();

                    if(modelAttribute is not null)
                        typeTitle = modelAttribute.Title;

                    modelTypes.Add(effectType, typeTitle);
                }
            }

            return modelTypes;
        }
    }
}
