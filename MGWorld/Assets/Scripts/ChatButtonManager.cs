using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

namespace MyGame
{
    public class ChatButtonManager : MonoBehaviour
    {
        GameObject m_NPCName;
        GameObject m_ChatButton;
        void Awake()
        {
            Transform[] father = GetComponentsInChildren<Transform>();
            foreach (var child in father)
            {
                if (child.tag == "ChatButton")
                {
                    m_ChatButton = child.gameObject;
                    m_ChatButton.SetActive(false);
                }
                else if (child.tag == "NPCName")
                {
                    m_NPCName = child.gameObject;
                    TMP_Text tMP_Text = m_NPCName.GetComponent<TMP_Text>();
                    tMP_Text.text = gameObject.name;
                }
                
            }
            EventManager.AddListener<ApproachEvent>(OnApproach);
            EventManager.AddListener<ApproachOverEvent>(OnApproachOver);
        }

        void start()
        {
            
        }

        void Update()
        {

        }

        void OnApproach(ApproachEvent evt)
        {
            if (evt.Name == gameObject.name)
            {
                m_ChatButton.SetActive(true);
            }
        }

        void OnApproachOver(ApproachOverEvent evt)
        {
            if (evt.Name == gameObject.name)
            {
                m_ChatButton.SetActive(false);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ApproachEvent>(OnApproach);
            EventManager.RemoveListener<ApproachOverEvent>(OnApproachOver);
        }
    }
}
