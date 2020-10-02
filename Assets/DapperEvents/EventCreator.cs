using UnityEngine;
using UnityEditor;
using System.IO;


namespace Assets.DapperEvents
{
    public class EventCreator : EditorWindow
    {
        //Where files should be placed
        const string parentFolder = "Assets/DapperEvents/GameEvents";
        const string eventsFolder = parentFolder + "/Events/";
        const string listenersFolder = parentFolder + "/Listeners/";
        const string unityEventsFolder = parentFolder + "/UnityEvents/";
        //Fills in naming in the template for you
        public string eventName = "Enter Name of Event";
        //not very type-safe, but allows you to generate the correct class in the required p;aces
        public string typeName = "Enter Type of Event";

        [MenuItem("Window/EventCreator")]
        public static void ShowWindow()
        {
            GetWindow<EventCreator>("EventCreator");
        }


        private void OnGUI()
        {
            GUILayout.Label("Create a Custom Event System", EditorStyles.boldLabel);

            eventName = EditorGUILayout.TextField("Event System Name", eventName);
            typeName = EditorGUILayout.TextField("Event Type", typeName);
            if (GUILayout.Button("Create Event System"))
            {

                if (eventName.Length == 0 || typeName.Length == 0) return;
                //Start Event Creation
                string currentEventName = eventName + "Event";
                string currentEventClassName = currentEventName + ".cs";
                string currentEventClassPath = eventsFolder + currentEventClassName;

                //Start Event Creation
                string currentListenerName = eventName + "Listener";
                string currentListenerClassName = currentListenerName + ".cs";
                string currentListenerClassPath = listenersFolder + currentListenerClassName;

                //Start Listener Creation
                string currentUnityEventName = "Unity" + eventName + "Event";
                string currentUnityEventClassName =  currentUnityEventName + ".cs";
                string currentUnityEventClassPath = unityEventsFolder + currentUnityEventClassName;

                if (File.Exists(currentEventClassPath) == false)
                { // do not overwrite
                    using (StreamWriter outfile =
                        new StreamWriter(currentEventClassPath))
                    {
                        GenerateNamespaces(outfile);
                        outfile.WriteLine("[CreateAssetMenu(fileName = \"New" + currentEventName + "\", menuName = \"Game Events /" + currentEventName + "\")]");
                        outfile.WriteLine("public class " + currentEventName + " : BaseGameEvent <" + typeName + "> {");
                        outfile.WriteLine("}"); 
                        outfile.WriteLine("}");
                    }//File written
                }
                if (File.Exists(currentListenerClassPath) == false)
                { // do not overwrite
                    using (StreamWriter outfile =
                        new StreamWriter(currentListenerClassPath))
                    {
                        GenerateNamespaces(outfile);
                        outfile.WriteLine("public class " + currentListenerName + " : BaseGameEventListener<" + typeName + ", " + eventName + "," + currentUnityEventName + " > {");
                        outfile.WriteLine("}");
                        outfile.WriteLine("}");
                    }//File written
                }
                if (File.Exists(currentUnityEventClassPath) == false)
                { // do not overwrite
                    using (StreamWriter outfile =
                        new StreamWriter(currentUnityEventClassPath))
                    {
                        GenerateNamespaces(outfile);
                        outfile.WriteLine("[System.Serializable]");
                        outfile.WriteLine("public class " + currentUnityEventName + " : UnityEvent<" + typeName + "> {");
                        outfile.WriteLine("}");
                        outfile.WriteLine("}");
                    }//File written
                }

                //template
                //[CreateAssetMenu(fileName = "New Int Event", menuName = "Game Events/Int Event")]
                //public class IntEvent : BaseGameEvent<int>
                //{

                //}

                //public class IntListener : BaseGameEventListener<int, IntEvent, UnityIntEvent> { }

                //[System.Serializable] public class UnityIntEvent : UnityEvent<int> { }

                //https://www.youtube.com/watch?v=iXNwWpG7EhM



                //Removed 
                //const string scriptTemplate = "D:/Development/2020.2.0b2/Editor/Data/Resources/ScriptTemplates"; 


                ////Create folders 
                //if (!AssetDatabase.IsValidFolder(parentFolder))
                //{
                //    AssetDatabase.CreateFolder("Assets", "DapperEvents");
                //}

                //if (!AssetDatabase.IsValidFolder(eventsFolder))
                //{
                //    AssetDatabase.CreateFolder(parentFolder, "Event");
                //}

                //if (!AssetDatabase.IsValidFolder(listenersFolder))
                //{
                //    AssetDatabase.CreateFolder(parentFolder, "Listeners");
                //}

                //if (!AssetDatabase.IsValidFolder(unityEventsFolder))
                //{
                //    AssetDatabase.CreateFolder(parentFolder, "UnityEvents");
                //}
            }
        }

        private static void GenerateNamespaces(StreamWriter outfile)
        {
            outfile.WriteLine("using UnityEngine;");
            outfile.WriteLine("using UnityEngine.Events; ");
            outfile.WriteLine("using System.Collections;");
            outfile.WriteLine("using System.Collections.Generic;");
            outfile.WriteLine("");
            outfile.WriteLine("namespace Assets.DapperEvents.GameEvents {");
        }
    }
}