using UnityEngine;

namespace OniExtract2024
{
    public class BUISprite
    {
        public string id;
        public string spriteName;
        public string textureName;
        public BColor color = null;

        public BUISprite(string id, Sprite sprite, Color color)
        {
            this.id = id;
            this.spriteName = sprite.name;
            this.textureName = sprite.texture.name;
            this.color = new BColor(color);
        }
        public BUISprite(string id, Sprite sprite)
        {
            this.id = id;
            this.spriteName = sprite.name;
            this.textureName = sprite.texture.name;
        }
    }
}
