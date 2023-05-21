using UnityEngine;

namespace MyGame
{
    // The Game Events used across the Game.
    // Anytime there is a need for a new event, it should be added here.

    public static class Events
    {
        public static ChatEvent ChatEvent = new ChatEvent();
        public static ChatOverEvent ChatOverEvent = new ChatOverEvent();
        public static ApproachEvent ApproachEvent = new ApproachEvent();
        public static ApproachOverEvent ApproachOverEvent = new ApproachOverEvent();
        public static ChatBackEvent ChatBackEvent = new ChatBackEvent();
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
        public string SubName;
        public string Chat;
        public ChatType Type;
        public string Option1;
        public string Option2;

        public ChatEvent()
        {
            
        }

        public ChatEvent(string name, string subName, string chat)
        {
            Name = name;
            SubName = subName;
            Chat = chat;
            Type = ChatType.Read;
        }

        public ChatEvent(string name, string subName, string chat, string option1, string option2)
        {
            Name = name;
            SubName = subName;
            Chat = chat;
            Type = ChatType.Options;
            Option1 = option1;
            Option2 = option2;
        }

        public ChatEvent(string name, string subName, string chat, string label)
        {
            Name = name;
            SubName = subName;
            Chat = chat;
            Type = ChatType.Input;
        }
    }

    public class ChatOverEvent : GameEvent
    {
        public string Name;
    }

    public class ApproachEvent : GameEvent
    {
        public string Name;
    }

    public class ApproachOverEvent : GameEvent
    {
        public string Name;
    }

    public class ChatBackEvent : GameEvent
    {
        public ChatType Type;
        public int Option;
        public string Chat;
    }
}
