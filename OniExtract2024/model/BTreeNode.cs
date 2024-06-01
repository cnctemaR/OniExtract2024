using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniExtract2024
{
    public class BTreeNode
    {
        public string name;
        public string parentName;
        public List<BTreeNode> children;

        public BTreeNode(string name)
        {
            this.name = name;
            this.parentName = "";
            this.children = new List<BTreeNode>();
        }

        public static List<BTreeNode> BuildTree(List<BTreeNode> nodes)
        {
            var nodeDict = new Dictionary<string, BTreeNode>();
            foreach (var nodeData in nodes)
            {
                if (!nodeDict.ContainsKey(nodeData.name))
                {
                    nodeDict.Add(nodeData.name, nodeData);
                }
            }

            var roots = new List<BTreeNode>();
            foreach (var nodeData in nodes)
            {
                BTreeNode node = null;
                if (!nodeDict.ContainsKey(nodeData.name))
                {
                    node = new BTreeNode(nodeData.name);
                    nodeDict[nodeData.name] = node;
                }
                else
                {
                    node = nodeDict[nodeData.name];
                }
                string parentName = nodeData.parentName;
                if (!string.IsNullOrEmpty(parentName))
                {
                    BTreeNode parent = null;
                    if (!nodeDict.ContainsKey(parentName))
                    {
                        parent = new BTreeNode(parentName);
                        nodeDict[parentName] = parent;
                    }
                    parent = nodeDict[parentName];
                    parent.children.Add(node);
                    
                }
                else
                {
                    roots.Add(node);
                }
            }

            return roots;
        }
    }
}
