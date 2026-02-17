using System.Collections.Generic;
using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using UnityEngine;

public class ReelViewController : MonoBehaviourExtBind
{
    [SerializeField] private List<RectTransform> _items;
    
    [Header("Settings")]
    [SerializeField] private float _itemHeight = 200f;
    [SerializeField] private float _scrollSpeedItems = 20f; 
    [SerializeField] private int _amountItemsToSpinOnStop = 15;

    private float _scrollPosition = 0f;
    private float _totalHeight;
    private float _startY;
    private float _currentSpeed = 0f;

    [OnStart]
    public void Init()
    {
        _totalHeight = _items.Count * _itemHeight;
        _startY = (_items.Count / 2) * _itemHeight; 
        UpdateItemsPosition();
    }

    private void UpdateItemsPosition()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            float basePos = _startY - (i * _itemHeight);
            float currentY = basePos - _scrollPosition;
            float halfHeight = _totalHeight / 2f;
            currentY = Mathf.Repeat(currentY + halfHeight, _totalHeight) - halfHeight;
            _items[i].anchoredPosition = new Vector2(0, currentY);
        }
    }

    [Bind("StartReelSpin")]
    public void StartSpin()
    {
        float targeSpeed = _scrollSpeedItems * _itemHeight;

        Path = new CPath()
            .EasingQuadEaseIn(1.0f, 0, targeSpeed, (value) =>
            {
                _currentSpeed = value;
                _scrollPosition += _currentSpeed * Time.deltaTime; 
                UpdateItemsPosition();
            })
            .Add(() => 
            {
                _currentSpeed = targeSpeed; 
                _scrollPosition += _currentSpeed * Time.deltaTime;
                UpdateItemsPosition();
                return Status.Continue;
            });
    }
    
   [Bind("StopReelSpin")]
   public void StopReelSpin()
   {
       if (Path != null) this.Path.StopPath();
        
       float currentPosition = _scrollPosition;
       
       float itemOffset = currentPosition % _itemHeight;
       float distanceToNextItem = _itemHeight - itemOffset;
       if (distanceToNextItem < 0) distanceToNextItem += _itemHeight;
       
       float extraDistance = _itemHeight * _amountItemsToSpinOnStop; 
        
       float totalDistance = distanceToNextItem + extraDistance;
       float targetPos = currentPosition + totalDistance;
       
       float duration = _currentSpeed > 0.1f ? (3f * totalDistance / _currentSpeed) : 1.0f;
       
       Path = new CPath()
           .EasingCubicEaseOut(duration, currentPosition, targetPos, (val) => 
           {
               _scrollPosition = val;
               UpdateItemsPosition();
           })
           .Action(() => 
           {
               _currentSpeed = 0;
               Settings.Invoke("OnReelStopped");
           });
   }
}