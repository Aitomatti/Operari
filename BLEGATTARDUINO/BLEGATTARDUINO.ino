/*
    Based on Neil Kolban example for IDF: https://github.com/nkolban/esp32-snippets/blob/master/cpp_utils/tests/BLE%20Tests/SampleWrite.cpp
    Ported to Arduino ESP32 by Evandro Copercini
*/

#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>
#include <BLE2902.h>
// See the following for generating UUIDs:
// https://www.uuidgenerator.net/

//Gyro/accel
#include "SparkFunLSM6DS3.h"
/*
 * Asennus ohjeet SparkFunLSM6DS3.h kirjastoon
 * 
 * 1. Sketsi > Sisällytä Kirjasto > Ylläpidä Kirjastoja
 * 2. Kirjoita hakuun "Accelerometer And Gyroscope LSM6DS3" (Oikean kirjaston on tehnyt Seeed Studio)
 * 3. Asenna versio 2.0 (tai uudempi)
 */
#include "Wire.h"

//Create a instance of class LSM6DS3
LSM6DS3 myIMU(I2C_MODE, 0x6A);


#define SERVICE_UUID        "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define CHARACTERISTIC_UUID "beb5483e-36e1-4688-b7f5-ea07361b26a8"


bool deviceConnected = false;
class MyServerCallbacks: public BLEServerCallbacks {
    void onConnect(BLEServer* pServer) {
      deviceConnected = true;
    };

    void onDisconnect(BLEServer* pServer) {
      deviceConnected = false;
    }
};


BLECharacteristic *pCharacteristic ;

const int PushButton = 16;

enum buttonstate {
  one = 0b0001,
  two = 0b0010,
  three = 0b0100
};


void setup() {
  
  Serial.begin(115200);

  Serial.println("Marko Tiitto");

  BLEDevice::init("TiittoESP32");
  BLEServer *pServer = BLEDevice::createServer();
  pServer->setCallbacks(new MyServerCallbacks());
  BLEService *pService = pServer->createService(SERVICE_UUID);
  
  pCharacteristic = pService->createCharacteristic(
                      CHARACTERISTIC_UUID,
                      BLECharacteristic::PROPERTY_READ   |
                      BLECharacteristic::PROPERTY_WRITE  |
                      BLECharacteristic::PROPERTY_NOTIFY |
                      BLECharacteristic::PROPERTY_INDICATE
                    );

  pCharacteristic->addDescriptor(new BLE2902());
  pService->start();
  pServer->getAdvertising()->start();

  pinMode(PushButton, INPUT);

  //Grove 6-axis setup
      if (myIMU.begin() != 0) {
        Serial.println("Device error");
    } else {
        Serial.println("Device OK!");
    }
}

void loop() {
  if (deviceConnected) { 
    int Push_button_state = digitalRead(PushButton);
    Serial.println(Push_button_state);

    int gyroX = myIMU.readFloatGyroX();
    Serial.println(gyroX);
    int mask = 0;

    mask = gyroX | one;
  
  pCharacteristic->setValue(mask);
  pCharacteristic->notify();
  pCharacteristic->notify(); 

    int gyroY = myIMU.readFloatGyroY();
    Serial.println(gyroY);

    mask = gyroY | two;
  
  pCharacteristic->setValue(gyroX);
  pCharacteristic->notify();
  pCharacteristic->notify(); 

/*
    int gyroY = myIMU.readFloatGyroY();
    Serial.println(gyroY);
   

  
  pCharacteristic->setValue(gyroY);
  pCharacteristic->notify();
  pCharacteristic->notify(); 

   int gyroZ = myIMU.readFloatGyroZ();
   Serial.println(gyroZ);


  
  pCharacteristic->setValue(gyroZ);
  pCharacteristic->notify();
  pCharacteristic->notify();  */
  }
  


    //int gyroX = myIMU.readFloatGyroX();
    //Serial.println(gyroX);
    
  delay(100);
  
}
