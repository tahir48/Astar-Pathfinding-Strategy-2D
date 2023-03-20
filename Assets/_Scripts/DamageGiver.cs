using StrategyGame_2DPlatformer.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StrategyGame_2DPlatformer
{
    public class DamageGiver : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit)
                {
                    Debug.Log("Damage geldi");
                    
                    IDamageable hitObj = hit.collider?.gameObject.GetComponent<IDamageable>();
                    hitObj?.Damage(2);
                }

            }
        }
    }
}
