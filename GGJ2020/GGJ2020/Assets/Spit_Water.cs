using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit_Water : MonoBehaviour
{
    KeyCode shoot_;
    public ParticleSystem ps_;

    // Start is called before the first frame update
    void Start()
    {
        ps_ = GetComponent<ParticleSystem>();
        shoot_ = KeyCode.Space;
    }

    int particles_;
    void Update()
    {
        var main_Module = ps_.main;

        if (Input.GetKey(shoot_))
            main_Module.maxParticles = 2000;
        else if(main_Module.maxParticles != 0)
        {
            particles_ = main_Module.maxParticles;
            particles_ -= Mathf.RoundToInt(Time.deltaTime * 10);
            if (particles_ > 0)
                main_Module.maxParticles -= particles_;
            else
                main_Module.maxParticles = 0;
        }

        if(Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            var shape_Module = ps_.shape;
            var noise_Module = ps_.noise;
            if (Input.GetKey(KeyCode.D))
            {
                shape_Module.rotation = new Vector3(0.0f, 0.0f, 323.83f);
                noise_Module.positionAmount = 1.5f;
            }
            else
            {
                shape_Module.rotation = new Vector3(1.0f, 1.0f, 130.47f);
                noise_Module.positionAmount = -1.5f;
            }
        }
    }

    private void OnParticleTrigger()
    {
        print("Trigger");
    }

}
