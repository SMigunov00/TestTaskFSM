using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonDataBind : MonoBehaviourExtBind 
{
    [SerializeField] private string _fieldName;
    private Button _button;

    [OnAwake]
    public void Init()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
    
    [Bind("On{_fieldName}Changed")] 
    public void OnStatusChanged(bool value)
    {
        if (_button != null)
            _button.interactable = value;
        
        if(value)
            Settings.Invoke($"On{_fieldName}Active");
    }
    
    [OnStart]
    public void RefreshState()
    {
        if (Settings.Model != null)
            OnStatusChanged(Settings.Model.GetBool(_fieldName, false));
    }

    private void OnClick()
    {
        if (_button.interactable)
            Settings.Invoke($"On{_fieldName}Click");
    }
}