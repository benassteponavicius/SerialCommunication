using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorData
{
   private int ID;
   private int humidity;
   private int waterLevel;
   private DateTime date;

   public SensorData(int id, int humidity, int waterLevel, DateTime date)
   {
      this.ID = id;
      this.humidity = humidity;
      this.waterLevel = waterLevel;
      this.date = date;
   }

   public SensorData()
   {
      ID = 0;
      humidity = 0;
      waterLevel = 0;
      date = DateTime.Now;
   }

   public int getID()
   {
      return ID;
   }
   public int getHumidity()
   {
      return humidity;
   }
   public int getWaterLevel()
   {
      return waterLevel;
   }
   public DateTime getDate()
   {
      return date;
   }

   public override string ToString()
   {
      return $"ID: {ID}  HUMIDITY: {humidity}%  WATER LEVEL: {waterLevel}  DATE: {date}";
   }
}
