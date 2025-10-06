using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUIPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    public void Hide()
    {
        _panel.SetActive(false);
    }
}
