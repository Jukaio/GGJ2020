using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Vicotry_Screen : MonoBehaviour
{
    public Image image_;

    public bool pressed_ = false;
    public float rotation_ = 0;
    // Start is called before the first frame update
    public float factor_ = 2.0f;
    float t_ = 0;

    Color color_;
    void Awake()
    {
        color_ = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    float ease_;
    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && pressed_ == false)
        {
            pressed_ = true;
        }

        if(pressed_ == true)
        {
            Quaternion test = Quaternion.Euler(0.0f, 0.0f, rotation_);
            gameObject.transform.rotation = test;

            ease_ = (float)EaseInCubic(t_);

            rotation_ += factor_ * ease_ * 3.0f;
            color_.a = ease_;
            image_.color = color_;

            t_ += Time.deltaTime / 1.5f;
            if (t_ > 1.0f)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public double EaseInCubic(double t)
    {
        return t * t * t;
    }

}
