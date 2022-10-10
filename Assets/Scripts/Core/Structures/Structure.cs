using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Structure
{
    protected string name;

    public string Name { get => name; }

    public override string ToString() => name;
}
