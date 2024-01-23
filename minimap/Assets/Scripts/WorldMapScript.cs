using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapScript : MonoBehaviour
{
    [SerializeField] RectTransform Arrow;
    public void TranslateMap(Vector3 PlayerPos)
    {
        Debug.Log(PlayerPos);
        Arrow.anchoredPosition = new Vector2(PlayerPos.x * 5, PlayerPos.z * 5);
        Debug.Log(Arrow.position);
    }
}
