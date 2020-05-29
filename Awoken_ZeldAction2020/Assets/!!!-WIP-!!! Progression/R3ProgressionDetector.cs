using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R3ProgressionDetector : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionDetection") && collision.transform.root.CompareTag("Player"))
        {
            if (ProgressionManager.Instance.thisSessionTimeLine == ProgressionManager.ProgressionTimeLine.ShieldChargeUnlock)
            {
                ProgressionManager.Instance.thisSessionTimeLine = ProgressionManager.ProgressionTimeLine.ThirdRegionOut;
                ProgressionManager.Instance.SaveTheProgression();
            }
        }
    }
}
