using System;
using UnityEngine;

public static class SelectionEvents
{
    public static event Action<ISelectable> OnSelected;

    public static void NotifySelected(ISelectable selectedTarget)
    {
        OnSelected?.Invoke(selectedTarget);
    }
}
