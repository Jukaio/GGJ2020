using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_Controller : MonoBehaviour
{
    public float max_Health_ = 200.0f;
    public float current_Health_ = 200.0f;

    public float max_Fire_Health_ = 200.0f;
    public float current_Fire_Health_ = 200.0f;

    SpriteRenderer renderer_;

    public Sprite tree_100_;
    public Sprite tree_75_;
    public Sprite tree_50_;
    public Sprite tree_25_;
    Animator anim_;

    public GameObject[] fires_;

    public bool burning_;
    public bool alive_;

    void Start()
    {
        renderer_ = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
        anim_ = (Animator)GetComponent(typeof(Animator));
        current_Health_ = max_Health_;
        current_Fire_Health_ = max_Fire_Health_;
        burning_ = true;
        alive_ = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (burning_ == true)
        {
            if (current_Health_ > 0)
                current_Health_ -= Time.deltaTime * 10.0f;
            else
                current_Health_ = 0;
        }

        if (current_Health_ > max_Health_ * 0.75f)
            renderer_.sprite = tree_100_;
        else if (current_Health_ > max_Health_ * 0.50f)
            renderer_.sprite = tree_75_;
        else if (current_Health_ > max_Health_ * 0.25f)
            renderer_.sprite = tree_50_;
        else if (current_Health_ > 0)
            renderer_.sprite = tree_25_;
        else
        {
            foreach (GameObject fire in fires_)
                fire.SetActive(false);
            GetComponent<Collider2D>().enabled = false;
            anim_.enabled = true;
            burning_ = false;
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        if (burning_)
        {
            current_Fire_Health_ -= 1.0f;

            if (current_Fire_Health_ < max_Fire_Health_ * 0.0f)
            {
                GetComponent<Collider2D>().enabled = false;
                fires_[fires_.Length - 4].SetActive(false);
                burning_ = false;
                alive_ = true;
            }
            if (current_Fire_Health_ < max_Fire_Health_ * 0.25f)
                fires_[fires_.Length - 3].SetActive(false);
            if (current_Fire_Health_ < max_Fire_Health_ * 0.50f)
                fires_[fires_.Length - 2].SetActive(false);
            if (current_Fire_Health_ < max_Fire_Health_ * 0.75f)
                fires_[fires_.Length - 1].SetActive(false);
        }
    }
}
