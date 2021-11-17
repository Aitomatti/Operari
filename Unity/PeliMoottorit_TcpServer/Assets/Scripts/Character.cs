/******************************************************************************
 * File        : Character.cs
 * Version     : 0
 * Author      : Toni Westerlund (toni.westerlund@lapinamk.com)
 * Copyright   : Lapland University of Applied Sciences
 * Licence     : MIT-Licence
 * 
 * Copyright (c) 2021 Lapland University of Applied Sciences
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 *****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    /// <summary>
    /// customController
    /// </summary>
    [SerializeField]private CustomController customController;

    protected float tiltAngle;
    protected Rigidbody rBody;
    protected Vector3 rEulerVel;

    Quaternion test;
     //   = new Quaternion(0, 0, 0, 0);


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rEulerVel = new Vector3(0, 100, 0);



    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    public void FixedUpdate()
    {
        tiltAngle = 1.0f;
        // FIXME: STUPID SIMPLE EXAMPLE, ONLY FOR DEMO PURPOSE
        //Debug.Log("Rotation= " + tiltAngle);


        //TESTATAAN GYRON ARVOJA!!!!
        test = new Quaternion(TcpServer.xRot, TcpServer.yRot, TcpServer.zRot, TcpServer.wRot);
        transform.rotation = test;
        //Debug.Log("tecp X= " + TcpServer.yRot);

        if (Input.GetKey("r"))
        {
            Debug.Log("RESET PIZZA POSITION");
            transform.rotation = new Quaternion(0, 0, 0, 0); ;
        }


        if (Input.GetKey("l") )
        {  
            Debug.Log("L pressed rotating right");

            //rBody.AddTorque(0, tiltAngle, 0); //Täytyy käyttä addForce ja addTorque jotta rigidBody reagoi keskenään
            rBody.angularVelocity = new Vector3(0, tiltAngle, 0);
        }
        else if (Input.GetKey("j") )
        {
            Debug.Log("J pressed rotating left");
            tiltAngle = -tiltAngle;

            rBody.angularVelocity = new Vector3(0, tiltAngle, 0); 
        }
        else
        {
            rBody.angularVelocity = Vector3.zero; //Pysäyttää pyörimisen
        }

        if (Input.GetKey("a"))
        {
            Debug.Log("a pressed move left");
            float move = 0;

            move++;
            
            
        }
    }
}
