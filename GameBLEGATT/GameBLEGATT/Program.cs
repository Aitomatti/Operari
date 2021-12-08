/******************************************************************************
 * File        : Program.cs
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


using System;
using System.Threading;

// This example code shows how you could implement the required main function for a 
// Console UWP Application. You can replace all the code inside Main with your own custom code.

// You should also change the Alias value in the AppExecutionAlias Extension in the 
// Package.appxmanifest to a value that you define. To edit this file manually, right-click
// it in Solution Explorer and select View Code, or open it with the XML Editor.

namespace GameBLEGATT
{
    class Program
    {
        static void Main(string[] args)
        {
            // BLE GATT DOCUMENTATION
            //https://docs.microsoft.com/en-us/windows/uwp/devices-sensors/gatt-client


            // Olen laittanut osan koodeista kommenteihin, poista koodit kommenteista sitä mukaan kun etenet
            // Koodit on kommentoitu, ei tule kääntäjä virheitä.

            // TODO : Important
            // You must declare the "bluetooth" capability in Package.appxmanifest.
            // < Capabilities > < DeviceCapability Name = "bluetooth" /> </ Capabilities >

            // TODO : Create instance of TcpGameClient class 
            TcpGameClient gameClient = new TcpGameClient();

            // TODO : Call Connect method from created instance
            /*bool exitLoop = false;
            while (!exitLoop)
            {
                Console.WriteLine("Yritetään muodostaa yhteyttä Unityyn");
                exitLoop = gameClient.Connect();

                if (!exitLoop)
                {
                    Console.WriteLine("Yhteyden muodostus epäonnitui. Yritetään uudelleen sekunnin päästä.");
                    Thread.Sleep(1000);
                }
            }*/
            gameClient.Connect();

            Console.WriteLine("Yhteys muodostettu Unityyn onnistunesti!\n\n\n\n\n");
            

            // TODO : Create instance of GameBLEGatt Class, pass TcpGameClient instance to constructor
            GameBLEGatt gameBLE = new GameBLEGatt(gameClient);

            // TODO : Call StartBLEScanner method from created instance
            gameBLE.StartBLEScanner();


            // FIXME: You can add loop where check possible user input, for example, if user want quit, or ...
            Console.ReadLine();

        }
    }

}
