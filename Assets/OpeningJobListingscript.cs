using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class OpeningJobListingscript : MonoBehaviour
{

    [SerializeField] private GameObject textObject;

    private void OnMouseDown() {

        if (textObject != null) {

            bool isActive = textObject.activeSelf;
            textObject.SetActive(!isActive);
        }
    }

}
