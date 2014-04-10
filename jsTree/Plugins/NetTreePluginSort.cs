
namespace jsTree.Plugins
{
    /// <summary>
    /// Autmatically sorts all siblings in the tree according to a sorting function.
    /// </summary>
    public class NetTreePluginSort : INetTreePlugin
    {
        public string Name { get { return "sort"; } }
        public string Settings { get { return ""; } }
    }
}
