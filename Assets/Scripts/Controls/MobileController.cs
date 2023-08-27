
using PieceScripts;
using UnityEngine;

namespace Controls
{
    public class MobileController : InputController
    {
        
        private Touch _theTouch;
        private Vector2 _touchStartPosition, _touchEndPosition;
        private const float HorizontalSwipeValue = 60;
        private const float VerticalSwipeValue = 150;
    
    private void LateUpdate()
    {
        if (Input.touchCount <= 0) return;
        _theTouch = Input.GetTouch(0);
        if (_theTouch.phase == TouchPhase.Began)
        {
            _touchStartPosition = _theTouch.position;
        }

        else if (_theTouch.phase == TouchPhase.Moved || _theTouch.phase == TouchPhase.Ended)
        {
            _touchEndPosition = _theTouch.position; ;
            var x = _touchEndPosition.x - _touchStartPosition.x;
            var y = _touchEndPosition.y - _touchStartPosition.y;
                
             
            if (Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0)
            {
                RotateObject();
            }

            else if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x > HorizontalSwipeValue)
                {
                    //Right
                    MoveObjectHorizontal(0.5f);
                    _touchStartPosition = _touchEndPosition;
                }
                if(x < -HorizontalSwipeValue)
                {
                    //Left
                    MoveObjectHorizontal(-0.5f);
                    _touchStartPosition = _touchEndPosition;
                }
            }
            else
            {
                if (y > VerticalSwipeValue)
                {
                    //Up
                        
                }
                else  if (y < -VerticalSwipeValue)
                {
                    //Down
                    ActivateBoostFall();
                    _touchStartPosition = _touchEndPosition;
                }
            }
        }

        if (_theTouch.phase == TouchPhase.Ended)
        {
            DeActivateBoostFall();  
        }
    }
    }
}
