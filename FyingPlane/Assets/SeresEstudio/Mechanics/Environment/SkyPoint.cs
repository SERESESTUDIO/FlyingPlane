using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeresEstudio.Mechanics.Player;
namespace SeresEstudio.Mechanics.Environment
{
    public class SkyPoint : MonoBehaviour
    {
        public float actionDistance;
        public int indexPoint;
        private SkyChange skyChange;
        private GameObject player;
        private void Awake()
        {
            skyChange = GameObject.FindObjectOfType<SkyChange>();
        }
        private void Update()
        {
            if(player == null && GameObject.FindObjectOfType<PlayerLocomotion>())
            {
                player = GameObject.FindObjectOfType<PlayerLocomotion>().gameObject;
            } else if(player != null)
            {
                float distance = (player.transform.position - transform.position).magnitude;
                if(distance <= actionDistance)
                {
                    skyChange.index = indexPoint;
                }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, actionDistance);
        }
    }
}
