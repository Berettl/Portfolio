using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WandContainer : MonoBehaviour
{
    [SerializeField]
    private Wand[] m_wands;

    private Vector2 m_direction = Vector2.right;

    private int m_currentWandID = 0;


    public int GetCurrentWandID()
    {
        return m_currentWandID;
    }

    public Wand GetCurrentWand()
    {
        int index = m_currentWandID - 1;

        if (index < 0)
        {
            return null;
        }

        return m_wands[index];
    }

    public void SetWandID(int id)
    {
        if (0 <= id && id <= m_wands.Length)
        {
            m_currentWandID = id;
        }
    }

    public void NextWand()
    {
        if (m_currentWandID < m_wands.Length)
        {
            m_currentWandID++;
        }
    }

    public void PreviousWand()
    {
        if (m_currentWandID > 0)
        {
            m_currentWandID--;
        }
    }

    public Vector2 GetDirection()
    {
        return m_direction;
    }

    public void SetDirection(Vector2 direction)
    {
        m_direction = direction;
    }

    public void WandCast()
    {
        if (GetCurrentWand() != null)
        {
            GetCurrentWand().WandCast();
        }
    }
}
