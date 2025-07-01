using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    public void OnInteract(InputValue value)
    {
        GameObject[] protesters = GameObject.FindGameObjectsWithTag("Protester");
        for(int i = 0; i < protesters.Length; i++)
        {
            ProtesterMovement pm = protesters[i].GetComponent<ProtesterMovement>();
            if (pm != null)
                pm.Call();
        }
    }
}
