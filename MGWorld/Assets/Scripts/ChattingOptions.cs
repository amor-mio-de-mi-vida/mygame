using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingOptions : MonoBehaviour
    {
        bool m_Chattable = false;
        bool m_Chatting = false;
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
            if (m_Chatting)
            {
                if (m_InputHandler.GetChat())
                {
                    m_RootVisualElement.style.display = DisplayStyle.None;
                    m_Chatting = false;
                }
            }
            else if (m_Chattable)
            {
                if (m_InputHandler.GetChat())
                {
                    if (m_ChatType == ChatType.Options)
                    {
                        m_RootVisualElement.style.display = DisplayStyle.Flex;
                        m_Chatting = true;
                    }
                }
            }
        }

        void OnChat(ChatEvent evt)
        {
            m_Chattable = true;
            m_Name = evt.Name;
            m_ChatType = evt.Type;
        }

        void OnChatOver(ChatOverEvent evt)
        {
            if (m_Chatting)
            {
                m_RootVisualElement.style.display = DisplayStyle.None;
                m_Chatting = false;
            }
            m_Chattable = false;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }

        private void ClickOption1(ClickEvent evt)
        {
            Debug.Log("Option1 was clicked!");
        }

        private void ClickOption2(ClickEvent evt)
        {
            Debug.Log("Option2 was clicked!");
        }
    }
}
