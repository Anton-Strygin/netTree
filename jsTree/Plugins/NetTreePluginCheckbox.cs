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
                if (!string.IsNullOrEmpty(Cascade)) settings.Add("'cascade': '" + Cascade  + "'");
                if (!TieSelection) settings.Add("'tie_selection': false");

                sb.Append(string.Join(", ", settings.ToArray()));
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
        /// a boolean indicating if the selected style of a node should be kept, or removed. Defaults to false.
        /// </summary>
        public bool KeepSelectedStyle { get; set; }

		/// <summary>
		/// This setting controls how cascading and undetermined nodes are applied.
		/// If 'up' is in the string - cascading up is enabled, if 'down' is in the string - cascading down is enabled, if 'undetermined' is in the string - undetermined nodes will be used.
		/// If `three_state` is set to `true` this setting is automatically set to 'up+down+undetermined'. Defaults to ''.
        /// </summary>
		public string Cascade { get; set; }
		
        /// <summary>
		/// This setting controls if checkbox are bound to the general tree selection or to an internal array maintained by the checkbox plugin. Defaults to `true`, only set to `false` if you know exactly what you are doing.
        /// </summary>
        public bool TieSelection { get; set; }

        public NetTreePluginCheckbox()
        {
            //default values
            Visible = true;
            ThreeState = true;
            WholeNode = true;
            KeepSelectedStyle = false;
            Cascade = "";
            TieSelection = true;
        }
    }
}
