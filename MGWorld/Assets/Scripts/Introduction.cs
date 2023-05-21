using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace MyGame
{
    public class Introduction : MonoBehaviour
    {
        VisualElement m_RootVisualElement;
        Button m_Button;
        // Start is called before the first frame update
        void Awake()
        {
            m_RootVisualElement = GetComponent<UIDocument>().rootVisualElement;

            m_Button = m_RootVisualElement.Query<Button>("Button");
            m_Button.RegisterCallback<ClickEvent>(OnClick);
        }

        void start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnClick(ClickEvent evt)
        {
            SceneManager.LoadScene("GameScene1", LoadSceneMode.Single);
        }
    }
}
