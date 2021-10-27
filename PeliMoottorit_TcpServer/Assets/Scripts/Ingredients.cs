using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    
    class Ingredients : MonoBehaviour
    {

        [SerializeField] private CustomController customController;

        private void FixedUpdate()
        {
            //Debug.Log("Character - Update() -> Button One Pressed " + customController.IsButtonOnePressed());

            if (customController.IsButtonOnePressed() == true || Input.GetKeyDown("y"))
            {
                Rigidbody fysiikka = this.GetComponent<Rigidbody>();
                //Rigidbody fysiikka = GameObject.Find("Sphere").GetComponent<Rigidbody>();
                fysiikka.AddForce(Vector3.left * 1000f);
            }


        }
    }
}
