using System.Collections.Generic;
using System.Linq;

namespace jsTree
{

    public class NetTreeDataSource<TK, TV> : Dictionary<TK, NetTreeDataSource<TK, TV>>
    {
        public NetTreeDataSource() {}

        public NetTreeDataSource(IList<TV> flatDataSource, GetKey getKey, GetKey getParentKey)
        {
            foreach (TV item in flatDataSource)
            {
                if (Equals(getParentKey(item), default(TK))) // add top-level items (without parents)
                {
                    var node = new NetTreeDataSource<TK, TV> { Value = item };
                    FillNode(node, flatDataSource, getKey, getParentKey);
                    this.Add(getKey(item), node);
                }
            }
        }

        /// <summary>
        /// Add children to node. Return true if node has any children, false otherwise.
        /// </summary>
        private bool FillNode(NetTreeDataSource<TK, TV> node, IList<TV> items, GetKey getKey, GetKey getParentKey)
        {
            TK nodeId = getKey(node.Value);
            // find all children for current node

            List<TV> children = items.Where(r => Equals(getParentKey(r), nodeId)).ToList();

            foreach (TV child in children)
            {
                var childNode = new NetTreeDataSource<TK, TV> { Value = child };
                this.FillNode(childNode, items, getKey, getParentKey);
                node.Add(getKey(child), childNode);
            }
            return children.Count > 0;
        }
        
        


        private int GetAmount(NetTreeDataSource<TK, TV> tree)
        {
            if (tree == null) return 0;
            int result = tree.Count;
            foreach (KeyValuePair<TK, NetTreeDataSource<TK, TV>> keyValuePair in tree)
            {
                result += GetAmount(keyValuePair.Value);
            }
            return result;
        }

        public delegate TK GetKey(TV item);

        public int Amount
        {
            get { return this.GetAmount(this); }
        }

        public TV Value { get; set; }
    }
}
