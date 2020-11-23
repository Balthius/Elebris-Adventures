using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// I feel like Im overcomplicating things. abandoned for now in favor of manual SO creation
/// </summary>
public class ScriptableObjectActionCreator : EditorWindow
{
    const string parentFolder = "Assets/Action";
    const string valueModelsFolder = parentFolder + "/ValueModels/";
    //Fills in naming in the template for you
    public string ValueModelName = "Enter Name of Object";


    [MenuItem("Window/EventCreator")]
    public static void ShowWindow()
    {
        GetWindow<ScriptableObjectActionCreator>("ValueModelCreator");
    }
    private void OnGUI()
    {
        GUILayout.Label("Create a Custom Value Model", EditorStyles.boldLabel);

        ValueModelName = EditorGUILayout.TextField("Value Model Name", ValueModelName);
        string valuePath = valueModelsFolder + ValueModelName;
        if (GUILayout.Button("Create Value Model"))
        {

            if (ValueModelName.Length == 0) return;
            //Start Event Creation
            string currentValueModel = ValueModelName + "ValueModel";

            if (File.Exists(valuePath) == false)
            { 

            }
           
        }


    }
    public class MakeScriptableObject
    {
        [MenuItem("Assets/Create/My Scriptable Object")]
        public static void CreateMyAsset(string path, string name, ScriptableObjectType type)
        {
            ActionContainerScriptableObject scriptObject = ScriptableObject.CreateInstance(typeof(ActionContainerScriptableObject)) as ActionContainerScriptableObject;

            AssetDatabase.CreateAsset(scriptObject, path + name);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            Selection.activeObject = scriptObject;
        }
    }


}

public enum ScriptableObjectType
{
    ValueModel,
    Calculation,
    Component
}


