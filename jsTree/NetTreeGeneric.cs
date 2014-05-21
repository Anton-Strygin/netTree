using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using jsTree.Plugins;

namespace jsTree
{
    /// <summary>
    /// This is the generic class for tree control implementation. 
    /// Any tree control should be derived from this class with types assignment.
    /// </summary>
    /// <typeparam name="TK">Type for node key (identifier)</typeparam>
    /// <typeparam name="TV">Type for node value</typeparam>
    public class NetTreeGeneric<TK, TV> : CompositeControl where TK : IConvertible
    {
        public string JSTreeId
        {
            get { return string.Concat("Tree", this.ClientID); }
        }

        //Javascript event occur when user selects some tree node
        //it should be javascript function name with two parameters - event args and data
        public string JSOnNodeSelected { get; set; }

        public string JSSelectedItems { get { return string.Format("getSelectedNodes('{0}')", JSTreeId); } }

        public string JSSelectedNodesValidation { get; set; }

        public NetTreeDataSource<TK, TV> DataSourceTree { get; set; }


        protected HiddenField _hfSelectedItems;
        private const string SelectedItemsDefaultValue = "default";

        private List<TK> _selectedItems;
        public List<TK> SelectedItems
        {
            get
            {
                return this._selectedItems ?? (this._selectedItems = new List<TK>());
            }
        }

        /// <summary>Control how tree should be shown - expanded or collapsed. Null - is default behaviour.
        /// This setting will be ignored if tree has "state" plugin</summary>
        public bool? InitiallyCollapsed { get; set; }

        /// <summary>Show tree leafs with default leaf icon.
        /// True by default</summary>
        public bool ShowLeafType { get; set; }

        public List<NetTreeNodeType> NodeTypes { get; private set; }

        /// <summary>This message shown if no items selected before form submit</summary>
        public string RequiredItemValidationMessage { get; set; }

        protected virtual string GetNodeId(TK key)
        {
            return string.Format("{0}_{1}", this.JSTreeId, key);
        }

        protected virtual TK ParseNodeId(string id)
        {
            return string.IsNullOrEmpty(id)
                       ? default(TK)
                       : (TK)Convert.ChangeType(id.Substring(this.JSTreeId.Length + 1), typeof(TK));
        }

        public List<INetTreePlugin> Plugins { get; private set; }

        public NetTreeGeneric()
        {
            Plugins = new List<INetTreePlugin>();
            ShowLeafType = true;
            NodeTypes = new List<NetTreeNodeType>();
        }

        protected virtual string GetNodeDescription(TV value)
        {
            return value.ToString();
        }
        
        private List<TK> ParseItems(string strItems)
        {
            if (string.IsNullOrEmpty(strItems)) return new List<TK>();
            var res1 =
                strItems.Split(',').ToList();
            var result = res1.Select(ParseNodeId).ToList();
            return result;
        }

        private void RememberCheckedNodes()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("$('#{0}')", this.Page.Form.ClientID);
            sb.AppendFormat(".submit(rememberCheckedNodes('{0}', '{1}', {2}));", this.JSTreeId, _hfSelectedItems.ClientID, string.IsNullOrEmpty(JSSelectedNodesValidation) ? "null" : JSSelectedNodesValidation);
            ScriptManager.RegisterOnSubmitStatement(this.Page, this.GetType(), "RememberCheckedNodes_" + this.JSTreeId, sb.ToString());
        }

        protected virtual string GetNodeType(NetTreeDataSource<TK, TV> value)
        {
            bool hasChildren = value.Any();
            return hasChildren ? "" : "leaf";
        }

        protected void RenderNode(TK key, NetTreeDataSource<TK, TV> value, HtmlTextWriter writer)
        {
            bool hasChildren = value.Any();
            string nodeType = GetNodeType(value);
            writer.WriteLine("<li id='{0}' {1}>", GetNodeId(key), string.IsNullOrEmpty(nodeType) ? "" : "data-jstree='{\"type\":\""+nodeType+"\"}'");
            writer.WriteLine("<a href='#'>{0}</a>", GetNodeDescription(value.Value));
            if (hasChildren)
            {
                writer.WriteLine("<ul>");
                foreach (KeyValuePair<TK, NetTreeDataSource<TK, TV>> keyValuePair in value)
                {
                    this.RenderNode(keyValuePair.Key, keyValuePair.Value, writer);
                }
                writer.WriteLine("</ul>");
            }
            writer.WriteLine("</li>");
        }

        private void InitializeTree()
        {
            var sb = new StringBuilder();
            string treeInstance = string.Format("$('#{0}')", this.JSTreeId);

            sb.AppendLine("$(function () {");
            sb.AppendFormat("{0}.jstree(", treeInstance);
            if (Plugins.Count > 0)
            {
                sb.AppendLine("{");
                foreach (var treePlugin in Plugins)
                {
                    if (!string.IsNullOrEmpty(treePlugin.Settings))
                        sb.AppendFormat("{0}, \n", treePlugin.Settings);
                }                
                sb.AppendFormat("'plugins' : [ {0} ] \n",
                                string.Join(", ", Plugins.Select(r => string.Format("'{0}'",r.Name)).Where(r => !string.IsNullOrEmpty(r)).ToArray()));
                sb.AppendLine("}");
            }
            sb.AppendLine(");");
            if (InitiallyCollapsed != null)
            {
                if (InitiallyCollapsed.Value)
                    sb.AppendLine(treeInstance + ".jstree(true).close_all();");
                else
                    sb.AppendLine(treeInstance + ".jstree(true).open_all();");
            }

            //when tree is loaded, set selected items, expande/collapse, and show the tree
            if (this.SelectedItems.Count > 0)
            {
                foreach (TK item in this.SelectedItems)
                {
                    sb.AppendFormat(treeInstance + ".jstree(true).select_node('{1}');\n", this.JSTreeId, GetNodeId(item));
                }
            }
            //add event handler OnNodeSelected if necessary
            if (!string.IsNullOrEmpty(JSOnNodeSelected))
            {
                sb.AppendFormat("bindNodeSelection('{0}', {1});\n", this.JSTreeId, JSOnNodeSelected);
            }

            sb.AppendLine(treeInstance + ".show();");
            sb.AppendLine("});");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "InitializeTree_" + this.JSTreeId, sb.ToString(), true);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Page.IsPostBack && _hfSelectedItems.Value != SelectedItemsDefaultValue)
            {
                // restore Checked items
                string checkedItemsStr = _hfSelectedItems.Value;
                this._selectedItems = this.ParseItems(checkedItemsStr);
            }
        }
        
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            _hfSelectedItems = new HiddenField { ID = "hfSelectedItems", Value = SelectedItemsDefaultValue };
            this.Controls.Add(_hfSelectedItems);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (ShowLeafType)
            {
                string leafIconUrl = this.Page.ResolveUrl("~/scripts/jsTree/themes/netTree/leaf_node.gif");
                var leafType = new NetTreeNodeType
                {
                    Icon = leafIconUrl,
                    Name = "leaf"
                };
                NodeTypes.Add(leafType);
            }

            if (NodeTypes.Count > 0)
            {
                this.Plugins.Add(new NetTreePluginTypes(NodeTypes));
            }

            InitializeTree();
            RememberCheckedNodes();            
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            _hfSelectedItems.RenderControl(writer);
            writer.WriteLine("<div id='{0}' style='display:none;'>", this.JSTreeId);
            writer.WriteLine("<ul>");
            foreach (KeyValuePair<TK, NetTreeDataSource<TK, TV>> keyValuePair in DataSourceTree)
            {
                this.RenderNode(keyValuePair.Key, keyValuePair.Value, writer);
            }
            writer.WriteLine("</ul>");
            writer.WriteLine("</div>");
        }

    }
}
