using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{

    // config param
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitsSprites;

    //cached reference
    levelScript level;

    //state variables
    [SerializeField] int timesHit; // Serialize because debug

    private void Start()
    {
        CountBrekableBlocks();
    }

    private void CountBrekableBlocks()
    {
        level = FindObjectOfType<levelScript>();
        if (tag == "Brekable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Brekable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitsSprites.Length+1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitsSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitsSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
            PlayBlockDestroySFX();
            Destroy(gameObject);
            level.BlockDestroyed();
            TriggerSparlesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore(hitsSprites.Length);
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparlesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }


}
