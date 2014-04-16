using System.Linq;
using System.Text;

namespace jsTree
{
    public class NetTreeNodeType
    {
        /// <summary>
        /// type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// a string - can be a path to an icon or a className, if using an image that is in the current directory use a ./ prefix, 
        /// otherwise it will be detected as a class. Omit to use the default icon from your theme.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// an array of node type strings, that nodes of this type can have as children. Do not specify for no limits.
        /// </summary>
        public string[] ValidChildren { get; set; }
        
        /// <summary>
        /// the maximum number of immediate children this node type can have. Do not specify or set to -1 for unlimited.
        /// </summary>
        public int MaxChildren { get; set; }
        /// <summary>
        /// the maximum number of nesting this node type can have. A value of 1 would mean that the node can have children, 
        /// but no grandchildren. Do not specify or set to -1 for unlimited.
        /// </summary>
        public int MaxDepth { get; set; }


        public NetTreeNodeType()
        {
            // default values
            MaxChildren = -1;
            MaxDepth = -1;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("'{0}' : ", Name);
            sb.Append("{ ");
            if (ValidChildren != null && ValidChildren.Length > 0)
            {
                var childTypes = string.Join(",", ValidChildren.Select(r => string.Format("'{0}'", r)).ToArray());
                sb.AppendFormat("'valid_children': [{0}]", childTypes);
            }
            if (!string.IsNullOrEmpty(Icon))
            {
                sb.AppendFormat(", 'icon': '{0}'", Icon);
            }
            if (MaxChildren>0)
            {
                sb.AppendFormat(", 'max_children' : {0}", MaxChildren);
            }
            if (MaxDepth > 0)
            {
                sb.AppendFormat(", 'max_depth' : {0}", MaxDepth);
            }
            sb.Append("}");
            return base.ToString();
        }

    }

}
