using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingManager : MonoBehaviour
    {
        bool m_Chattable = false;
        bool m_Chatting = false;
        string m_Name;
        PlayerInputHandler m_InputHandler;
        VisualElement m_RootVisualElement;
        Label m_Dialog_Name;
        GameObject m_Player;
        PlayerCharacterController m_PlayerCharacterController;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            m_RootVisualElement.style.display = DisplayStyle.None;
            m_Dialog_Name = m_RootVisualElement.Query<Label>("Name");
            m_InputHandler = GetComponent<PlayerInputHandler>();
            EventManager.AddListener<ChatEvent>(OnChat);
            EventManager.AddListener<ChatOverEvent>(OnChatOver);

            m_Player = GameObject.FindWithTag("P");
            m_PlayerCharacterController = m_Player.GetComponent<PlayerCharacterController>();
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
                    m_PlayerCharacterController.CanMove = true;
                }
            }
            else if (m_Chattable)
            {
                if (m_InputHandler.GetChat())
                {
                    m_RootVisualElement.style.display = DisplayStyle.Flex;
                    m_Chatting = true;
                    m_PlayerCharacterController.CanMove = false;
                    m_Dialog_Name.text = m_Name;
                }
            }
        }

        void OnChat(ChatEvent evt)
        {
            m_Chattable = true;
            m_Name = evt.Name;
        }

        void OnChatOver(ChatOverEvent evt)
        {
            if (m_Chatting)
            {
                m_RootVisualElement.style.display = DisplayStyle.None;
                m_Chatting = false;
                m_PlayerCharacterController.CanMove = true;
            }
            m_Chattable = false;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }
    }
}
