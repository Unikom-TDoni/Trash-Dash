using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

#if UNITY_EDITOR
namespace Lncodes.Module.Unity.Editor
{
    [CustomPropertyDrawer(typeof(TagSelectorAttribute))]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType is SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);

                var attribute = this.attribute as TagSelectorAttribute;

                if (attribute.UseDefaultTagFieldDrawer)
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                else
                {
                    var index = -1;
                    var tagCollection = new List<string>();
                    var propertyString = property.stringValue;

                    tagCollection.Add("<NoTag>");
                    tagCollection.AddRange(UnityEditorInternal.InternalEditorUtility.tags);

                    if (string.IsNullOrEmpty(propertyString)) index = 0;
                    else index = tagCollection.FindIndex(i => i.Equals(propertyString));

                    index = EditorGUI.Popup(position, label.text, index, tagCollection.ToArray());

                    property.stringValue = index switch
                    {
                        0 => String.Empty,
                        > 1 => tagCollection[index],
                        _ => String.Empty,
                    };
                }
                EditorGUI.EndProperty();
            }
            else EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif