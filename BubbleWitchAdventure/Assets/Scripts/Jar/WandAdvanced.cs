using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandAdvanced : Wand
{
    [SerializeField]
    private BubbleController m_BigBubblePrefab;
    [SerializeField]
    private Vector2 m_bigBubbleOffset;
    [SerializeField]
    protected BubbleController m_SmallBubblePrefab;
    [SerializeField]
    private int m_numberOfBubbles = 8;
    [SerializeField]
    protected float m_delay = 0.125f;
    [SerializeField]
    private float m_coolDown = 0.125f;


    override public void WandCast()
    {
        WandCast_Internal(AdvancedWandCastRoutine(), m_coolDown);
    }

    private IEnumerator AdvancedWandCastRoutine()
    {
        for (int i = 1; i <= m_numberOfBubbles; i++)
        {
            if (i % 3 == 0)
            {
                SummonBubble(m_BigBubblePrefab, m_bigBubbleOffset);
            }
            else
            {
                SummonBubble(m_SmallBubblePrefab, Vector2.zero);
            }

            yield return new WaitForSeconds(m_delay);
        }

        AccessoryFinished();
    }
}
