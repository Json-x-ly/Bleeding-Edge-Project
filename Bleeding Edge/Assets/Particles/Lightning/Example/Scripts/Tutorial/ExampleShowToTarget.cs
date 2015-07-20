using UnityEngine;
using System.Collections;
using Lightning;

public class ExampleShowToTarget : MonoBehaviour
{
    public LightningObject lightning;

    public void ShowToTarget(Transform target)
    {
        if (!lightning.gameObject.activeSelf)
        {
            lightning.Show(target);
        }
        else
        {
            lightning.gameObject.SetActive(false);
        }
    }
}
