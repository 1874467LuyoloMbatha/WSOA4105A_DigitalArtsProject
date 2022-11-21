using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace StudYolo.Blendshapes
{
    [RequireComponent(typeof(Slider))]
    public class UIBlendShapeController : MonoBehaviour
    {
        [Header("Generic Elements - NO SUFFIX")]
        [Tooltip("Just the name of the blend shape without the MIN?MAX")]
        public string blendShapeName;

        [Header("Unity Handles")]
        Slider blendShapeSlider;

        void Start()
        {
            //Removes spaces from string and assign slider to script
            blendShapeName = blendShapeName.Trim();
            blendShapeSlider = GetComponent<Slider>();

            CharacterCustomisation.Instance.GetPlayerPrefs(blendShapeName);
            blendShapeSlider.value = PlayerPrefs.GetFloat(blendShapeName);
			blendShapeSlider.onValueChanged.AddListener(value => CharacterCustomisation.Instance.ChangeBlendshapeValue(blendShapeName, value));
        }



    }
}

/*

Copyright (C) Nhlanhla 'Stud' Langa

*/