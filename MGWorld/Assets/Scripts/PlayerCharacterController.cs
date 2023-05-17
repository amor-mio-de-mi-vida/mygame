using UnityEngine;
using UnityEngine.Events;

namespace MyGame
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInputHandler))]
    public class PlayerCharacterController : MonoBehaviour
    {

        [Header("Movement")] [Tooltip("Max movement speed when grounded (when not sprinting)")]
        public float MaxSpeedOnGround = 10f;

        CharacterController m_Controller;
        PlayerInputHandler m_InputHandler;
        

        void Start()
        {
            // fetch components on the same gameObject
            m_Controller = GetComponent<CharacterController>();
            m_InputHandler = GetComponent<PlayerInputHandler>();
            m_Controller.enableOverlapRecovery = true;
        }

        void Update()
        {
            m_Controller.Move(m_InputHandler.GetMoveInput() * MaxSpeedOnGround * Time.deltaTime);
        }

        void OnTriggerEnter(Collider collider) {
            if (collider.gameObject.tag == "NPC")
            {
                ChatEvent evt = Events.ChatEvent;
                evt.Name = collider.gameObject.name;
                EventManager.Broadcast(evt);
            }
        }

        void OnTriggerExit(Collider collider) {
            if (collider.gameObject.tag == "NPC")
            {
                ChatOverEvent evt = Events.ChatOverEvent;
                evt.Name = collider.gameObject.name;
                EventManager.Broadcast(evt);
            }
        }

    }
}