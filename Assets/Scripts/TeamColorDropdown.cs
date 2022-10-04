using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Dropdown))]
public class TeamColorDropdown : MonoBehaviour
{
    [SerializeField] private int _player;
    [SerializeField] private UnityEvent _event;
    [SerializeField]private TMP_Dropdown _dropdown;
    private int _selectedOption;
    List<TMP_Dropdown.OptionData> dropDownOptions = new List<TMP_Dropdown.OptionData>();
    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(delegate { OnValueChanged(_dropdown); });
    }
    private void OnValueChanged(TMP_Dropdown change)
    {
        _selectedOption = change.value;
        _event?.Invoke();
    }
    public void SetTeamColor(TeamColor color)
    {
        for(int i = 0; i < _dropdown.options.Count; i++)
        {
            if ((TeamColor)Enum.Parse(typeof(TeamColor), _dropdown.options[i].text) == color)
            {
                _dropdown.value = i;
            }
        }
    }
    public void SetOptions(TeamColor[] options)
    {
        _dropdown.ClearOptions();
        for(int i = 0; i < options.Length;i++)
        {
            TMP_Dropdown.OptionData currentOption = new TMP_Dropdown.OptionData();
            currentOption.text = options[i].ToString();
            dropDownOptions.Add(currentOption);
            _dropdown.options.Add(dropDownOptions[i]);
        }
    }
    public TeamColor Color()
    {
        string text = _dropdown.options[_selectedOption].text;
        return (TeamColor)Enum.Parse(typeof(TeamColor), text);
    }
    public int Player()
    {
        return _player;
    }
}
