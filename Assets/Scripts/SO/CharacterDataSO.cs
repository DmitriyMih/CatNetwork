using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Variant", menuName = "Character Data", order = 2)]
public class CharacterDataSO : ScriptableObject
{
    [Header("Data")]
    public Sprite characterSprite; 
    public string characterName; 
    public GameObject playerPrefab; 
    public Color color;

    [Header("Client Info")]
    public ulong clientId;
    public int playerId;
    public bool isSelected;

    private void OnEnable()
    {
        EmptyData();        
    }

    public void EmptyData()
    {
        isSelected = false;
        clientId = 0;
        playerId = -1;
    }
}