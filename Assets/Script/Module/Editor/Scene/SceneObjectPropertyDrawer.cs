using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
namespace Lncodes.Module.Unity.Editor
{
    [CustomPropertyDrawer(typeof(SceneObject))]
    public class SceneObjectPropertyDrawer : PropertyDrawer
    {
        private readonly string _nameProperty = "_name";
        private readonly string _assetProperty = "_asset";
        private readonly string _indexProperty = "_index";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            var name = property.FindPropertyRelative(_nameProperty);
            var asset = property.FindPropertyRelative(_assetProperty);
            var index = property.FindPropertyRelative(_indexProperty);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            if (asset is not null)
            {
                asset.objectReferenceValue = EditorGUI.ObjectField(position, asset.objectReferenceValue, typeof(SceneAsset), false);
                if (asset.objectReferenceValue is not null)
                {
                    var sceneAsset = asset.objectReferenceValue as SceneAsset;
                    name.stringValue = sceneAsset.name;
                    index.intValue = SceneUtility.GetBuildIndexByScenePath(AssetDatabase.GetAssetPath(sceneAsset));
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
#endif