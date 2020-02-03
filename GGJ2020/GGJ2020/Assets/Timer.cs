using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Image image_;
    float ease_;
    public Image timer;
    float seconds = 20;
    public GameObject[] trees_;

    int dead_Tree_;
    int alive_Tree_;

    bool once_ = false;

    float t_ = 0;
    Color color_;

    void Awake()
    {
        color_ = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (seconds <= 0)
        {
            alive_Tree_ = 0;
            foreach(GameObject tree in trees_)
            {
                if (tree.GetComponent<Tree_Controller>().alive_)
                    alive_Tree_++;
            }

            if (alive_Tree_ == trees_.Length)
            {
                ease_ = (float)EaseInCubic(t_);

                color_.a = ease_;
                image_.color = color_;

                t_ += Time.deltaTime / 1.5f;
                if (t_ > 1.0f)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                if (once_ == false)
                {
                    once_ = true;
                }

                ease_ = (float)EaseInCubic(t_);

                color_.a = ease_;
                image_.color = color_;

                t_ += Time.deltaTime / 3.0f;
                if (t_ > 1.0f)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
        else
        {
            seconds -= Time.deltaTime;
            timer.fillAmount = seconds / 20;
        }
    }

    public static void lose_Screen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public double EaseInCubic(double t)
    {
        return t * t * t;
    }
}
