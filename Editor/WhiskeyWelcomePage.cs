using UnityEditor;
using UnityEngine;

namespace FasterGames.Whiskey.Editor
{
    /*! \mainpage Whiskey - Easy to swallow events framework. ⚡🥃🕹
     *  
     *  This is the top-level page for generated documentation for Whiskey.
     *  
     *  To browse this documentation, a good place to start is [the namespace overview](./namespace_faster_games_1_1_whiskey.html).
     *  
     *  To learn more about Whiskey, it's goals, how to sponsor, and more, head over [to GitHub](https://github.com/faster-games/whiskey).
     *  
     *  [![](https://github.com/faster-games/whiskey/raw/main/Assets/whiskey_header.png)](https://github.com/faster-games/whiskey)
     *  
     */

    /// <summary>
    /// Loader for the whiskey welcome page.
    /// </summary>
    [InitializeOnLoad]
    public static class WhiskeyWelcomePageLoader
    {
        private static readonly string DID_SHOW_WELCOME_KEY = "FasterGames.Whiskey.WelcomePage.DidShowWelcome";

        static WhiskeyWelcomePageLoader()
        {
            var alreadyShown = EditorPrefs.GetBool(DID_SHOW_WELCOME_KEY, false);

            if (!alreadyShown)
            {
                Init();
            }
        }

        [MenuItem("Window/General/Whiskey/About")]
        static void Init()
        {
            EditorWindow.GetWindow<WhiskeyWelcomePage>().Show();
            EditorPrefs.SetBool(DID_SHOW_WELCOME_KEY, true);
        }
    }

    /// <summary>
    /// Welcome page editor window.
    /// </summary>
    /// <remarks>
    /// Displays welcome content the first time a consumer loads Whiskey.
    /// </remarks>
    public class WhiskeyWelcomePage : EditorWindow
    {
        private static readonly string IntroText = @"
Whiskey adds a collection of ScriptableObjects and MonoBehaviours that allow you to create durable events, with decoupled triggers and reactions.
Triggers represent actions that can be, well, triggered at will. A trigger can have any number of reactions, that execute when it's triggered.
A Reaction is just a function - it can be any logic you define.

Listeners bind Triggers to Reactions, via GameObjects. This helps to optimize away cases when a Trigger is triggered, but some number of Reactions
are only relevant to inactive GameObjects, and therefore should not be run. By using a Listener as a sort of 'middleman' between the Trigger and the Reaction,
we can automatically detect this case.

The novel thing about Whiskey is that Events and Reactions are not strongly coupled - You can imagine an event 'doDamage' that collision code triggers.
When this event is triggered, multiple systems can be informed - say a 'health data' system, that updates a representation of health to reflect the damage,
a 'hud healthbar' system that renders the damage, and a 'camera shake' system that shakes the camera when the damage occurs. Without Whiskey, we might be
tempted to write code for all those systems that references a 'float' data member, strongly coupling the implementation of health into all these systems.
With Whiskey, we're able to instead use Events + Reactions to break that coupling, and allow the systems to react to any 'float event' that's triggered.
This keeps our systems separated, and also makes them easily testable - For instance, we can now write a test for the 'hud healthbar' system that validates
it works by triggering a 'float event' with random float values.

The general usage flow for Whiskey Events is as follows:
+ Create an Event, using 'Assets => Create => Whiskey => Events => EventType'.
+ This creates an Event asset on disk, in your project folder. 
+ Create a GameObject, and attach a 'EventType Listener' MonoBehavior - it should be the same type as the created Event.
+ Configure the listener to be bound to the created Event by dragging the Event to the property in the Inspector.
+ Configure the reaction by wiring up the Reaction field of the listener.

When your Application is running, you can manually fire any Whiskey Event by selecting it in the Project pane, and using the Inspector to Raise it.
";

        private void OnGUI()
        {
            titleContent = new GUIContent("Welcome To Whiskey");
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.LabelField("Welcome To Whiskey - Easy to swallow events framework. ⚡🥃🕹");
            EditorGUILayout.SelectableLabel("https://github.com/faster-games/whiskey");
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(IntroText);
            EditorGUILayout.Space();
            if (GUILayout.Button("Close"))
            {
                this.Close();
            }
        }
    }

}