using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    [RequireComponent(typeof(PlayerInputHandler))]
    public class ChattingInputs : MonoBehaviour
    {
        string m_Name;
        ChatType m_ChatType;
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
            if (m_InputHandler.GetSend())
            {
                ChatBackEvent evt = Events.ChatBackEvent;
                evt.Type = ChatType.Input;
                evt.Chat = m_Input.value;
                EventManager.Broadcast(evt);
                m_Input.value = "";
            }
        }

        void OnChat(ChatEvent evt)
        {
            if (evt.Type == ChatType.Input)
            {
                m_RootVisualElement.style.display = DisplayStyle.Flex;
                m_Name = evt.Name;
                m_ChatType = evt.Type;
                m_Input.Focus();
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

        private void Input(InputEvent evt)
        {
            // Debug.Log("from " + evt.previousData + " to " + evt.newData);
        }
    }
}
