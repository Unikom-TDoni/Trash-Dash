using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Group8.TrashDash.Setting
{
    public sealed class SettingHandler : MonoBehaviour
    {
        private Slider _sliderSfx = default;

        private Slider _sliderBgm = default;

        private TMP_Dropdown _dropdownResolution = default;

        private void Awake()
        {
            _sliderSfx.onValueChanged.AddListener(value =>
            {

            });

            _sliderBgm.onValueChanged.AddListener(value =>
            {

            });

            _dropdownResolution.onValueChanged.AddListener(value =>
            {
                Screen.SetResolution(1920, 1980, true);
            });
        }
    }
}