#include "BluetoothSerial.h"

#include <Adafruit_MPU6050.h>
// #include <Adafruit_HMC5883_U.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>

#define PIN_JOYSTICK_X 12
#define PIN_JOYSTICK_Y 14
#define PIN_JOYSTICK_SEL 27

#define PIN_BUTTON_1 32
#define PIN_BUTTON_2 33

BluetoothSerial SerialBT;

Adafruit_MPU6050 mpu;
sensors_event_t a, g, temp;
sensor_t aSensor, gSensor;

/* Assign a unique ID to this sensor at the same time */
// Adafruit_HMC5883_Unified mag = Adafruit_HMC5883_Unified(12345);

// void magDisplaySensorDetails(void)
// {
//   sensor_t sensor;
//   mag.getSensor(&sensor);
//   Serial.println("------------------------------------");
//   Serial.print  ("Sensor:       "); Serial.println(sensor.name);
//   Serial.print  ("Driver Ver:   "); Serial.println(sensor.version);
//   Serial.print  ("Unique ID:    "); Serial.println(sensor.sensor_id);
//   Serial.print  ("Max Value:    "); Serial.print(sensor.max_value); Serial.println(" uT");
//   Serial.print  ("Min Value:    "); Serial.print(sensor.min_value); Serial.println(" uT");
//   Serial.print  ("Resolution:   "); Serial.print(sensor.resolution); Serial.println(" uT");  
//   Serial.println("------------------------------------");
//   Serial.println("");
//   delay(500);
// }

void setup() {
  Serial.begin(115200);

  SerialBT.begin("Mulheres que Inspiram - Controle");
  Serial.println("Bluetooth Device is Ready to Pair");

  pinMode(PIN_JOYSTICK_X, INPUT);
  pinMode(PIN_JOYSTICK_Y, INPUT);
  pinMode(PIN_JOYSTICK_SEL, INPUT_PULLUP);

  Serial.println("Finished joystick pinMode setup");

  pinMode(PIN_BUTTON_1, INPUT_PULLUP);
  pinMode(PIN_BUTTON_2, INPUT_PULLUP);

  Serial.println("Finished buttons pinMode setup");
  
  // if(!mag.begin())
  // {
  //   Serial.println("HMC5883 not connected!");
  //   while(1);
  // }
  // Serial.println("HMC5883 ready!");

  while (!mpu.begin()) {
    Serial.println("MPU6050 not connected!");
    delay(1000);
  }
  Serial.println("MPU6050 ready!");

  /* Display some basic information on this sensor */
  // magDisplaySensorDetails();
}

// void magDisplaySensorEvent() {
//   /* Get a new sensor event */ 
//   sensors_event_t event; 
//   mag.getEvent(&event);
 
//   /* Display the results (magnetic vector values are in micro-Tesla (uT)) */
//   Serial.print("X: "); Serial.print(event.magnetic.x); Serial.print("  ");
//   Serial.print("Y: "); Serial.print(event.magnetic.y); Serial.print("  ");
//   Serial.print("Z: "); Serial.print(event.magnetic.z); Serial.print("  ");Serial.println("uT");

//   // Hold the module so that Z is pointing 'up' and you can measure the heading with x&y
//   // Calculate heading when the magnetometer is level, then correct for signs of axis.
//   float heading = atan2(event.magnetic.y, event.magnetic.x);
  
//   // Once you have your heading, you must then add your 'Declination Angle', which is the 'Error' of the magnetic field in your location.
//   // Find yours here: http://www.magnetic-declination.com/
//   // Mine is: -13* 2' W, which is ~13 Degrees, or (which we need) 0.22 radians
//   // If you cannot find your Declination, comment out these two lines, your compass will be slightly off.
//   float declinationAngle = -0.35;
//   heading += declinationAngle;
  
//   // Correct for when signs are reversed.
//   if(heading < 0)
//     heading += 2*PI;
    
//   // Check for wrap due to addition of declination.
//   if(heading > 2*PI)
//     heading -= 2*PI;
   
//   // Convert radians to degrees for readability.
//   float headingDegrees = heading * 180/M_PI; 
  
//   Serial.print("Heading (degrees): "); Serial.println(headingDegrees);
  
//   delay(500);
// }

void loop() {
  // Read joystick values
  int valueX = analogRead(PIN_JOYSTICK_X);
  int valueY = analogRead(PIN_JOYSTICK_Y);
  int valueSEL = digitalRead(PIN_JOYSTICK_SEL);

  // Read buttons values
  int valueBUTTON1 = digitalRead(PIN_BUTTON_1);
  int valueBUTTON2 = digitalRead(PIN_BUTTON_2);

  // Read MPU6050 values
  mpu.getAccelerometerSensor()->getEvent(&a);
  mpu.getAccelerometerSensor()->getSensor(&aSensor);
  mpu.getGyroSensor()->getEvent(&g);
  mpu.getGyroSensor()->getSensor(&gSensor);
  mpu.getTemperatureSensor()->getEvent(&temp);

  // Create a buffer to hold the data
  byte dataBuffer[39];

  // Fill the buffer with joystick data
  dataBuffer[0] = valueX & 0xFF;    // Low byte of valueX
  dataBuffer[1] = valueX >> 8;      // High byte of valueX
  dataBuffer[2] = valueY & 0xFF;    // Low byte of valueY
  dataBuffer[3] = valueY >> 8;      // High byte of valueY
  dataBuffer[4] = valueSEL & 0xFF;  // Low byte of valueSEL
  dataBuffer[5] = valueSEL >> 8;    // High byte of valueSEL

  // Fill the buffer with MPU6050 data
  memcpy(&dataBuffer[6], &a.acceleration.x, sizeof(float));   // 4 bytes
  memcpy(&dataBuffer[10], &a.acceleration.y, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[14], &a.acceleration.z, sizeof(float));  // 4 bytes

  memcpy(&dataBuffer[18], &g.gyro.x, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[22], &g.gyro.y, sizeof(float));  // 4 bytes
  memcpy(&dataBuffer[26], &g.gyro.z, sizeof(float));  // 4 bytes

  memcpy(&dataBuffer[30], &temp.temperature, sizeof(float));  // 4 bytes

  // Fill the buffer with the two buttons data
  dataBuffer[34] = valueBUTTON1 & 0xFF;    // Low byte of valueBUTTON1
  dataBuffer[35] = valueBUTTON1 >> 8;      // High byte of valueBUTTON1
  dataBuffer[36] = valueBUTTON2 & 0xFF;    // Low byte of valueBUTTON2
  dataBuffer[37] = valueBUTTON2 >> 8;      // High byte of valueBUTTON2

  // Compute a simple checksum
  byte checksum = 0;
  for (int i = 0; i < 38; i++) {
    checksum ^= dataBuffer[i];
  }
  dataBuffer[38] = checksum;  // Store checksum in the last byte of the buffer

  // Send the buffer over BluetoothSerial
  size_t bytesWritten = SerialBT.write(dataBuffer, sizeof(dataBuffer));

  // magDisplaySensorEvent();
  
  // Check if the write was successful
  if (bytesWritten == sizeof(dataBuffer)) {
    Serial.println("Data sent successfully over BluetoothSerial.");
  } else {
    Serial.println("Failed to send data over BluetoothSerial.");
  }

  // Log the values being sent
  Serial.print("aSensor min/max: ");
  Serial.print(aSensor.min_value);
  Serial.print("/");
  Serial.print(aSensor.max_value);

  Serial.print(" gSensor min/max: ");
  Serial.print(gSensor.min_value);
  Serial.print("/");
  Serial.print(gSensor.max_value);

  Serial.print(" Sent Joystick X: ");
  Serial.print(valueX);
  Serial.print(", Y: ");
  Serial.print(valueY);
  Serial.print(", SEL: ");
  Serial.print(valueSEL);
  Serial.print(", BUTTON1: ");
  Serial.print(valueBUTTON1);
  Serial.print(", BUTTON2: ");
  Serial.print(valueBUTTON2);
  Serial.print(", Accel X: ");
  Serial.print(a.acceleration.x);
  Serial.print(", Y: ");
  Serial.print(a.acceleration.y);
  Serial.print(", Z: ");
  Serial.print(a.acceleration.z);
  Serial.print(", Gyro X: ");
  Serial.print(g.gyro.x);
  Serial.print(", Y: ");
  Serial.print(g.gyro.y);
  Serial.print(", Z: ");
  Serial.print(g.gyro.z);
  Serial.print(", Temp: ");
  Serial.println(temp.temperature);

  // // increment previous time, so we keep proper pace
  // microsPrevious = microsPrevious + microsPerReading;
}
