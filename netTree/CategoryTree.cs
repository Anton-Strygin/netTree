using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jsTree;
using jsTree.Plugins;

namespace netTree
{
    public class CategoryTree : NetTreeCheckbox<int, Category>
    {
        public CategoryTree()
        {
            RootNode = "Select all";
            TwoState = true;            
        }

        protected override string GetNodeDescription(Category value)
        {
            return value.Description;
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
    }

    
}