using System.Text;

namespace jsTree.Plugins
{
    public class NetTreePluginThemes : INetTreePlugin
    {
        /// <summary>the name of the theme to use (if not set the default theme is used)</summary>
        public string Theme { get; set; }

        /// <summary>a boolean indicating if connecting dots are shown</summary>
        public bool Dots { get; set; }

        /// <summary>a boolean indicating if node icons are shown</summary>
        public bool Icons { get; set; }

        /// <summary>a boolean indicating if the tree background is striped</summary>
        public bool Stripes { get; set; }

        /// <summary>a boolean specifying if a reponsive version of the theme should 
        /// kick in on smaller screens (if the theme supports it). Defaults to true.</summary>
        public bool Responsive { get; set; }

        public string Name { get { return "themes"; } }

        public string Settings
        {
            get
            {
                var sb = new StringBuilder();                
                sb.Append("'themes': { 'theme': ");
                sb.Append(string.IsNullOrEmpty(Theme) ? "'default'" : Theme);
                sb.AppendFormat(", 'dots': {0}, 'icons': {1}, 'stripes': {2}, 'responsive': {3} ", Dots, Icons, Stripes, Responsive);
                sb.Append("}");
                return sb.ToString();
            }
        }
    }
}
