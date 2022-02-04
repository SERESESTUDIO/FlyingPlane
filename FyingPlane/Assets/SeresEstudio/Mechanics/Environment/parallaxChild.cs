using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeresEstudio.Mechanics.Environment
{
    public class parallaxChild : MonoBehaviour
    {
        public float speed;

        private float saveDir;
        private GameObject tarjet;
        private float initPos;
        private void Awake()
        {
            if (tarjet == null && Camera.main)
            {
                tarjet = Camera.main.gameObject;
                initPos = tarjet.transform.position.y - transform.position.y;
            }
        }

        void FixedUpdate()
        {
            if (tarjet == null && Camera.main)
            {
                tarjet = Camera.main.gameObject;
            }
            else
            {
                if(saveDir != transform.position.x)
                {
                    if(saveDir > transform.position.x + 0.1f)
                    {
                        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
                    } else if(saveDir < transform.position.x - 0.1f)
                    {
                        transform.Translate(-Vector3.right * speed * Time.fixedDeltaTime);
                    }
                    saveDir = transform.position.x;
                    transform.position = new Vector3(transform.position.x, tarjet.transform.position.y - initPos, transform.position.z);
                }
            }
        }
    }
}
