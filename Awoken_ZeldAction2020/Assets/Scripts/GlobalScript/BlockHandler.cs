using UnityEngine;

public class BlockHandler: MonoBehaviour
{
    public bool isBlocked;

    private void Update()
    {
        if(isBlocked == true)
        {
            Debug.Log("blocked");
        }
    }
}
