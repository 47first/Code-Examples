using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameTasks
{
    [CustomPropertyDrawer(typeof(DefaultCompositeModel))]
    public class OneDimensionalCompositePropertyDrawer: PropertyDrawer
    {
        private IEnumerable<KeyValuePair<Type, ComponentModelAttribute>> _componentTypes = ModelTypesContainer.GetComponentModelTypes();
        private int _columns = 2;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = property.managedReferenceValue.GetType().Name;

            EditorGUI.PropertyField(position, property, label, true);

            position.y += EditorGUI.GetPropertyHeight(property) + 5;

            if(property.isExpanded)
            {
                int curColumn = 0;
                int curRow = 0;
                float columnWidth = (position.width / Mathf.Min(_componentTypes.Count(), _columns)) - 5;

                Rect rowPosition = position;

                foreach(var reflectedComponent in _componentTypes)
                {
                    //New Row
                    if(++curColumn > _columns)
                    {
                        curRow++;
                        curColumn = 1;
                        rowPosition.y += 25;
                        rowPosition.x = position.x;

                        int rowColumns = Mathf.Min(_componentTypes.Count() - (curRow * _columns), _columns);
                        columnWidth = (position.width / rowColumns) - 5;
                    }

                    var newPosition = new Rect(rowPosition.x + 2.5f, rowPosition.y, columnWidth, 20f);

                    rowPosition.x += columnWidth + 5;

                    if(GUI.Button(newPosition, "Add " + reflectedComponent.Value.ButtonTitle))
                    {
                        (property.managedReferenceValue as DefaultCompositeModel).Children.Add(
                            Activator.CreateInstance(reflectedComponent.Key));
                    }
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if(property.isExpanded)
                return EditorGUI.GetPropertyHeight(property) + 5f + (25f * Mathf.CeilToInt((float)_componentTypes.Count() / _columns));

            return EditorGUI.GetPropertyHeight(property);
        }
    }
}
