using AxGrid.FSM;

[State("PostStopDelayState")]
public class PostStopDelayState : FSMState 
{
    [Enter]
    public void Enter()
    {
        
    }

    [One(2.0f)] 
    private void BackToIdle() => Parent.Change("IdleState");
    
}