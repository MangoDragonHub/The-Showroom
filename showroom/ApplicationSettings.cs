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
    Vector3 resetRotation = Vector3.Zero;

    [ExportCategory("Lighting Settings")]
    public Vector3 scale = new Vector3(1,1,1); //Allows the user to adjust how far all lights are from the model.
    Vector3 resetScale = new Vector3(1,1,1); //Reset the lighting position.
    
    [ExportCategory("Camera Settings")]
    [Export] public Node3D CameraArm;
    [Export] public float currentCameraFOV = 75f;
    float CameraFOV = 75f; //The default FOV setting
    float CameraMaxFOV = 150f;
    float CameraMinFOV = 30f;


    public override void _Ready()
    {
        Camera.Fov = currentCameraFOV;
    }

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

    public override void _Input(InputEvent ev)
    {
        float zoomInput = Input.GetActionStrength("CameraZoomOut") - Input.GetActionStrength("CameraZoomIn");
       

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

        //Zoom Scroll

        if(zoomInput > 0)
        {
            IncreaseZoom();
        }
        else if(zoomInput < 0)
        {
            DecreaseZoom();
        }
        if(Input.IsActionPressed("ResetZoom"))
        {
            ResetZoom();
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
        ModelTurntable.Rotation = resetRotation;
        IsTurnTableActive = false;
    }

// Camera Zoom Functions
    void IncreaseZoom()
    {
        Camera.Fov += 1;
        if(Camera.Fov >= CameraMaxFOV)
        {
            GD.Print("FOV reached max limit.");
            Camera.Fov = CameraMaxFOV;
        }
    }
    void DecreaseZoom()
    {
        Camera.Fov -= 1;
                if(Camera.Fov <= CameraMinFOV)
        {
            GD.Print("FOV reached min limit.");
            Camera.Fov = CameraMinFOV;
        }
    }
    void ResetZoom()
    {
        Camera.Fov = CameraFOV;
    }

}
