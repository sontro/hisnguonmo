using System.Collections.Generic;

namespace HIS.Desktop.Plugins.HisTutorial
{
    public class NavBarFilter : System.IDisposable
    {
        //Fields
        private Dictionary<DevExpress.XtraNavBar.NavBarGroup, bool> initialGroupsVisibility;
        private Dictionary<DevExpress.XtraNavBar.NavBarItemLink, bool> initialLinksVisibility;
        private DevExpress.XtraNavBar.NavBarItemLink initialSelectedLink;
        private DevExpress.XtraNavBar.NavBarControl navBar;

        //Methods
        public NavBarFilter(DevExpress.XtraNavBar.NavBarControl NavBar)
            : base()
        {
            navBar = NavBar;
        }


        //Properties
        protected DevExpress.XtraNavBar.NavBarControl NavBar
        {
            get
            {
                return navBar;
            }
        }
        public void FilterNavBar(string text)
        {
            if (initialLinksVisibility == null)
                UpdateLinksVisibility();
            if (initialGroupsVisibility == null)
                UpdateGroupsVisibility();
            if (NavBar.SelectedLink != null)
                initialSelectedLink = NavBar.SelectedLink;
            text = text.ToLowerInvariant();
            NavBar.BeginUpdate();
            try
            {
                foreach (KeyValuePair<DevExpress.XtraNavBar.NavBarItemLink, bool> current in initialLinksVisibility)
                {
                    string toLowerInvariant = current.Key.Caption.ToLowerInvariant();
                    current.Key.Visible = string.IsNullOrEmpty(text) || toLowerInvariant.Contains(text);
                }
                foreach (DevExpress.XtraNavBar.NavBarGroup key in NavBar.Groups)
                {
                    if (key.VisibleItemLinks.Count == 0)
                    {
                        key.Visible = false;
                        continue;
                    }
                    key.Visible = initialGroupsVisibility.ContainsKey(key);
                }
                CheckSelectedLink();
            }
            finally
            {
                NavBar.EndUpdate();
            }
        }
        private void UpdateLinksVisibility()
        {
            initialLinksVisibility = new Dictionary<DevExpress.XtraNavBar.NavBarItemLink, bool>();
            initialSelectedLink = NavBar.SelectedLink;
            foreach (DevExpress.XtraNavBar.NavBarGroup navBarGroup in NavBar.Groups)
                foreach (DevExpress.XtraNavBar.NavBarItemLink key in navBarGroup.ItemLinks)
                    if (key.Visible)
                        initialLinksVisibility[key] = true;
                    else
                        initialLinksVisibility[key] = false;
        }
        private void UpdateGroupsVisibility()
        {
            initialGroupsVisibility = new Dictionary<DevExpress.XtraNavBar.NavBarGroup, bool>();
            foreach (DevExpress.XtraNavBar.NavBarGroup key in NavBar.Groups)
                if (key.Visible)
                    initialGroupsVisibility[key] = true;
                else
                    initialGroupsVisibility[key] = false;
        }
        private void CheckSelectedLink()
        {
            if (initialSelectedLink == NavBar.SelectedLink)
                return;
            if ((NavBar.SelectedLink != null) && (NavBar.SelectedLink.Group != null))
                NavBar.SelectedLink.Group.SelectedLinkIndex = -1;
            if ((NavBar.SelectedLink != null) || (initialSelectedLink == null) || !initialSelectedLink.Visible || !initialSelectedLink.Group.Visible)
                return;
            NavBar.SelectedLink = initialSelectedLink;
        }

        public virtual void Dispose()
        {
            if (initialGroupsVisibility != null)
                initialGroupsVisibility.Clear();
            if (initialLinksVisibility != null)
                initialLinksVisibility.Clear();
            initialSelectedLink = null;
        }
        public void Reset()
        {
            FilterNavBar("");
            initialGroupsVisibility = null;
            initialLinksVisibility = null;
            initialSelectedLink = null;
        }
    }
}
