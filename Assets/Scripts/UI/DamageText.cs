using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private float _dissapearTimerMax = 1f;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _dissapearSpeed = 3f;
    [SerializeField] private float _scaleIncrease = 0.7f;
    [SerializeField] private TextMeshProUGUI _text;
    private Color _startColor;
    private float _dissapearTimer = 1f;
    private Color _textColor;
    private void Awake()
    {
        _startColor = _text.color;
    }
    private void OnEnable()
    {        
        _text.color = _startColor;
        _textColor = _startColor;
        _dissapearTimer = _dissapearTimerMax;
        transform.localScale = Vector3.one;
    }
    public void SetText(int damage)
    {
        _text.SetText(damage.ToString());
    }

    private void Update()
    {
        transform.position += new Vector3(0, _moveSpeed) * Time.deltaTime;

        if(_dissapearTimer > _dissapearTimerMax * 0.5f)
        {
            transform.localScale += Vector3.one * _scaleIncrease * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * _scaleIncrease * Time.deltaTime;
        }

        _dissapearTimer -= Time.deltaTime;
        if(_dissapearTimer < 0)
        {
            _textColor.a -= _dissapearSpeed * Time.deltaTime;
            _text.color = _textColor;
            if(_textColor.a < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}