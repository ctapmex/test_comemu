using System;
using System.Windows.Forms;
using Remote4Support.Gui;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Utils
{
    public class SingletonToolWindowHelper<T> where T : ToolWindow
    {
        public delegate T WindowInitializer(SingletonToolWindowHelper<T> helper);
        public delegate void InstanceChangedHandler(T instance);
        public event InstanceChangedHandler InstanceChanged;

        public string Name { get; }
        public DockPanel DockPanel { get; }
        public WindowInitializer Initializer { get; }
        public Object InitializerResource { get; }
        public T Instance { get; private set; }

        public SingletonToolWindowHelper(string name, DockPanel dockPanel) : this(name, dockPanel, null, null)
        {
        }

        public SingletonToolWindowHelper(string name, DockPanel dockPanel, Object initializerResource,
            WindowInitializer initializer)
        {
            Name = name;
            DockPanel = dockPanel;
            Initializer = initializer;
            InitializerResource = initializerResource;
        }

        public void ShowWindow(DockState dockState)
        {
            if (Instance == null)
            {
                Initialize();
                Instance.Show(DockPanel, dockState);
            }
            else
            {
                Instance.Show(DockPanel);
            }
        }

        public T Initialize()
        {
            Instance = Initializer == null ? Activator.CreateInstance<T>() : Initializer(this);

            Instance.FormClosed += new FormClosedEventHandler(Instance_FormClosed);
            InstanceChanged?.Invoke(Instance);
            return Instance;
        }

        void Instance_FormClosed(object sender, FormClosedEventArgs e)
        {
            Instance = null;
        }
    }
}