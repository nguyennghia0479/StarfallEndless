using UnityEngine;

public static class GameIdentifiers
{
    public static class SharderIDs
    {
        public static readonly int BASE_COLOR = Shader.PropertyToID("_BaseColor");
    }

    public static class GameTags
    {
        public const string TAG_PLAYER = "Player";
        public const string TAG_ENEMY = "Enemy";
    }

    public static class Locales
    {
        public const string LOCALES_EN = "en";
        public const string LOCALES_VI = "vi";
    }
}
