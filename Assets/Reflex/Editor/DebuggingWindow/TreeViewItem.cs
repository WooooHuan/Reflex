using UnityEditor.IMGUI.Controls;

namespace Reflex.Editor.DebuggingWindow
{
    internal class TreeViewDataItem<T> : TreeViewItem<int> where T : TreeElement
    {
        public T Data { get; }

        public TreeViewDataItem(int id, int depth, string displayName, T data) : base(id, depth, displayName)
        {
            Data = data;
        }
    }
}
