using System.Text;

namespace jsTree.Plugins
{
    public class NetTreePluginState : INetTreePlugin
    {
        public string Name { get { return "state"; } }
        public string Settings
        {
            get
            {
                var sb = new StringBuilder();

                return sb.ToString();
            }
        }

        /// <summary>
        /// A string for the key to use when saving the current tree (change if using multiple trees in your project). Defaults to jstree.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Time in milliseconds after which the state will expire. Defaults to -1 meaning - no expire.
        /// </summary>
        public int Ttl { get; set; }

        public NetTreePluginState()
        {
            //default values
            Key = "jstree";
            Ttl = -1;
        }
    }
}
