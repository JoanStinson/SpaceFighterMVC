using UnityEngine;
using UnityEditor;

namespace JGM.GameEditor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class NotNullInspector : Editor
    {
        private bool m_errorMessagePrinted = false;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            MonoBehaviour script = (MonoBehaviour)target;
            SerializedProperty property = serializedObject.GetIterator();

            while (property.NextVisible(true))
            {
                if (property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.objectReferenceValue == null)
                {
                    bool hasSerializeFieldAttribute = false;

                    var field = script.GetType().GetField(property.name,
                        System.Reflection.BindingFlags.Instance |
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Public);

                    if (field != null)
                    {
                        hasSerializeFieldAttribute = field.GetCustomAttributes(typeof(SerializeField), true).Length > 0;
                    }

                    if (hasSerializeFieldAttribute && !m_errorMessagePrinted)
                    {
                        Debug.LogError($"{script.gameObject.name}: {property.name} is null or unassigned!", script.gameObject);
                        m_errorMessagePrinted = true;
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
