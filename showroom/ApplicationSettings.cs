using Godot;
using System;

public partial class ApplicationSettings : Node
{

    [ExportCategory("References")]
    [Export] public Node3D ModelTurntable;
    [Export] public Camera3D Camera;
    [Export] public Node3D SecondaryLightHolder;
    [Export] public MeshInstance3D FillLight;
    [Export] public MeshInstance3D KeyLight;


    [ExportCategory("Turntable Settings")]
    [Export] public float RotationSpeed = .5f;
    [Export] bool IsTurnTableActive = false;
    float stopAngle = 0f;
    Vector3 resetTurntableRotation = new Vector3(0,0,0);

    [ExportCategory("Lighting Settings")]
    public Vector3 scale = new Vector3(1,1,1); //Allows the user to adjust how far all lights are from the model.
    Vector3 resetScale = new Vector3(1,1,1); //Reset the lighting position.
    
    [ExportCategory("Camera Settings")]
    [Export] public float CameraFOV = 75f;


    public override void _PhysicsProcess(double delta)
    {
       
       if(IsTurnTableActive)
       {
            ActivateTurntable(delta);
       }
       else
       {
            DeActivateTurntable();
       }



    }

    public override void _Input(InputEvent @event)
    {
        if(Input.IsActionPressed("TurntableToggle") && !IsTurnTableActive)
        {
            IsTurnTableActive = true; //Activates the Turntable function.
            GD.Print("Turn Table On");
        }
        else if (Input.IsActionPressed("TurntableToggle") && IsTurnTableActive)
        {
            IsTurnTableActive = false;
            DeActivateTurntable();
            GD.Print("Stop Turn Table");
        }

        if(Input.IsActionPressed("ResetTurntable"))
        {
            ResetTurntable();
            GD.Print("Turn Table has been reset");
        }
    }

// Turntable Functions
    void ActivateTurntable(double delta)
    {
        ModelTurntable.RotateY( (float)delta * RotationSpeed);
    }

    void DeActivateTurntable()
    {
        ModelTurntable.RotateY(stopAngle);
        
    }
    void ResetTurntable()
    {
        ModelTurntable.Rotation = resetTurntableRotation;
        IsTurnTableActive = false;
    }



}
