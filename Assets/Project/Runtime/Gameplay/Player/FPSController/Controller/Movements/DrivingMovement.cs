using System.Collections;
using System.Collections.Generic;
using Project.Runtime.Gameplay.Vehicles;
using UnityEngine;

public class DrivingMovement : MovementType
{
    public VehicleMotor currentVehicle;
    public override void Start()
    {
        base.Start();
    }

    public override void Movement()
    {
        
    }

    public void UpdateCurrentVehicle()
    {
        if (currentVehicle == null)
        {
            Debug.Log("Called");
            currentVehicle = GetComponentInParent<VehicleMotor>();
        }
        else
        {
            currentVehicle = null;
        }
    }
    
}
