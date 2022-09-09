using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinMax<T>
{
    [SerializeField] T min;
    [SerializeField] T max;

    public T Min { get => min; set => min = value; }
    public T Max { get => max; set => max = value; }

    public MinMax(T min, T max)
    {
        this.min = min;
        this.max = max;
    }

    public override string ToString() => $"Min: {min}, Max: {max}";
}
