using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TreeFilterable;
using UnityEngine;

namespace OniExtract2024
{
    public class OutTreeFilterable
    {
        public BColor filterTint = null;
        public BColor noFilterTint = null;
        [SerializeField]
        public bool dropIncorrectOnFilterChange = true;
        [SerializeField]
        public bool autoSelectStoredOnLoad = true;
        public bool showUserMenu = true;
        public bool copySettingsEnabled = true;
        public bool preventAutoAddOnDiscovery;
        public bool filterAllStoragesOnBuilding;
        public UISideScreenHeight uiHeight = UISideScreenHeight.Tall;
        public HashSet<Tag> AcceptedTags;
        public HashSet<Tag> acceptedTagSet;
        public bool filterByStorageCategoriesOnSpawn = true;

        public OutTreeFilterable(TreeFilterable obj) {
            this.filterTint = new BColor(obj.filterTint);
            this.noFilterTint = new BColor(obj.noFilterTint);
            this.dropIncorrectOnFilterChange |= obj.dropIncorrectOnFilterChange;
            this.autoSelectStoredOnLoad = obj.autoSelectStoredOnLoad;
            this.showUserMenu = obj.showUserMenu;
            this.copySettingsEnabled = obj.copySettingsEnabled;
            this.preventAutoAddOnDiscovery = obj.preventAutoAddOnDiscovery;
            this.filterAllStoragesOnBuilding = obj.filterAllStoragesOnBuilding;
            this.uiHeight = obj.uiHeight;
            this.AcceptedTags = obj.AcceptedTags;
            this.acceptedTagSet = obj.GetTags();
            this.filterAllStoragesOnBuilding |= obj.filterAllStoragesOnBuilding;
        }
    }
}
