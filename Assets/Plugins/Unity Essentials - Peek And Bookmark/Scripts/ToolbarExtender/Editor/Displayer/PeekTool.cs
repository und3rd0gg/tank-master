using UnityEditor;
using UnityEngine;
using UnityEssentials.Peek.Options;
using UnityEssentials.Peek.Extensions;

namespace UnityEssentials.Peek.toolbarExtent
{
    public class PeekTool
    {
        private const int HEIGHT_TEXT = 8;
        private const int HEIGHT_BUTTON = 15;
        private const int SIZE_SLIDER = 120;

        public static readonly GUIStyle _miniText;
        private PeekToolbarDrawer _drawer = new PeekToolbarDrawer();

        static PeekTool()
        {
            _miniText = new GUIStyle()
            {
                fontSize = 9,
            };
            _miniText.normal.textColor = Color.white;
        }

        public void Init()
        {

        }

        public void DisplaySliderLeft()
        {
            if (!EditorPrefs.GetBool(UnityEssentialsPreferences.SHOW_PEEK_MENU, true))
            {
                return;
            }
            float percent = EditorPrefs.GetFloat(UnityEssentialsPreferences.POSITION_IN_TOOLBAR, UnityEssentialsPreferences.DEFAULT_TOOLBAR_POSITION);
            if (percent > 0.5f)
            {
                return;
            }

#if UNITY_2021_1_OR_NEWER

            percent /= 2f;

            Rect rect = new Rect(0, 0, ToolbarExtender.GetLeftRect().width, 20);
            percent = ExtMathf.Remap(percent, 0f, 0.5f, 0f, 1f);

            float width = (rect.width - SIZE_SLIDER) / 1f * percent;
            GUILayout.Label("", GUILayout.MinWidth(0), GUILayout.Width(width * 0.94f));
#if UNITY_2018_3_OR_NEWER
            Rect finalRect = SetupLocalRect(width);
            AddRightClickBehavior(finalRect);
#endif
            DisplayPeek();

#else
             Rect left = ToolbarExtender.GetLeftRect();
            percent = ExtMathf.Remap(percent, 0f, 0.5f, 0f, 1f);
            float width = (left.width - SIZE_SLIDER) / 1f * percent;

            GUILayout.Label("", GUILayout.MinWidth(0), GUILayout.Width(width));
#if UNITY_2018_3_OR_NEWER
            Rect finalRect = SetupLocalRect(width);
            AddRightClickBehavior(finalRect);
#endif
            DisplayPeek();
#endif
        }

        public void DisplaySliderRight()
        {
            if (!EditorPrefs.GetBool(UnityEssentialsPreferences.SHOW_PEEK_MENU, true))
            {
                return;
            }
            float percent = EditorPrefs.GetFloat(UnityEssentialsPreferences.POSITION_IN_TOOLBAR, UnityEssentialsPreferences.DEFAULT_TOOLBAR_POSITION);
            if (percent <= 0.5f)
            {
                return;
            }

#if UNITY_2021_1_OR_NEWER
            percent += 0.01f;
            if (percent < 0.75f)
            {
                Rect rect = new Rect(0, 0, ToolbarExtender.GetRightRect().width, 20);
                percent = ExtMathf.MirrorFromInterval(percent, 0.5f, 0.75f);
                percent = ExtMathf.Remap(percent, 0.5f, 1f, 0f, 1f);
                float width = ((rect.width - SIZE_SLIDER) / 1f * percent);
                DisplayPeek();

#if UNITY_2018_3_OR_NEWER
                Rect finalRect = SetupLocalRect(width);
                finalRect.x -= width;
                AddRightClickBehavior(finalRect);
#endif
                GUILayout.Label("", GUILayout.MinWidth(0), GUILayout.Width(width));
            }
            else
            {
                Rect rect = new Rect(0, 0, ToolbarExtender.GetRightRect().width, 22);
                percent = ExtMathf.Remap(percent, 0.5f, 1f, 0f, 1f);
                float width = ((rect.width - SIZE_SLIDER) / 1f * percent);
                GUILayout.Label("", GUILayout.MinWidth(0), GUILayout.Width(((width / 2) - SIZE_SLIDER * 1.2f) * 1.15f));
                DisplayPeek();

            }
#else
            Rect right = ToolbarExtender.GetRightRect();
            percent = ExtMathf.Remap(percent, 0.5f, 1f, 0f, 1f);
            float width = (right.width - SIZE_SLIDER) / 1f * percent;

            GUILayout.Label("", GUILayout.MinWidth(0), GUILayout.Width(width));
#if UNITY_2018_3_OR_NEWER
            Rect finalRect = SetupLocalRect(width);
            AddRightClickBehavior(finalRect);
#endif
            DisplayPeek();
#endif




        }

        private void DisplayPeek()
        {
            if (_drawer.DisplayPeekToolBar())
            {
#if UNITY_2018_3_OR_NEWER
                CreateGenericMenu();
#endif
            }
        }


#if UNITY_2018_3_OR_NEWER
        private static Rect SetupLocalRect(float width)
        {
            Rect finalRect = GUILayoutUtility.GetLastRect();
            finalRect.x += width;
            finalRect.width = SIZE_SLIDER;
            finalRect.height = 27;
            return finalRect;
        }

        private void AddRightClickBehavior(Rect rect)
        {
            if (rect.Contains(Event.current.mousePosition) && Event.current.type == EventType.ContextClick)
            {
                CreateGenericMenu();
            }
        }

        private void CreateGenericMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Settings"), false, OpenPreferenceSettings);
            menu.ShowAsContext();
            if (Event.current.type == EventType.ContextClick)
            {
                Event.current.Use();
            }
        }

        private void OpenPreferenceSettings()
        {
            SettingsService.OpenProjectSettings(UnityEssentialsPreferences.NAME_PREFERENCE);
        }
#endif
    }
}