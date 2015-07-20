using UnityEngine;
using System.Collections;

public class LoadSceneButton : MonoBehaviour
{
    public void LoadPrevious(Object scene)
    {
        if (scene != null)
        {
            Application.LoadLevel(scene.name);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel - 1);
        }
    }

    public void LoadNext(Object scene)
    {
        if (scene != null)
        {
            Application.LoadLevel(scene.name);
        }
        else
        {
            Application.LoadLevel(Application.loadedLevel + 1);
        }
    }
}
