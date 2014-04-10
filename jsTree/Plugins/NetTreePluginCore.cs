using System.Text;

namespace jsTree.Plugins
{
    public class NetTreePluginCore : INetTreePlugin
    {
        /// <summary>a boolean indicating if multiple nodes can be selected</summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// determines what happens when a user tries to modify the structure of the tree
        ///If left as false all operations like create, rename, delete, move or copy are prevented.
        ///You can set this to true to allow all interactions or use a function to have better control.
        ///Examples
        ///$('#tree').jstree({
        ///    'core' : {
        ///        'check_callback' : function (operation, node, node_parent, node_position, more) {
        ///            // operation can be 'create_node', 'rename_node', 'delete_node', 'move_node' or 'copy_node'
        ///            // in case of 'rename_node' node_position is filled with the new node name
        ///            return operation === 'rename_node' ? true : false;
        ///        }
        ///    }
        ///});
        /// </summary>
        public string CheckCallback { get; set; }

        public string Name { get { return "core"; } }

        public string Settings
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("'core': { 'multiple': ");
                sb.Append(Multiple);
                if (!string.IsNullOrEmpty(CheckCallback))
                    sb.AppendFormat(", 'check_callback': {0}", CheckCallback);
                sb.Append(" }");
                return sb.ToString();
            }
        }
    }
}
