using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Inspector")]
    public GameObject knifePrefab;
    public float power;
    public float restartTimeSec;
    [Header("Dynamic")]
    public Rigidbody2D rigid;
    public GameObject currentKnife;
    public static Controller S;
    public bool dying;

    public void Spawn()
    {
        currentKnife = Instantiate<GameObject>(knifePrefab);
        rigid = currentKnife.GetComponent<Rigidbody2D>();
        dying = false;

    }
    void Start()
    {
        S = this;
        Spawn();
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
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
