using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using UnityEngine;

public class SlotInitializer : MonoBehaviourExt
{
    [OnAwake]
    private void CreateFsm()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new StartingState());
        Settings.Fsm.Add(new SpinningState());
        Settings.Fsm.Add(new PostStopDelayState());
        Settings.Fsm.Add(new StoppingState());
    }
    
    [OnStart]
    public void StartFsm()
    {
        Settings.Fsm.Start("IdleState");
        Settings.Fsm.Update(0);
    }

    [OnUpdate]
    public void UpdateFsm()
    {
        if (Settings.Fsm != null) 
            Settings.Fsm.Update(Time.deltaTime);
    }
}