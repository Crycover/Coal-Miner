using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    #region Variables
    [Header("List")]
    public List<GameObject> tnt = new List<GameObject> { };                     // Collectable Object List

    [Header("Objects")]
    public Camera cam;                                                          // Main Camera
    public Transform parentPlayer;                                              // Player
    public GameObject basket;                                                   // Location object of TNTs
    public GameObject castle;                                                   // Finish Enemy

    [Header("Enemy Vector")]
    public GameObject castleVector;                                             // Enemy Vector for the shoot
    
    [Header("Ease Type")]
    public Ease easeType = Ease.OutElastic;
    public Ease scaleEase = Ease.OutBack;
    
    Sequence seq;
    Rigidbody rB;

    private Vector3 rotate = new Vector3(0, 450, 90);                           // Shoot Rotation
    private Vector3 turn = new Vector3(0, 360, 90);                             // Collect Rotation
    private int index;                                                          // Finish Shot Send Tnt
    private int indexTwo;                                                       // Finish Shot Send Tnt
    #endregion

    #region Start and Update
    private void Start()
    {
        DOTween.Init();
        rB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        for (int i = 1; i < tnt.Count; i++)                                     // Collide the obstacle remove the list.
        {
            if (tnt[i].CompareTag("Destroy"))
            {
                tnt.Remove(tnt[i]);
            }
        }

        if (tnt.Count < 2 && GameManager.instance.gameOver == true)             // When Finish the Game boolean in the GameManager script = true
        {
            GameManager.instance.dance = true;
        }
        
        if (GameManager.instance.dance == true)                                 // Down the basket object.
        {
            seq.Append(basket.transform.DOLocalMove(new Vector3(0, -1.35f, -1), 1f));
            seq.Append(basket.transform.DORotate(new Vector3(-6f, 0, 0), 1f));
        }
    }
    #endregion

    public void CollectTnt(GameObject obj, int Count)
    {
        seq = DOTween.Sequence();

        obj.transform.SetParent(parentPlayer);                                  // Set Tnt Parent
        Vector3 newPos = tnt[Count].transform.localPosition;                    // Set newPos

        if (tnt.Count < 2)       
            newPos += new Vector3(0, 2.15f, 0.05f);                             // First Position       
        else       
            newPos += new Vector3(0, 0.5f, 0);                                  // And than
        
        tnt.Add(obj);                                                           // Add to list Tnt
        obj.transform.DOLocalMove(newPos, 0.5f).SetEase(easeType);              // Send to Basket
        obj.transform.DORotate(turn, 0.8f);                                     // Rotate the TNT
        seq.Append(obj.transform.DOScale(1, 0.4f).SetEase(scaleEase));          // Bigger Baby
        seq.Append(obj.transform.DOScale(0.5f, 0.4f).SetEase(scaleEase));       // Little Baby
        cam.transform.DOShakePosition(0.5f, 0.35f);                             // Shaker
    }

    #region OnTriggerEnter Function
    private void OnTriggerEnter(Collider other)
    {   
        
        if (other.gameObject.CompareTag("Tnt"))                                // Collect Tnt
        {
            CollectTnt(other.gameObject, tnt.Count - 1);
            other.gameObject.tag = "Collected";
            other.gameObject.GetComponent<CapsuleCollider>().isTrigger = false;
        }

        if (other.gameObject.CompareTag("Obstacle"))                            // Obstacle
        {
            StartCoroutine(nameof(DownTheTnt));
        }

        if (other.gameObject.CompareTag("Jump"))
        {
            Debug.Log("Temass");
            rB.AddForce(Vector3.up * 500f);
        }

        if (other.gameObject.CompareTag("Finish"))                              // Finish Line
        {
            castle.GetComponent<FinishPart>().enabled = true;
            StartCoroutine(nameof(SendTnt));
            StartCoroutine(nameof(SendTntTwo));
            GameManager.instance.gameOver = true;       
        } 
    }
    #endregion

    IEnumerator DownTheTnt()
    {
        for (int i = tnt.Count - 1; i > 0; i--)
        {
            tnt[i].transform.parent = null;
            tnt[i].GetComponent<Rigidbody>().mass = 500;
            tnt[i].GetComponent<Rigidbody>().useGravity = true;
            tnt[i].GetComponent<Rigidbody>().isKinematic = false;
            tnt[i].tag = "Destroy";
            yield return new WaitForSeconds(0.01f);
        }
    }

    #region Finish Shot
    IEnumerator SendTnt()
    {
        index = tnt.Count / 2;
        for (int i = index; i < tnt.Count; i++)
        {
            tnt[i].transform.DOMove(castleVector.transform.position, 2f);
            tnt[i].transform.DORotate(rotate, 0.4f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    IEnumerator SendTntTwo()
    {
        indexTwo = tnt.Count / 2;
        for (int i = indexTwo - 1; i > 0; i--)
        {
            tnt[i].transform.DOMove(castleVector.transform.position, 2f);
            tnt[i].transform.DORotate(rotate, 0.4f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    #endregion
}
