using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bob : MonoBehaviour
{
    private float _heightChange;
    private float _index;

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _scale;
    [SerializeField] private float _increment;

    private void Update()
    {
        _rectTransform.Translate(new Vector3(0, Mathf.Sin(_index) * Time.deltaTime * _scale, 0));
        _index += _increment;
    }
}
