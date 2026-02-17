using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("IdleState")]
public class IdleState : FSMState 
{
    [Enter]
    public void Enter() 
    {
        Settings.Model.Set("CanStart", true);
        Settings.Model.Set("CanStop", false);
    }

    [Bind("OnCanStartClick")]
    private void OnStartClick() 
    {
        if (Parent != null) 
            Parent.Change("StartingState");
    }
}