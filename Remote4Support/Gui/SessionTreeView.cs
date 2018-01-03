
using System;
using System.Collections;
using System.Windows.Forms;
using log4net;
using Remote4Support.Data;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class SessionTreeView : ToolWindow, IComparer
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        TreeNode nodeRoot;
        public const string SessionIdDelim = "/";


        public SessionTreeView(DockPanel dockPanel)
        {
            InitializeComponent();

            treeView1.HideSelection = false;
            treeView1.TreeViewNodeSorter = this;
            LoadSessionsInTree();
        }

        private void LoadSessionsInTree()
        {
            treeView1.Nodes.Clear();

            nodeRoot = treeView1.Nodes.Add("root", "Sessions");

            foreach (SessionBase session in Remote4Support.GetAllSessions())
            {
                TryAddSessionNode(session);
            }
            ResortNodes();
        }

        private void TryAddSessionNode(SessionBase session)
        {
            TreeNode nodeParent = nodeRoot;
            if (session.SessionId != null && session.SessionId != session.SessionName)
            {
                // take session id and create folder nodes
                nodeParent = FindOrCreateParentNode(session.SessionId);
            }
            AddSessionNode(nodeParent, session, true);

        }

        TreeNode FindOrCreateParentNode(string sessionId)
        {
            Log.DebugFormat("Finding Parent Node for sessionId ({0})", sessionId);
            TreeNode nodeParent = nodeRoot;

            string[] parts = sessionId.Split(SessionIdDelim.ToCharArray());
            if (parts.Length > 1)
            {
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    string part = parts[i];
                    TreeNode node = nodeParent.Nodes[part];
                    if (node == null)
                    {
                        nodeParent = this.AddFolderNode(nodeParent, part);
                    }
                    else if (IsFolderNode(node))
                    {
                        nodeParent = node;
                    }
                }
            }

            Log.DebugFormat("Returning node ({0})", nodeParent.Text);
            return nodeParent;
        }

        TreeNode AddSessionNode(TreeNode parentNode, SessionBase session, bool isInitializing)
        {
            TreeNode addedNode = null;
            if (parentNode.Nodes.ContainsKey(session.SessionName))
            {
                Log.WarnFormat("Node with the same name exists.  New node ({0}) NOT added", session.SessionName);
            }
            else
            {
                addedNode = parentNode.Nodes.Add(session.SessionName, session.SessionName);
                addedNode.Tag = session;
            }

            return addedNode;
        }

        TreeNode AddFolderNode(TreeNode parentNode, string nodeName)
        {
            TreeNode nodeNew = null;
            if (parentNode.Nodes.ContainsKey(nodeName))
            {
                Log.WarnFormat("Node with the same name exists.  New node ({0}) NOT added", nodeName);
            }
            else
            {
                Log.DebugFormat("Adding new folder, {1}.  parent={0}", parentNode.Text, nodeName);
                nodeNew = parentNode.Nodes.Add(nodeName, nodeName);
            }
            return nodeNew;
        }

        private bool IsSessionNode(TreeNode node)
        {
            return node != null && node.Tag is SessionBase;
        }

        private bool IsFolderNode(TreeNode node)
        {
            return !IsSessionNode(node);
        }

        public int Compare(object x, object y)
        {
            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            return String.CompareOrdinal(tx.Text, ty.Text);
        }

        void ResortNodes()
        {
            this.treeView1.TreeViewNodeSorter = null;
            this.treeView1.TreeViewNodeSorter = this;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e != null ? e.Node : treeView1.SelectedNode;

            if (IsSessionNode(node) && node == treeView1.SelectedNode)
            {
                SessionBase sessionData = (SessionBase)node.Tag;
                Remote4Support.OpenSession(sessionData);
            }
        }
    }
}
