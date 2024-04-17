using System;
using Godot;

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZero;
    public event Action OnUpdate;
    [Export] public Stat StatType { get; set; }
    private float _statValues;
    [Export] public float StatValue 
    { 
        get => _statValues; 
        set
        {
            _statValues = Mathf.Clamp(value,0,Mathf.Inf);

            OnUpdate?.Invoke();

            if(_statValues == 0)
            {
                OnZero?.Invoke();
            }
        }  
    }
}