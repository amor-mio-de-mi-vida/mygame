using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class GroundController : MonoBehaviour
    {
        float gravity = 9.8f;
        private float vSpeed = 0f;

        CharacterController m_Controller;

        // Start is called before the first frame update
        void Start()
        {
            m_Controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (m_Controller.isGrounded)
            {
                vSpeed = 0;
            }
            vSpeed -= gravity * Time.deltaTime;
            m_Controller.Move(new Vector3(0f, vSpeed, 0f) * Time.deltaTime);
        }
    }
}
