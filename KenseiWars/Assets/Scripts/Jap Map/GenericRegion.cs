using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericRegion : MonoBehaviour
{
    public Sprite mInRegionSprite;
    public Sprite mOutRegionSprite;
    protected SpriteRenderer mSpriteRenderer;

    public abstract GenericRegion GetNextRegion(Vector2 _targetDirection);

    void Start()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetInRegion()
    {
        if(mInRegionSprite == null)
        {
            Debug.LogError("mInRegionSprite is null!!!");
        }
        else
        {
            mSpriteRenderer.sprite = mInRegionSprite;
        }
    }

    public void SetOutOFRegion()
    {
        if (mOutRegionSprite == null)
        {
            Debug.LogError("mOutRegionSprite is null!!!");
        }
        else
        {
            mSpriteRenderer.sprite = mOutRegionSprite;
        }
    }
    /*{
        Debug.LogError("GetNextRegion() called in parent!!!");
        return null;
    }*/
}
