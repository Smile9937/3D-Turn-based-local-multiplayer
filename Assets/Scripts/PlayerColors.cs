using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Player Color List", fileName = "New Player Color List")]
public class PlayerColors : ScriptableObject
{
    [Serializable]
    public class PlayerColor
    {
        public TeamColor _teamColor;
        public Material _material;
    }
    [SerializeField] private PlayerColor[] _colors;

    public Material GetColor(TeamColor teamColor)
    {
        foreach(PlayerColor color in _colors)
        {
            if(teamColor == color._teamColor)
            {
                return color._material;
            }
        }
        return null;
    }

    public Material GetColor(string teamColor)
    {
        foreach (PlayerColor color in _colors)
        {
            if ((TeamColor)Enum.Parse(typeof(TeamColor), teamColor) == color._teamColor)
            {
                return color._material;
            }
        }
        return null;
    }

    public TeamColor[] GetColorArray()
    {
        List<TeamColor> colors = new List<TeamColor>();
        foreach(PlayerColor color in _colors)
        {
            colors.Add(color._teamColor);
        }
        return colors.ToArray();
    }
    public Material[] GetMaterialArray()
    {
        List<Material> colors = new List<Material>();
        foreach(PlayerColor color in _colors)
        {
            colors.Add(color._material);
        }
        return colors.ToArray();
    }
}