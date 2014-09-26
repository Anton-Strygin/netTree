using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using jsTree.Plugins;

namespace jsTree
{
    public class NetTreeCheckbox<TK, TV> : NetTreeGeneric<TK, TV> where TK : IConvertible
    {
        private readonly NetTreePluginCheckbox _cbPlugin = new NetTreePluginCheckbox();

        protected string RootNodeId
        {
            get
            {
                return "RootNode_" + JSTreeId;
            }
        }

        /// <summary>
        /// Root node description. If not empty, root node will be added.
        /// </summary>
        public string RootNode { get; set; }

        public NetTreePluginCheckbox CheckboxPluginSettings { get { return _cbPlugin; } }

        public NetTreeCheckbox()
        {                        
            Plugins.Add(_cbPlugin);
        }

        public override string JSSelectedItems
        {
            get
            {
                return !string.IsNullOrEmpty(RootNode)
                        ? string.Format("getSelectedNodes('{0}', '{1}')", JSTreeId, RootNodeId)
                        : base.JSSelectedItems;
            }
        }

        public bool TwoState
        {
            get { return !_cbPlugin.ThreeState; }
            set { _cbPlugin.ThreeState = !value; }
        }

        protected override bool ParseNodeId(string id, out TK parsed)
        {
            parsed = default(TK);
            if (!string.IsNullOrEmpty(RootNode))
            {
                if (id == RootNodeId) return false;
            }            
            return base.ParseNodeId(id, out parsed);
        }

        /// <summary>
        /// This method used to make root node work as Select All / Select None. Used only in case of two_state is true
        /// </summary>
        private void BindCheckNodeEvent()
        {
            if (TwoState && !string.IsNullOrEmpty(RootNode))
            {
                int amountOfTreeNodes = this.DataSourceTree.Amount + 1;// +1 - is a root node
                var sb = new StringBuilder();
                sb.AppendLine("$(function () {");
                sb.AppendLine("$('#" + this.JSTreeId + "').bind('select_node.jstree', {amountOfTreeNodes : " + amountOfTreeNodes + ", rootNodeId : '" + RootNodeId + "'}, selectNode);");
                sb.AppendLine("$('#" + this.JSTreeId + "').bind('deselect_node.jstree', {amountOfTreeNodes : " + amountOfTreeNodes + ", rootNodeId : '" + RootNodeId + "'}, deselectNode);");
                sb.AppendFormat("setRootNodeState('{0}', '{1}', '{2}', '{3}');\n", JSTreeId, RootNodeId, this.SelectedItems.Count, amountOfTreeNodes);
                sb.AppendLine("});");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "BindCheckNodes_" + this.JSTreeId, sb.ToString(), true);
            }
        }        

        protected override void OnPreRender(EventArgs e)
        {            
            base.OnPreRender(e);
            BindCheckNodeEvent();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            _hfSelectedItems.RenderControl(writer);
            writer.WriteLine("<div id='{0}' style='display:none;'>", this.JSTreeId);
            writer.WriteLine("<ul>");
            if (!string.IsNullOrEmpty(RootNode))
            {
                writer.WriteLine("<li id='{0}'>", RootNodeId);
                writer.WriteLine("<a href='#' class='rootNode'>{0}</a>", RootNode);
                writer.WriteLine("<ul>");
            }
            foreach (KeyValuePair<TK, NetTreeDataSource<TK, TV>> keyValuePair in DataSourceTree)
            {
                RenderNode(keyValuePair.Key, keyValuePair.Value, writer);
            }
            if (!string.IsNullOrEmpty(RootNode))
            {
                writer.WriteLine("</ul>");
                writer.WriteLine("</li>");
            }
            writer.WriteLine("</ul>");
            writer.WriteLine("</div>");
        }

    }
}
