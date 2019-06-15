using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Player
{
    private const int LEFT_BUTTON = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MyTurn() && Input.GetMouseButtonUp(LEFT_BUTTON))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Field field = hit.transform.GetComponent<Field>();
                if(field != null)
                {
                    Controller.TryMakeMove(Color, field.I, field.J);
                }
            }
        }
    }
}
