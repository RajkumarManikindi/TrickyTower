using UnityEngine;

namespace Controls
{
    public class KeyBoardController : InputController 
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                MoveObjectHorizontal(-0.5f);
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                MoveObjectHorizontal(0.5f);
            }
            
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                ActivateBoostFall();
            }
            
            if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                DeActivateBoostFall();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
               RotateObject();
            }
            

        }

       
    }
}