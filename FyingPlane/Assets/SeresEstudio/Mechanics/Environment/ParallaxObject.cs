using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SeresEstudio.Mechanics.Environment
{
    public class ParallaxObject : MonoBehaviour
    {
        public bool FollowY;
        private GameObject tarjet;
        private void Awake()
        {
            if(tarjet == null && Camera.main)
            {
                tarjet = Camera.main.gameObject;
            } else
            {
                transform.position = new Vector3(tarjet.transform.position.x, transform.position.y, transform.position.z);
            }
        }
        private void Update()
        {
            if (tarjet == null && Camera.main)
            {
                tarjet = Camera.main.gameObject;
            }
            else
            {
                if (FollowY)
                {
                    transform.position = new Vector3(tarjet.transform.position.x, tarjet.transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(tarjet.transform.position.x, transform.position.y, transform.position.z);
                }
            }
        }
    }
}
