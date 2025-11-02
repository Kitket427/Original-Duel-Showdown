using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneRestart : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void GameOver()
    {
        Invoke(nameof(Restart), Random.Range(2f, 7f));
    }
    void Restart()
    {
        anim.Play("End");
        Invoke(nameof(Load), 2);
    }
    private void Load()
    {
        SceneManager.LoadScene(0);
    }
}
