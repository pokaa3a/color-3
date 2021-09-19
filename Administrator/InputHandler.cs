using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    // True if the touch just happend in this frame
    public static bool newTouch { get; private set; } = false;
    // Ture if the touch is happening
    public static bool touching { get; private set; } = false;
    public static Vector2 touchPos { get; private set; }

    private static bool prevTouch = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touching = true;

            Touch touch = Input.GetTouch(0);
            touchPos = touch.position;
        }
        else
        {
            touching = false;
        }
        newTouch = !prevTouch && touching;
        prevTouch = touching;

        if (TurnHandler.Instance.playersTurn)
        {
            if (InputHandler.newTouch)
            {
                Vector2 xy = Camera.main.ScreenToWorldPoint(InputHandler.touchPos);
                // CardManager.Instance.Act(xy);
                SkillManager.Instance.Act(xy);
            }
        }
    }
}
