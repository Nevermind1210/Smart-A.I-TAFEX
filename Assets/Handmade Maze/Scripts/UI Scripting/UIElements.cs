using System;
using UnityEngine;
using TMPro;
using Objectives;
namespace UIElements
{
    public class UIElements : MonoBehaviour
    {
        public GameObject collectibleCount;
        private void Update()
        {
            collectibleCount.GetComponent<TextMeshProUGUI>().text = GameVariables.keyItems + "/4 keys collected";
        }
    }
}