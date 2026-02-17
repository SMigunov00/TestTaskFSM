using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("SpinningState")]
public class SpinningState : FSMState
{
    [Enter]
    public void Enter() => Settings.Model.Set("CanStop", false);
    
    [One(2.0f)]
    public void EnableStopButton() => Settings.Model.Set("CanStop", true);
    
    [Bind("OnCanStopClick")]
    private void OnStopClick() 
    {
        Settings.Invoke("StopReelSpin"); 
        Settings.Invoke("OnReelStoping");
        Parent.Change("StoppingState");
    }
}