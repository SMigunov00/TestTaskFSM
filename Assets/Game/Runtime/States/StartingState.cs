using AxGrid;
using AxGrid.FSM;

[State("StartingState")]
public class StartingState : FSMState
{
    [Enter]
    public void Enter()
    {
        Settings.Model.Set("CanStart", false);
        Settings.Model.Set("CanStop", false);
        Settings.Invoke("StartReelSpin");
    }

    [One(0.5f)]
    private void GoToSpinning() => Parent.Change("SpinningState");
}