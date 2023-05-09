using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -1.0F, 0);
        Debug.Log("Nyan!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        // root.Add(new Label("Press any key to see the keyDown properties"));
        root.Add(new TextField());
        // root.Q<TextField>().Focus();
        root.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);
        root.RegisterCallback<KeyUpEvent>(OnKeyUp, TrickleDown.TrickleDown);
    }
    void OnKeyDown(KeyDownEvent ev)
    {
        Debug.Log("KeyDown:" + ev.keyCode);
        Debug.Log("KeyDown:" + ev.character);
        Debug.Log("KeyDown:" + ev.modifiers);
    }

    void OnKeyUp(KeyUpEvent ev)
    {
        Debug.Log("KeyUp:" + ev.keyCode);
        Debug.Log("KeyUp:" + ev.character);
        Debug.Log("KeyUp:" + ev.modifiers);
    }
}
