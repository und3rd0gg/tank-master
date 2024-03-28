﻿using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace UnityEssentials.Peek.Extensions.Editor.EventEditor
{
    public static class ExtEventEditor
    {
        public static void Use()
        {
            if (Event.current.type != EventType.Layout && Event.current.type != EventType.Repaint)
            {
                Event.current.Use();
            }
        }
    }
}