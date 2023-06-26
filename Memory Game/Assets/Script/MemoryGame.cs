using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{
    [SerializeField] GameObject cardBack;
    [SerializeField] Sprite image;
    [SerializeField] SceneController controller;
    
    private int _id;
    
    public int Id
    { 
        get => _id;
    }
    
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = image;
    }
    
    public void OnMouseDown()
    {
        if(cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    
    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
    
    public void Start()
    {
        
    }
    
}
