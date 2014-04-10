using System.Collections.Generic;
using System.Text;

namespace jsTree.Plugins
{
    public class NetTreePluginCheckbox : INetTreePlugin
    {
        public string Name { get { return "checkbox"; } }
        public string Settings
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("'checkbox': { ");

                // add settings only they're different from default
                var settings = new List<string>();
                if (!Visible) settings.Add("'visible': false");
                if (!ThreeState) settings.Add("'three_state': false");
                if (!WholeNode) settings.Add("'whole_node': false");
                if (!KeepSelectedStyle) settings.Add("'keep_selected_style': false");
                
                sb.Append(string.Join(", ", settings));
                sb.Append(" }");
                return sb.ToString();
            }
        }

        /// <summary>
        /// a boolean indicating if checkboxes should be visible (can be changed at a later time using show_checkboxes() and hide_checkboxes). Defaults to true.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// a boolean indicating if checkboxes should cascade down and have an undetermined state. Defaults to true.
        /// </summary>
        public bool ThreeState { get; set; }

        /// <summary>
        /// a boolean indicating if clicking anywhere on the node should act as clicking on the checkbox. Defaults to true.
        /// </summary>
        public bool WholeNode { get; set; }

        /// <summary>
        /// a boolean indicating if the selected style of a node should be kept, or removed. Defaults to true.
        /// </summary>
        public bool KeepSelectedStyle { get; set; }

        public NetTreePluginCheckbox()
        {
            //default values
            Visible = true;
            ThreeState = true;
            WholeNode = true;
            KeepSelectedStyle = true;
        }
    }
}
