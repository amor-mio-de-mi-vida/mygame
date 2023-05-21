using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingManager : MonoBehaviour
    {
        bool m_Reading = false;
        bool m_PrevReading = false;
        PlayerInputHandler m_InputHandler;
        VisualElement m_RootVisualElement;
        VisualElement m_Panel;
        Label m_Name;
        Label m_SubName;
        Label m_Chat;
        ChatType m_ChatType;

        GameObject m_Player;
        PlayerCharacterController m_PlayerCharacterController;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            m_RootVisualElement.style.display = DisplayStyle.None;
            m_Panel= m_RootVisualElement.Query<VisualElement>("Panel");
            m_Name = m_RootVisualElement.Query<Label>("Name");
            m_SubName = m_RootVisualElement.Query<Label>("SubName");
            m_Chat = m_RootVisualElement.Query<Label>("Chat");

            m_InputHandler = GetComponent<PlayerInputHandler>();
            EventManager.AddListener<ChatEvent>(OnChat);
            EventManager.AddListener<ChatOverEvent>(OnChatOver);
            m_Panel.RegisterCallback<ClickEvent>(OnClick);

            m_Player = GameObject.FindWithTag("P");
            m_PlayerCharacterController = m_Player.GetComponent<PlayerCharacterController>();
        }

        void start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (m_PrevReading)
            {
                if (m_InputHandler.GetChat())
                {
                    Read();
                }
            }
            m_PrevReading = m_Reading;
        }

        void OnChat(ChatEvent evt)
        {
            m_RootVisualElement.style.display = DisplayStyle.Flex;
            m_Name.text = evt.Name;
            m_SubName.text = evt.SubName;
            m_Chat.text = evt.Chat;
            m_ChatType = evt.Type;
            if (evt.Type == ChatType.Read)
            {
                m_Reading = true;
            }
        }

        void OnChatOver(ChatOverEvent evt)
        {
            m_RootVisualElement.style.display = DisplayStyle.None;
            m_Reading = false;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }

        private void OnClick(ClickEvent evt)
        {
            if (m_ChatType == ChatType.Read)
            {
                Read();
            }
        }

        private void Read()
        {
            ChatBackEvent evt = Events.ChatBackEvent;
            evt.Type = ChatType.Read;
            EventManager.Broadcast(evt);
        }
    }
}
