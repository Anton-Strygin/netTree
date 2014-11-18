using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jsTree;
using jsTree.Plugins;

namespace netTree
{
    public partial class test : System.Web.UI.Page
    {
        private void TreeDataSourceExample()
        {
            simpleTree.DataSourceTree = new NetTreeDataSource<decimal, string>();
            var bNode = new NetTreeDataSource<decimal, string>() { Value = "b" };
            bNode.Add(22, new NetTreeDataSource<decimal, string>() { Value = "subnode b" });
            bNode.Add(23, new NetTreeDataSource<decimal, string>() { Value = "one more subnode" });
            simpleTree.DataSourceTree.Add(1, new NetTreeDataSource<decimal, string>() { Value = "aaa" });
            simpleTree.DataSourceTree.Add(2, bNode);
            simpleTree.DataSourceTree.Add(3, new NetTreeDataSource<decimal, string>() { Value = "cc" });
            simpleTree.SelectedItems.Add(22);
            simpleTree.SelectedItems.Add(3);
        }

        private void FlatDataSourceExample()
        {
            var corePlugin = new NetTreePluginCore {Multiple = true};
            categoryTree.Plugins.Add(corePlugin);
            var categories = new List<Category>
                {
                    new Category {Id = 1, ParentId = 0, Description = "first root node"},
                    new Category {Id = 2, ParentId = 0, Description = "second root node"},
                    new Category {Id = 3, ParentId = 0, Description = "last root node"},
                    new Category {Id = 4, ParentId = 2, Description = "subnode 1"},
                    new Category {Id = 5, ParentId = 2, Description = "subnode 2"},
                    new Category {Id = 6, ParentId = 3, Description = "subnode 3"}
                };
            categoryTree.DataSourceTree = new NetTreeDataSource<int, Category>(categories, item => item.Id, item => item.ParentId);            
            categoryTree.SelectedItems.Add(2);
            categoryTree.SelectedItems.Add(6);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            TreeDataSourceExample();
            FlatDataSourceExample();

            btnPostback.Click += btnPostback_Click;
        }

        void btnPostback_Click(object sender, EventArgs e)
        {
            litSelectedItems.Text = string.Join(", ", categoryTree.SelectedItems);
        }
    }
}