using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ease_Into_Scene : MonoBehaviour
{
    AudioSource audio_Source_;
    public AudioClip clip_;

    public Image image_;
    public float t_ = 1.0f;
    public float dt_;

    float last_ = 0;
    float now_ = 0; 

    public Color black_;
    bool start_ = false;

    // Start is called before the first frame update
    void Awake()
    {
        audio_Source_ = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        Time.timeScale = 0.0f;
        last_ = now_;
        now_ = Time.realtimeSinceStartup;
        //game_Object_.SetActive(false);
    }

    private void Start()
    {
        start_ = true;
    }

    void Update()
    {
        if (start_)
        {
            t_ -= dt_ / 2.0f;
            if (t_ < 0.0f)
            {
                audio_Source_.clip = clip_;
                audio_Source_.pitch = 1.00f;
                audio_Source_.volume = 0.228f;
                audio_Source_.PlayDelayed(0.2f);
                if(SceneManager.GetActiveScene().buildIndex == 2)
                    audio_Source_.loop = true;
                Time.timeScale = 1.0f;
                this.enabled = false;
            }
            last_ = now_;
            now_ = Time.realtimeSinceStartup;
            dt_ = now_ - last_;

            black_.a = (float)EaseInCubic(t_);
            image_.color = black_;

        }
    }

    public double EaseInCubic(double t)
    {
        return t * t * t;
    }
}
