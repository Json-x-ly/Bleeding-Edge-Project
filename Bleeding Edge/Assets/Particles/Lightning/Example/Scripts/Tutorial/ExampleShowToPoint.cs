using UnityEngine;
using System.Collections;
using Lightning;

public class ExampleShowToPoint : MonoBehaviour
{
    public void ShowToPoint(LightningObject lightning)
    {
        if (!lightning.gameObject.activeSelf)
        {
            lightning.Show(Vector3.zero);
        }
        else
        {
            lightning.gameObject.SetActive(false);
        }
    }
}
