using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingOptions : MonoBehaviour
    {
        string m_Name;
        ChatType m_ChatType;
        PlayerInputHandler m_InputHandler;
        VisualElement m_RootVisualElement;
        Button m_Option1;
        Button m_Option2;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            m_RootVisualElement.style.display = DisplayStyle.None;
            m_InputHandler = GetComponent<PlayerInputHandler>();
            EventManager.AddListener<ChatEvent>(OnChat);
            EventManager.AddListener<ChatOverEvent>(OnChatOver);

            m_Option1 = m_RootVisualElement.Query<Button>("Option1");
            m_Option2 = m_RootVisualElement.Query<Button>("Option2");
            m_Option1.RegisterCallback<ClickEvent>(ClickOption1);
            m_Option2.RegisterCallback<ClickEvent>(ClickOption2);
        }

        void start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnChat(ChatEvent evt)
        {
            if (evt.Type == ChatType.Options)
            {
                m_RootVisualElement.style.display = DisplayStyle.Flex;
                m_Name = evt.Name;
                m_ChatType = evt.Type;
                m_Option1.text = evt.Option1;
                m_Option2.text = evt.Option2;
            }
        }

        void OnChatOver(ChatOverEvent evt)
        {
            m_RootVisualElement.style.display = DisplayStyle.None;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }

        private void ClickOption1(ClickEvent evt1)
        {
            ChatBackEvent evt = Events.ChatBackEvent;
            evt.Type = ChatType.Options;
            evt.Option = 1;
            evt.Chat = m_Option1.text;
            EventManager.Broadcast(evt);
        }

        private void ClickOption2(ClickEvent evt1)
        {
            ChatBackEvent evt = Events.ChatBackEvent;
            evt.Type = ChatType.Options;
            evt.Option = 2;
            evt.Chat = m_Option2.text;
            EventManager.Broadcast(evt);
        }
    }
}
