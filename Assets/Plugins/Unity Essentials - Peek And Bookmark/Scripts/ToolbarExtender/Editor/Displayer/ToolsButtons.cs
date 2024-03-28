using UnityEditor;

namespace UnityEssentials.Peek.toolbarExtent
{
    [InitializeOnLoad]
    public class ToolsButton
    {
        private static PeekTool _peekTool = new PeekTool();
        public static PeekTool PeekTool { get { return (_peekTool); } }

        static ToolsButton()
        {
            _peekTool.Init();
            ToolbarExtender.LeftToolbarGUI.Add(_peekTool.DisplaySliderLeft);
            ToolbarExtender.RightToolbarGUI.Add(_peekTool.DisplaySliderRight);
        }
    }
}