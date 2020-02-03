using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose_Code : MonoBehaviour
{
    float last_ = 0;
    float now_ = 0;
    public float t_ = 1.0f;
    public float dt_;

    // Start is called before the first frame update
    void Awake()
    {
        last_ = now_;
        now_ = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        t_ -= dt_ / 2.2f;
        if (t_ < 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(0);
        }

        last_ = now_;
        now_ = Time.realtimeSinceStartup;
        dt_ = now_ - last_;
    }
}
