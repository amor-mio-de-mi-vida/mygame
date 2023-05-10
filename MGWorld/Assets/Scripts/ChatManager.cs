using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyGame
{
    public class ChatManager : MonoBehaviour
    {
        VisualElement m_RootVisualElement;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;
            m_RootVisualElement.style.display = DisplayStyle.None;
            EventManager.AddListener<ChatEvent>(OnChat);
            EventManager.AddListener<ChatOverEvent>(OnChatOver);
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
            m_RootVisualElement.style.display = DisplayStyle.Flex;
            Debug.Log("enter: " + evt.Name);
        }

        void OnChatOver(ChatOverEvent evt)
        {
            m_RootVisualElement.style.display = DisplayStyle.None;
            Debug.Log("exit");
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ChatEvent>(OnChat);
            EventManager.RemoveListener<ChatOverEvent>(OnChatOver);
        }
    }
}
