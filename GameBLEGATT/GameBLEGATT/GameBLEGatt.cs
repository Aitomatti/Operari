/******************************************************************************
 * File        : GameBLEGatt.cs
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
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

namespace GameBLEGATT
{
    class GameBLEGatt
    {

        /// <summary>
        /// deviceWatcher
        /// </summary>
        private DeviceWatcher deviceWatcher;


        /// <summary>
        /// tcpGameClient
        /// </summary>
        private TcpGameClient tcpGameClient;

        /// <summary>
        /// GameBLEGatt
        /// </summary>
        public GameBLEGatt(TcpGameClient tcpGameClient)
        {
            this.tcpGameClient = tcpGameClient;
        }

        public GameBLEGatt()
        {
        }

        /// <summary>
        /// StartBLEScanner
        /// </summary>
        public void StartBLEScanner()
        {
    
            Console.WriteLine("GameBLEGatt - StartBLEScanner()");

            //deviceWatcher = DeviceInformation.CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(false));
            deviceWatcher = DeviceInformation.CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(false));
            // TODO : Register Handlers, and Start watcher Check Documention!!!!
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            // Register event handlers before starting the watcher.
            // Added, Updated and Removed are required to get all nearby devices


            // EnumerationCompleted and Stopped are optional to implement.
            

            // Start the watcher.
            deviceWatcher.Start();
        }

        private void DeviceWatcher_Added1(DeviceWatcher sender, DeviceInformation args)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// DeviceWatcher_Stopped
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Stopped(DeviceWatcher a, object b)
        {
        }

        /// <summary>
        /// DeviceWatcher_Removed
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Removed(DeviceWatcher a, DeviceInformationUpdate b)
        {
        }

        /// <summary>
        /// DeviceWatcher_Updated
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Updated(DeviceWatcher a, DeviceInformationUpdate b)
        {
        }

        /// <summary>
        /// DeviceWatcher_AddedAsync
        /// </summary>
        /// <param name="deviceWatcher"></param>
        /// <param name="deviceInformation"></param>
        public void DeviceWatcher_Added(DeviceWatcher deviceWatcher, DeviceInformation deviceInformation)
        {
            
            // TODO : Check that bluetooth name is same what you setup on esp32 side
            if (deviceInformation.Name == "TiittoESP32")
            {
                Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() - > Device Found");

                // TODO : Stop Device Watcher
                deviceWatcher.Stop();

                // TODO : Connecting to the device

                BluetoothLEDevice bluetoothLEDevice = Task.Run(async () => await BluetoothLEDevice.FromIdAsync(deviceInformation.Id)).Result;
                // HOX!! Use Task.Run for all async methods
                // // Note: BluetoothLEDevice.FromIdAsync must be called from a UI thread because it may prompt for consent.


                // TODO : Enumerating supported services and characteristics

                // Get Gatt Services
                GattDeviceServicesResult result = Task.Run(async () => await bluetoothLEDevice.GetGattServicesAsync()).Result;
                //BluetoothLEDevice result = Task.Run(async () => await bluetoothLEDevice.GetGattServicesAsync()).Result;
                // Now that you have a BluetoothLEDevice object, the next step is to discover what data the device exposes. The first step to do this is to query for services:


                if (result.Status == GattCommunicationStatus.Success)
                {
                    var services = result.Services[2];

                    // TODO: Get Chharastics

                    // Once the service of interest has been identified, the next step is to query for characteristics.
                    GattCharacteristicsResult resultGATT = Task.Run(async () => await services.GetCharacteristicsAsync()).Result;

                    if (resultGATT.Status == GattCommunicationStatus.Success)
                    {

                        var characteristics = resultGATT.Characteristics;
                        GattCharacteristicProperties properties = characteristics[0].CharacteristicProperties;

                        if (properties.HasFlag(GattCharacteristicProperties.Read))
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Read OK");
                        }
                        else
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Read FAIL");
                        }
                        if (properties.HasFlag(GattCharacteristicProperties.Write))
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Write OK");
                        }
                        else
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Write FAIL");
                        }
                        if (properties.HasFlag(GattCharacteristicProperties.Notify))
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Notify OK");
                        }
                        else
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Notify FAIL");
                        }

                        // TODO : Subscribing for notifications

                        

                        GattCommunicationStatus status = Task.Run(async () => await characteristics[0].WriteClientCharacteristicConfigurationDescriptorAsync(
                        GattClientCharacteristicConfigurationDescriptorValue.Notify)).Result;


                        if (status == GattCommunicationStatus.Success)
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Notify Register OK ");
                            characteristics[0].ValueChanged += Characteristic_ValueChanged;
                        }
                        else
                        {
                            Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> Notify Register FAIL ");
                        }

                    }
                    else
                    {
                        Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() -> GattDeviceServicesResult FAILED");
                        StartBLEScanner();
                        // You can add error management here, if fail, propably you need retry, for ex. startScan again
                    }
                }
            }
        }

        /// <summary>
        /// Characteristic_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void Characteristic_ValueChanged(GattCharacteristic sender,
                            GattValueChangedEventArgs args)
        {
            var reader = DataReader.FromBuffer(args.CharacteristicValue);
            byte[] input = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(input);

            tcpGameClient.SendMessage(input);

            //Console.WriteLine(input);
            //Console.WriteLine("_______");


            // TODO : Perform Read/Write operations on a characteristic
            // Read Values and call tcmpGameClient.SendMessage and pass readed byte array

        }
    }
}
