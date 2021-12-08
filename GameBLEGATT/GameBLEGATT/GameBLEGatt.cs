using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;

public enum BleState
{
    ENone, EDiscovery, EConnecting, EConnected
}

namespace GameBLEGATT
{
    class GameBLEGatt
    {
        private GattCharacteristic selectedGattCharacteristic;
        private BluetoothLEDevice bluetoothLeDevice;
        private BleState bleState = BleState.ENone;
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

            bleState = BleState.EDiscovery;
            Console.WriteLine("GameBLEGatt - StartBLEScanner()");

            //deviceWatcher = DeviceInformation.CreateWatcher(BluetoothLEDevice.GetDeviceSelectorFromPairingState(false));
            deviceWatcher = DeviceInformation.CreateWatcher(
                BluetoothLEDevice.GetDeviceSelectorFromPairingState(false));
            // TODO : Register Handlers, and Start watcher Check Documention!!!!
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;
            // Register event handlers before starting the watcher.
            // Added, Updated and Removed are required to get all nearby devices


            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.Stopped += DeviceWatcher_Stopped;

            // Start the watcher.
            deviceWatcher.Start();
        }

        /*private void DeviceWatcher_Added1(DeviceWatcher sender, DeviceInformation args)
        {
            throw new NotImplementedException();
        }*/


        /// <summary>
        /// DeviceWatcher_Stopped
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Stopped(DeviceWatcher a, object b)
        {
            Console.WriteLine("GameBLEGatt - DeviceWatcher_Stopped() - > DeviceWatcher_Stopped");
            if (bleState == BleState.EDiscovery || bleState == BleState.EConnecting)
            {
                if (bluetoothLeDevice != null)
                {
                    bluetoothLeDevice.ConnectionStatusChanged -= ConnectionStatusChangedHandler;
                    bluetoothLeDevice.Dispose();
                }
                if (selectedGattCharacteristic != null)
                {
                    selectedGattCharacteristic.ValueChanged -= Characteristic_ValueChanged;
                }
                bleState = BleState.EDiscovery;
                deviceWatcher.Start();
            }
        }

        private void ConnectionStatusChangedHandler(BluetoothLEDevice sender, object o)
        {
            Console.WriteLine("GameBLEGatt - ConnectionStatusChangedHandler()");
            if (bluetoothLeDevice.ConnectionStatus == BluetoothConnectionStatus.Connected)
            {
                bleState = BleState.EConnected;
            }
            else if (bluetoothLeDevice.ConnectionStatus == BluetoothConnectionStatus.Disconnected)
            {
                if (bluetoothLeDevice != null)
                {
                    bluetoothLeDevice.ConnectionStatusChanged -= ConnectionStatusChangedHandler;
                    bluetoothLeDevice.Dispose();
                }
                if (selectedGattCharacteristic != null)
                {
                    selectedGattCharacteristic.ValueChanged -= Characteristic_ValueChanged;
                }

                bleState = BleState.EDiscovery;
                deviceWatcher.Start();
            }
        }

        /// <summary>
        /// DeviceWatcher_Removed
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Removed(DeviceWatcher a, DeviceInformationUpdate b)
        {
            //Console.WriteLine("DeviceWatcher Removed");
        }

        /// <summary>
        /// DeviceWatcher_Updated
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public void DeviceWatcher_Updated(DeviceWatcher a, DeviceInformationUpdate b)
        {
            //Console.WriteLine("DeviceWatcher Updated");
        }

        /// <summary>
        /// DeviceWatcher_AddedAsync
        /// </summary>
        /// <param name="deviceWatcher"></param>
        /// <param name="deviceInformation"></param>
        public void DeviceWatcher_Added(DeviceWatcher deviceWatcher, DeviceInformation deviceInformation)
        {
            
            // TODO : Check that bluetooth name is same what you setup on esp32 side
            if (deviceInformation.Name == "TiittoESP32" && bleState == BleState.EDiscovery)
            {
                bleState = BleState.EConnecting;
                Console.WriteLine("GameBLEGatt - DeviceWatcher_Added() - > Device Found");

                // TODO : Stop Device Watcher
                deviceWatcher.Stop();

                // TODO : Connecting to the device

                bluetoothLeDevice = Task.Run(async () => await BluetoothLEDevice.FromIdAsync(deviceInformation.Id)).Result;
                bluetoothLeDevice.ConnectionStatusChanged += ConnectionStatusChangedHandler;
                // HOX!! Use Task.Run for all async methods
                // // Note: BluetoothLEDevice.FromIdAsync must be called from a UI thread because it may prompt for consent.


                // TODO : Enumerating supported services and characteristics

                // Get Gatt Services
                
                GattDeviceServicesResult result = Task.Run(async () => await bluetoothLeDevice.GetGattServicesAsync()).Result;
                //BluetoothLEDevice result = Task.Run(async () => await bluetoothLEDevice.GetGattServicesAsync()).Result;
                // Now that you have a BluetoothLEDevice object, the next step is to discover what data the device exposes. The first step to do this is to query for services:

                
                if (result.Status == GattCommunicationStatus.Success)
                {
                    var services = result.Services[2];

                    // TODO: Get Chharastics

                    // Once the service of interest has been identified, the next step is to query for characteristics.
                    GattCharacteristicsResult resultGATT = Task.Run(async () => await services.GetCharacteristicsAsync()).Result;
                    //GattCharacteristicsResult resultGATT = Task.Run(async () => await services.GetCharacteristicsAsync()).Result;

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
                            selectedGattCharacteristic = characteristics[0];
                            selectedGattCharacteristic.ValueChanged += Characteristic_ValueChanged;
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

                    if (bluetoothLeDevice.ConnectionStatus == BluetoothConnectionStatus.Connected)
                    {
                        bleState = BleState.EConnected;
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

            Console.WriteLine(input);
            Console.WriteLine("_______");


            // TODO : Perform Read/Write operations on a characteristic
            // Read Values and call tcmpGameClient.SendMessage and pass readed byte array

        }
    }
}
