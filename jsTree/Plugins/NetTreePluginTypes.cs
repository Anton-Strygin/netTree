using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jsTree.Plugins
{
    /// <summary>This plugin is internal. To add node types, use tree public property NodeTypes</summary>
    internal class NetTreePluginTypes : INetTreePlugin
    {
        public NetTreePluginTypes() : this(new List<NetTreeNodeType>())
        {}

        public NetTreePluginTypes(List<NetTreeNodeType> types)
        {
            Types = types;
        }

        public string Name { get { return "types"; } }

        public List<NetTreeNodeType> Types { get; private set; }

        public string Settings { 
            get
            {
                if (Types == null || Types.Count == 0) return "";
                var sb = new StringBuilder();
                sb.AppendLine("'types': {");
                sb.AppendLine(string.Join(",\n", Types.Select(r => r.ToString()).ToArray()));                
                sb.AppendLine("}");
                return sb.ToString();
            }
        }
    }
}
