namespace Controls
{
    public class AIController: InputController
    {
        private int _number;
        private const int RepeatRate = 3;
        private const int Time = 1;
        private void Start()
        {
            InvokeRepeating(nameof(AiCall), Time, RepeatRate);
        }

        private void OnDestroy()
        {
            CancelInvoke(nameof(AiCall));
        }

        private void Call1()
        {
            MoveObjectHorizontal();
        }

        private void Call2()
        {
            MoveObjectHorizontal(-1f);
        }

        private void Call3()
        {
            RotateObject();
        }

        private void Call4()
        {
            ActivateBoostFall();
        }


        private void AiCall()
        {
            _number = UnityEngine.Random.Range(0, 5);
            switch (_number)
            {
                case 0:
                    Call1();
                    break;
                case 1:
                    Call2();
                    break;
                case 2:
                    Call3();
                    break;
                case 3:
                    Call4();
                    break;
                default:
                    Call4();
                    break;
            }
        }
        
    }
    
    

}