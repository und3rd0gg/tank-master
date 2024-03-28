using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityEssentials.Peek.toolbarExtent
{
	[InitializeOnLoad]
	public static class ToolbarExtender
	{
		private const string UNITY_EDITOR_TOOLBAR_REFLECTION = "UnityEditor.Toolbar";
		private const string REPAINT_FUNCTION = "RepaintToolbar";


		static int m_toolCount;
		static GUIStyle m_commandStyle = null;

		public static readonly List<Action> LeftToolbarGUI = new List<Action>();
		public static readonly List<Action> RightToolbarGUI = new List<Action>();
		private static Rect _leftRect = new Rect();
		public static Rect GetLeftRect() { return (_leftRect); }
		private static Rect _rightRect = new Rect();
		public static Rect GetRightRect() { return (_rightRect); }


		static ToolbarExtender()
		{
			Type toolbarType = typeof(Editor).Assembly.GetType(UNITY_EDITOR_TOOLBAR_REFLECTION);

#if UNITY_2019_1_OR_NEWER
			string fieldName = "k_ToolCount";
#else
			string fieldName = "s_ShownToolIcons";
#endif

			FieldInfo toolIcons = toolbarType.GetField(fieldName,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

#if UNITY_2019_3_OR_NEWER
			m_toolCount = toolIcons != null ? ((int)toolIcons.GetValue(null)) : 8;
#elif UNITY_2019_1_OR_NEWER
			m_toolCount = toolIcons != null ? ((int) toolIcons.GetValue(null)) : 7;
#elif UNITY_2018_1_OR_NEWER
			m_toolCount = toolIcons != null ? ((Array) toolIcons.GetValue(null)).Length : 6;
#else
			m_toolCount = toolIcons != null ? ((Array) toolIcons.GetValue(null)).Length : 5;
#endif

			ToolbarCallback.OnToolbarGUI = OnGUI;
			ToolbarCallback.OnToolbarGUILeft = GUILeft;
			ToolbarCallback.OnToolbarGUIRight = GUIRight;
		}

#if UNITY_2019_3_OR_NEWER
		public const float space = 8;
#else
		public const float space = 10;
#endif
		public const float largeSpace = 20;
		public const float buttonWidth = 32;
		public const float dropdownWidth = 80;
#if UNITY_2019_1_OR_NEWER
		public const float playPauseStopWidth = 140;
#else
		public const float playPauseStopWidth = 100;
#endif

		/// <summary>
		/// ask to repaint toolBar
		/// </summary>
		public static void Repaint()
		{
			Type toolbarType = typeof(Editor).Assembly.GetType(UNITY_EDITOR_TOOLBAR_REFLECTION);
			MethodInfo repaintMethod = toolbarType.GetMethod(REPAINT_FUNCTION, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
			repaintMethod?.Invoke(new object(), new object[0]);
		}

		static void OnGUI()
		{
			// Create two containers, left and right
			// Screen is whole toolbar

			if (m_commandStyle == null)
			{
				m_commandStyle = new GUIStyle("CommandLeft");
			}

			var screenWidth = EditorGUIUtility.currentViewWidth;

			// Following calculations match code reflected from Toolbar.OldOnGUI()
			float playButtonsPosition = Mathf.RoundToInt((screenWidth - playPauseStopWidth) / 2);

			_leftRect = new Rect(0, 0, screenWidth, Screen.height);
			_leftRect.xMin += space; // Spacing left
			_leftRect.xMin += buttonWidth * m_toolCount; // Tool buttons
#if UNITY_2019_3_OR_NEWER
			_leftRect.xMin += space; // Spacing between tools and pivot
#else
			_leftRect.xMin += largeSpace; // Spacing between tools and pivot
#endif
			_leftRect.xMin += 64 * 2; // Pivot buttons
			_leftRect.xMax = playButtonsPosition;

			_rightRect = new Rect(0, 0, screenWidth, Screen.height);
			_rightRect.xMin = playButtonsPosition;
			_rightRect.xMin += m_commandStyle.fixedWidth * 3; // Play buttons
			_rightRect.xMax = screenWidth;
			_rightRect.xMax -= space; // Spacing right
			_rightRect.xMax -= dropdownWidth; // Layout
			_rightRect.xMax -= space; // Spacing between layout and layers
			_rightRect.xMax -= dropdownWidth; // Layers
#if UNITY_2019_3_OR_NEWER
			_rightRect.xMax -= space; // Spacing between layers and account
#else
			_rightRect.xMax -= largeSpace; // Spacing between layers and account
#endif
			_rightRect.xMax -= dropdownWidth; // Account
			_rightRect.xMax -= space; // Spacing between account and cloud
			_rightRect.xMax -= buttonWidth; // Cloud
			_rightRect.xMax -= space; // Spacing between cloud and collab
			_rightRect.xMax -= 78; // Colab

			// Add spacing around existing controls
			_leftRect.xMin += space;
			_leftRect.xMax -= space;
			_rightRect.xMin += space;
			_rightRect.xMax -= space;

			// Add top and bottom margins
#if UNITY_2019_3_OR_NEWER
			_leftRect.y = 4;
			_leftRect.height = 22;
			_rightRect.y = 4;
			_rightRect.height = 22;
#else
			_leftRect.y = 5;
			_leftRect.height = 24;
			_rightRect.y = 5;
			_rightRect.height = 24;
#endif
			if (_leftRect.width > 0)
			{
				GUILayout.BeginArea(_leftRect);

				GUILayout.BeginHorizontal();
				foreach (var handler in LeftToolbarGUI)
				{
					handler();
				}

				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}

			if (_rightRect.width > 0)
			{
				GUILayout.BeginArea(_rightRect);
				GUILayout.BeginHorizontal();
				foreach (var handler in RightToolbarGUI)
				{
					handler();
				}

				GUILayout.EndHorizontal();
				GUILayout.EndArea();
			}
		}

#if UNITY_2021_1_OR_NEWER
		public static void GUILeft()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.Height(0));
			Rect rect = GUILayoutUtility.GetLastRect();
			rect.width *= 2;
			rect.height = 22;
			if (rect.width > 100)
			{
				_leftRect = rect;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			foreach (var handler in LeftToolbarGUI)
			{
				handler();
			}
			GUILayout.EndHorizontal();
		}


		public static void GUIRight()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("", GUILayout.ExpandWidth(true), GUILayout.Height(0));
			Rect rect = GUILayoutUtility.GetLastRect();
			rect.width *= 2;
			rect.height = 22;
			if (rect.width > 100)
			{
				_rightRect = rect;
			}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal();
			foreach (var handler in RightToolbarGUI)
			{
				handler();
			}
			GUILayout.EndHorizontal();
		}

		private static void DrawQuad(Rect position, Color color)
		{
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(position, GUIContent.none);
		}

#else
		public static void GUILeft()
		{
			GUILayout.BeginHorizontal();
			foreach (var handler in LeftToolbarGUI)
			{
				handler();
			}
			GUILayout.EndHorizontal();
		}

		public static void GUIRight()
		{
			GUILayout.BeginHorizontal();
			foreach (var handler in RightToolbarGUI)
			{
				handler();
			}
			GUILayout.EndHorizontal();
		}
#endif
	}
}
