using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : MonoBehaviour
{
    

    private void OnBecameInvisible()
    {
        Debug.Log("Kapatýldý");
        gameObject.SetActive(false);
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }


}
