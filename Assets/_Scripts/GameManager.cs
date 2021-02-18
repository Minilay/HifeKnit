using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Inspector")]
    public float power;
    public GameObject knifePrefab;
    public bool isLastLevel;
    public float restartTimeSec;
    public int knifeTotal;
    public Color disabledColor;
    public GameObject knifeUI;
    public Transform knifesParent;

    [Header("Dynamic")]
    public Rigidbody2D rigid;
    private GameObject currentKnife;
    public static GameManager S;
    public bool dying;
    public int knifeLeft;
    public Camera main;
    public List<SpriteRenderer> knifeSprites;
    public void Spawn()
    {
        if(!dying)
        {
            currentKnife = Instantiate<GameObject>(knifePrefab);
            rigid = currentKnife.GetComponent<Rigidbody2D>();
        }
    }
    void Start()
    {
        S = this;
        dying = false;
        Spawn();

        main = Camera.main;
        knifeLeft = knifeTotal;
        float height = main.orthographicSize;
        float width = height * main.aspect;


        for (int i = 0; i < knifeTotal; i++)
        {
            GameObject go = Instantiate<GameObject>(knifeUI);
            go.transform.position = new Vector3(-width * 0.5f, -height + 1 + i * 0.3f);
            knifeSprites.Add(go.GetComponentInChildren<SpriteRenderer>());
            go.transform.SetParent(knifesParent);
        }
    }

    public void knifeDecrease()
    {
        knifeLeft--;
        knifeSprites[knifeLeft].color = disabledColor;
        if (knifeLeft == 0)
        {
            print("you win!");
            win();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if((Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)) && currentKnife != null)
        {
            rigid.velocity = Vector3.up * power;
        }
    }

    //RestartGame
    public IEnumerator reload()
    {
        currentKnife = null;
        dying = true;
        yield return new WaitForSeconds(restartTimeSec);
        SceneManager.LoadScene("Scene0");
    }
    void win()
    {
        if(isLastLevel)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
        }
    }
}
