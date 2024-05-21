using System.Collections.Generic;
using UnityEngine;

namespace OniExtract2024
{ 
    public class OutCodexEntry
    {
        public EntryDevLog log = new EntryDevLog();
        //public List<ContentContainer> contentContainers = new List<ContentContainer>();
        public string[] dlcIds;
        public string[] forbiddenDLCIds;
        public string[] NONE = new string[0];
        public string id;
        public string parentId;
        public string category;
        public string title;
        public string name;
        public string subtitle;
        public List<SubEntry> subEntries = new List<SubEntry>();
        public Sprite icon;
        public BColor iconColor;
        public string iconPrefabID;
        public string iconLockID;
        public string iconAssetName;
        public bool disabled;
        public bool searchOnly;
        public int customContentLength;
        public string sortString;
        public bool showBeforeGeneratedCategoryLinks;

        public OutCodexEntry(CodexEntry codexEntry) {
            this.log = codexEntry.log;
            //this.contentContainers = codexEntry.contentContainers;
            this.dlcIds = codexEntry.dlcIds;
            this.forbiddenDLCIds = codexEntry.forbiddenDLCIds;
            this.id = codexEntry.id;
            this.parentId = codexEntry.parentId;
            this.category = codexEntry.category;
            this.title = codexEntry.title;
            this.name = codexEntry.name;
            this.subtitle = codexEntry.subtitle;
            this.subEntries = codexEntry.subEntries;
            this.icon = codexEntry.icon;
            this.iconColor = new BColor(codexEntry.iconColor);
            this.iconPrefabID = codexEntry.iconPrefabID;
            this.iconLockID = codexEntry.iconLockID;
            this.iconAssetName = codexEntry.iconAssetName;
            this.disabled = codexEntry.disabled;
            this.searchOnly = codexEntry.searchOnly;
            this.customContentLength = codexEntry.customContentLength;
            this.sortString = codexEntry.sortString;
            this.showBeforeGeneratedCategoryLinks = codexEntry.showBeforeGeneratedCategoryLinks;
        }
    }
}
