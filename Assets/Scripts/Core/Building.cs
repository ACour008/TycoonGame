using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IClickable
{
    public GameObject GameObject => this.gameObject;

    public void OnPointerClicked()
    {
        Debug.Log("Clicked Building!");
    }
}
