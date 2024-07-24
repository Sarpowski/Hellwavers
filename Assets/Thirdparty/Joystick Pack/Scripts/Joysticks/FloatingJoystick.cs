using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    [SerializeField] private Vector2 AnchorPosition = new Vector2(251 , 307) ;

   

    protected override void Start()
    {
        base.Start();
        AnchorPosition = background.anchoredPosition;    
       // background.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //SHIT happens at here , at start but no clue 
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.anchoredPosition = AnchorPosition;
        //background.gameObject.SetActive(true);
        base.OnPointerUp(eventData);
    }
}