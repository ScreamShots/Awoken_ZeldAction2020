using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Made by Antoine
/// This script is temporary. Use it to count the number of fragment Player have 
/// </summary>

public class FragmentCounter : MonoBehaviour
{
    #region SerialiazeFiled var Statement

    [Header("Requiered Elements")]
    [SerializeField] private TextMeshProUGUI fragmentNumber = null;

    #endregion

    private void Update()
    {
        fragmentNumber.text = "" + PlayerManager.fragmentNumber;                //Show number of Fragments Player have
    }
}
