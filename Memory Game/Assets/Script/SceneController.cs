using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] MemoryGame originalCard;
    [SerializeField] Sprite[] images;
    [SerializeField] TMP_Text scoreLabel;
    
    private MemoryGame firstRevealed;
    private MemoryGame secondRevealed;
    
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2.0f;
    public const float offsetY = 2.5f;
    
    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = originalCard.transform.position;
        
        int[] numbers = {0, 0, 1, 1, 2, 2, 3, 3};
        numbers = ShuffleArray(numbers);
        
        for(int j=0; j<gridCols; j++)
        {
            for(int i=0; i<gridRows; i++)
            {
                MemoryGame card;
                if(i==0 && j==0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate<MemoryGame>(originalCard);
                }
                
                int index = i * gridCols + j;
                
                int id = numbers[index];
                card.SetCard(id, images[id]);
                
                float posX = startPos.x + offsetX * j;
                float posY = startPos.y - offsetY * i;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i=0; i<newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    
    public bool canReveal
    {
        get
        {
            if(secondRevealed == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    public void CardRevealed(MemoryGame card)
    {
        if(firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }
    
    private IEnumerator CheckMatch()
    {
        if(firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            scoreLabel.text = $"Score: {score}";
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        
        firstRevealed = null;
        secondRevealed = null;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
