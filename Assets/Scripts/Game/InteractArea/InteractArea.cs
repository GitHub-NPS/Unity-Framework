using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractArea : MonoBehaviour
{
    //[SerializeField] private bool isLog = false;

    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerEnter2D;
    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerStay2D;
    [SerializeField] private UnityEvent<Collider2D> m_OnTriggerExit2D;

    protected virtual bool condition(Collider2D collision)
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (condition(collision))
        {
            //Debug.Log("OnTriggerEnter2D: " + collision.name);
            m_OnTriggerEnter2D?.Invoke(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (condition(collision))
        {
            //Debug.Log("OnTriggerExit2D: " + collision.name);
            m_OnTriggerExit2D?.Invoke(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (condition(collision))
        {
            //Debug.Log("OnTriggerStay2D: " + collision.name);
            m_OnTriggerStay2D?.Invoke(collision);
        }
    }    
}
