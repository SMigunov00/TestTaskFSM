using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("StoppingState")]
public class StoppingState : FSMState 
{
    [Enter]
    public void Enter() 
    {
        Settings.Model.Set("CanStop", false);
        Settings.Model.Set("CanStart", false);
    }
    
    [Bind("OnReelStopped")]
    private void OnStopped() => Parent.Change("PostStopDelayState");
    
}