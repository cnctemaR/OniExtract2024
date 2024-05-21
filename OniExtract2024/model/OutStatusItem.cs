using System;
using static StatusItem;

namespace OniExtract2024
{
    public class OutStatusItem
    {
        public string Name;
        public string Id;
        public HashedString IdHash;
        public bool Disabled;
        public ResourceGuid Guid;
        public string tooltipText;
        public string notificationText;
        public string notificationTooltipText;
        public string soundPath;
        public string iconName;
        public bool unique;
        public TintedSprite sprite;
        public bool shouldNotify;
        public IconType iconType;
        public NotificationType notificationType;
        public Notification.ClickCallback notificationClickCallback;
        //public Func<string, object, string> resolveStringCallback;
        //public Func<string, object, string> resolveTooltipCallback;
        public bool resolveStringCallback_shouldStillCallIfDataIsNull;
        public bool resolveTooltipCallback_shouldStillCallIfDataIsNull;
        public bool allowMultiples;
        //public Func<HashedString, object, bool> conditionalOverlayCallback;
        public HashedString render_overlay;
        public int status_overlays;
        public Action<object> statusItemClickCallback;


        public OutStatusItem(StatusItem obj)
        {
            this.Name = obj.Name;
            this.Id = obj.Id;
            this.IdHash = obj.IdHash;
            this.Disabled = obj.Disabled;
            this.Guid = obj.Guid;
            this.tooltipText = obj.tooltipText;
            this.notificationText = obj.notificationText;
            this.notificationTooltipText = obj.notificationTooltipText;
            this.soundPath = obj.soundPath;
            this.iconName = obj.iconName;
            this.unique = obj.unique;
            this.sprite = obj.sprite;
            this.shouldNotify = obj.shouldNotify;
            this.iconType = obj.iconType;
            this.notificationType = obj.notificationType;
            this.notificationClickCallback = obj.notificationClickCallback;
            this.resolveStringCallback_shouldStillCallIfDataIsNull = obj.resolveStringCallback_shouldStillCallIfDataIsNull;
            this.resolveTooltipCallback_shouldStillCallIfDataIsNull = obj.resolveTooltipCallback_shouldStillCallIfDataIsNull;
            this.allowMultiples = obj.allowMultiples;
            this.render_overlay = obj.render_overlay;
            this.status_overlays = obj.status_overlays;
            this.statusItemClickCallback = obj.statusItemClickCallback;
        }
    }
}
