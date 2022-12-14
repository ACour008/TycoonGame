using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickable
{
    public void OnPointerClicked();
    public GameObject GameObject { get; }
}
