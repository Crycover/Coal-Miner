using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    #region Variables
    [HideInInspector]
    public bool gameOver;

    [HideInInspector]
    public bool dance;

    [HideInInspector]
    public bool finishTnt;

    [Header("Control Type")]
    public bool Android;
    public bool PC;

    [Header("Test Mode")]
    public bool testMode;
    [Header("Finish Camera")]
    public GameObject finishCam;

    [Header("Main Camera")]
    public GameObject mainCamera;
    #endregion

    private void Start()
    {
        gameOver = false;
        dance = false;
        finishTnt = false;
        finishCam.SetActive(false);
    }

}
