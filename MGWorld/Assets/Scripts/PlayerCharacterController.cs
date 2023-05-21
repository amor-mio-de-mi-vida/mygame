using UnityEngine;
using UnityEngine.Events;

namespace MyGame
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public class PlayerCharacterController : MonoBehaviour
    {

        [Header("Movement")] [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 10f;
        public bool CanMove = true;

        CharacterController m_Controller;
        PlayerInputHandler m_InputHandler;

        bool m_Chattable = false;
        bool m_Chatting = false;
        string m_Name;
        

        void Start()
        {
            // fetch components on the same gameObject
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_Controller.enableOverlapRecovery = true;

            EventManager.AddListener<ChatBackEvent>(OnChatBack);
        }

        void Update()
        {
            if (CanMove)
            {
                m_Controller.Move(m_InputHandler.GetMoveInput() * MaxSpeedOnGround * Time.deltaTime);
            }

            if (m_Chatting)
            {
                if (m_InputHandler.GetChat())
                {
                    // ChatOver();
                }
            }
            else if (m_Chattable)
            {
                if (m_InputHandler.GetChat())
                {
                    Chat();
                }
            }
        }

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "NPC")
            {
                Approach(collider.gameObject.name);
                m_Name = collider.gameObject.name;
            }
        }

        void OnTriggerExit(Collider collider) {
            if (collider.gameObject.tag == "NPC")
            {
                ApproachOver(collider.gameObject.name);
                if (m_Chatting)
                {
                    ChatOver();
                }
            }
        }

        void OnChatBack(ChatBackEvent evt) {
            if (evt.Type == ChatType.Read)
            {
                Debug.Log("Read");
            }
            else if (evt.Type == ChatType.Options)
            {
                Debug.Log("Click option" + evt.Option + " " + evt.Chat);
            }
            else if (evt.Type == ChatType.Input)
            {
                Debug.Log("Input " + evt.Chat);
            }
            ChatOver();
        }

        private void Chat() {
            ChatEvent evt = new ChatEvent();
            evt.Name = m_Name;
            evt.SubName = "可爱的" + m_Name;
            evt.Chat = "可爱的" + m_Name + m_Name + m_Name;
            evt.Type = ChatType.Read;
            evt.Option1 = "猫";
            evt.Option2 = "狐狸";
            EventManager.Broadcast(evt);
            CanMove = false;
            m_Chatting = true;
        }

        private void ChatOver() {
            ChatOverEvent evt = Events.ChatOverEvent;
            evt.Name = m_Name;
            EventManager.Broadcast(evt);
            CanMove = true;
            m_Chatting = false;
        }

        private void Approach(string name) {
            ApproachEvent evt = Events.ApproachEvent;
            evt.Name = name;
            EventManager.Broadcast(evt);
            m_Chattable = true;
        }

        private void ApproachOver(string name) {
            ApproachOverEvent evt = Events.ApproachOverEvent;
            evt.Name = name;
            EventManager.Broadcast(evt);
            m_Chattable = false;
        }

        void OnDestroy() {
            EventManager.RemoveListener<ChatBackEvent>(OnChatBack);
        }
    }
}