using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{

    //
    //
    // protected override void Start()
    // {
    //     base.Start();
    //     background.gameObject.SetActive(true);
    // }
    //
    // public override void OnPointerDown(PointerEventData eventData)
    // {
    //     //SHIT happens at here , at start but no clue 
    //     background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
    //     background.gameObject.SetActive(true);
    //     base.OnPointerDown(eventData);
    // }
    //
    // public override void OnPointerUp(PointerEventData eventData)
    // {
    //     background.gameObject.SetActive(true);
    //     base.OnPointerUp(eventData);
    // }
    [SerializeField] private Vector2 AnchorPosition;
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
        AnchorPosition = background.anchoredPosition;
        background.gameObject.SetActive(true);
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
        background.gameObject.SetActive(false);
        background.anchoredPosition = AnchorPosition;
        background.gameObject.SetActive(true);
        base.OnPointerUp(eventData);
    }
    
    
    
      
    

}
