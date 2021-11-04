/******************************************************************************
 * File        : TcpGameClient.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameBLEGATT
{
    class TcpGameClient
    {

        /// <summary>
        /// socketConnection
        /// </summary>
        private TcpClient socketConnection;

        public TcpGameClient()
        {

        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {

            // Unityyn ei saaha yhdistettä error handling
            
            try
            {
                socketConnection = new TcpClient("localhost", 8052);
            }
            catch (Exception  e)
            {
                Console.WriteLine("Unityyn ei saatu yhteyttä\n");
                Console.WriteLine(e);
            }

                if (socketConnection == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            

        }

        /// <summary>
        /// SendMessage
        /// </summary>
        public void SendMessage(byte[] command)
        {
            // TODO: Check if socketConnection is null then return, do not try to send message to unity server
            if (socketConnection == null)
            {
                return;
            }
            else
            {
                try
                {
                    // Get a stream object for writing. 			
                    NetworkStream stream = socketConnection.GetStream();
                    if (stream.CanWrite)
                    {
                        // Write byte array to socketConnection stream.                 
                        stream.Write(command, 0, command.Length);
                        int value = BitConverter.ToInt32(command, 0);

                        
                        //stream.Write(command, 0, command.Length);
                        //int valueY = BitConverter.ToInt32(command, 1);

                        Console.WriteLine("TcpGameClient - SendMessage() --> Send" + value );
                    }
                }
                catch (SocketException socketException)
                {
                    Console.WriteLine("TcpGameClient - SendMessage() --> Error " + socketException);
                }
            }
        }
    }
}
