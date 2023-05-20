using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingInputs : MonoBehaviour
    {
        bool m_Chattable = false;
        bool m_Chatting = false;
        string m_Name;
        PlayerInputHandler m_InputHandler;
        VisualElement m_RootVisualElement;
        TextField m_Input;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            m_RootVisualElement.style.display = DisplayStyle.None;
            m_InputHandler = GetComponent<PlayerInputHandler>();
            EventManager.AddListener<ChatEvent>(OnChat);
            EventManager.AddListener<ChatOverEvent>(OnChatOver);

            m_Input = m_RootVisualElement.Query<TextField>("Input");
            m_Input.RegisterCallback<InputEvent>(Input);
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
                    m_RootVisualElement.style.display = DisplayStyle.Flex;
                    m_Chatting = true;
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
            }
            m_Chattable = false;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }

        private void Input(InputEvent evt)
        {
            Debug.Log("from " + evt.previousData + " to " + evt.newData);
        }
    }
}
