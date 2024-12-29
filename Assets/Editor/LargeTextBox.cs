using Codice.CM.Client.Differences.Graphic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(TestSO))]
public class LargeTextBox : Editor
{
    public override void OnInspectorGUI()
    {
        TestSO optionSO = (TestSO)target;

        SerializedProperty isActiveProp = serializedObject.FindProperty("isActive");
        EditorGUILayout.PropertyField(isActiveProp);

        // Ensure that serialized object is updated before changes
        serializedObject.Update();

        EditorGUILayout.LabelField("Test 2", EditorStyles.boldLabel); // "Test 2" is your header, you can change it to any text

        SerializedProperty test2Prop = serializedObject.FindProperty("test2");
        int lineCount = test2Prop.stringValue.Split('\n').Length;
        test2Prop.stringValue = EditorGUILayout.TextArea(test2Prop.stringValue,
            GUILayout.Height(20 + lineCount * 20), GUILayout.ExpandWidth(true));
        
        EditorGUILayout.LabelField("Test 1", EditorStyles.boldLabel); // "Test 2" is your header, you can change it to any text

         SerializedProperty test1Prop =serializedObject.FindProperty("test1");
         // Loop through the list of strings and display each one as a larger TextArea
         for (int i = 0; i < test1Prop.arraySize; i++)
         {
             // Display a larger input field for each string in the list
             SerializedProperty elemProp = test1Prop.GetArrayElementAtIndex(i);
             elemProp.stringValue = EditorGUILayout.TextArea(elemProp.stringValue, GUILayout.Height(20));
         }

        // Add button to add a new element to the list
        if (GUILayout.Button("Add New Element"))
        {
            test1Prop.arraySize += 1; // Add a new element to the list
        }

        // Add button to remove the last element from the list
        if (GUILayout.Button("Remove Last Element") && test1Prop.arraySize > 0)
        {
            test1Prop.arraySize -= 1; // Remove the last element from the list
        }
        // Manually draw the rest of the properties except 'test2' (which we've already handled)
        // This will ensure the default Inspector fields for buttonText, isActive, etc. are displayed

        SerializedProperty buttonTextProp = serializedObject.FindProperty("buttonText");
        SerializedProperty reactionMessagesProp = serializedObject.FindProperty("reactionMessages");
         SerializedProperty happinessEVChangeProp = serializedObject.FindProperty("happinessEVChange");
         SerializedProperty hotEVChangeProp = serializedObject.FindProperty("hotEVChange");

         // Display remaining fields normally
         EditorGUILayout.PropertyField(buttonTextProp);
         EditorGUILayout.PropertyField(reactionMessagesProp);
         EditorGUILayout.PropertyField(happinessEVChangeProp);
         EditorGUILayout.PropertyField(hotEVChangeProp);

        // Apply changes made through the custom editor
        serializedObject.ApplyModifiedProperties();
    }
}
