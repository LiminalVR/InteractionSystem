using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideControllers : MonoBehaviour
{
    public GameObject PrimaryController;
    public GameObject SecondaryController;

    public bool ShowControllers;

    // Update is called once per frame
    void Update()
    {
        if (PrimaryController != null && PrimaryController.activeSelf != ShowControllers)
        {
            PrimaryController.SetActive(ShowControllers);
        }

        if (SecondaryController != null && SecondaryController.activeSelf != ShowControllers)
        {
            SecondaryController.SetActive(ShowControllers);
        }
    }

    public void SetShowControllers(bool state)
    {
        ShowControllers = state;
    }
}
