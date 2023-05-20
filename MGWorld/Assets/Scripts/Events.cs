using UnityEngine;

namespace MyGame
{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.

    public static class Events
    {
        public static ChatEvent ChatEvent = new ChatEvent();
        public static ChatOverEvent ChatOverEvent = new ChatOverEvent();
    }

    public enum ChatType
    {
        Read,
        Options,
        Input
    }

    public class ChatEvent : GameEvent
    {
        public string Name;
        public ChatType Type;
    }

    public class ChatOverEvent : GameEvent
    {
        public string Name;
    }
}
