using System.Linq;
using System.Text;

namespace jsTree.Plugins
{
    public class NetTreePluginTypes : INetTreePlugin
    {
        public string Name { get { return "types"; } }

        public NetTreeNodeType[] Types { get; set; }

        public string Settings { 
            get
            {
                if (Types == null || Types.Length == 0) return "";
                var sb = new StringBuilder();
                sb.AppendLine("'types': {");
                sb.AppendLine(string.Join(",\n", Types.Select(r => r.ToString())));                
                sb.AppendLine("}");
                return sb.ToString();
            }
        }
    }
}
