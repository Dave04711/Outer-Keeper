using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    public InteractType type;
    public virtual void Interact()
    {
        Debug.Log($"Interacted with : {gameObject.name}");
    }

    public int CheckType()
    {
        switch (type)
        {
            case InteractType.Building: return 0;
            case InteractType.Dialogue: return 1;
            case InteractType.Crate: return 2;
            case InteractType.Cannon: return 3;
        }
        return -1;
    }
}
public enum InteractType { Building, Dialogue, Crate, Cannon }