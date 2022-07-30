using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FinishPart : MonoBehaviour
{
    #region Variables
    public ParticleSystem explosion;                                                                // Castle Shoot Explosion
    public Camera cam;                                                                              // Main Camera for Shake
    public Text scoreText;                                                                          // Score Text for Finish
    
    private int count = 0;                                                                          // Shoot Point
    private int score = 0;                                                                          // Score = Score * Count.

    Sequence seq;
    #endregion

    #region Start and Update
    private void Start()
    {
        Invoke(nameof(FinishCam), 2.5f);
        Invoke(nameof(Finish), 3f);
        Invoke(nameof(Score), 9f);
    }

    private void Update()
    {
        score = count * 10;   

    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collected"))
        {
            other.gameObject.SetActive(false);                                                      // When Shot the Castle Deactivate Tnt
            other.gameObject.tag = "Destroy";                                                       // Change Tag For The Destroy.
            explosion.Play();                                                                       // Particle Effect
            cam.transform.DOShakePosition(0.5f, 1);                                                 // Shake
            seq = DOTween.Sequence();
            seq.Append(gameObject.transform.DOScale(6, 0.4f));
            seq.Append(gameObject.transform.DOScale(4, 0.4f));
            count += 5;
        }
    }

    #region Finish Functions
    public void Finish()
    {
        transform.DORotate(new Vector3(0, -180, 0), 1).SetLoops(-1 , LoopType.Incremental);         // Rotate Infinity
        transform.DOMoveY(count, 5f);                                                               // Up for the Score
    }

    public void FinishCam()
    {
        if (GameManager.instance.dance == true)
        {
            GameManager.instance.finishCam.SetActive(true);
            GameManager.instance.mainCamera.SetActive(false);
        }
    }

    public void Score()
    {
        scoreText.enabled = true;
        scoreText.text = "Your Score is " + score.ToString();
    }
    #endregion
}
