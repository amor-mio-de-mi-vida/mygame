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

    public class ChatEvent : GameEvent
    {
        public string Name;
    }

    public class ChatOverEvent : GameEvent
    {
    }
}
