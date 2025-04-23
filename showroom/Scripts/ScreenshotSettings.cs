using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


public enum FileFormats
{
    WEBP,
    JPG,
    PNG
}

[Tool]
public partial class ScreenshotSettings : Node
{

    [Export] public string cmdScreenshot = "TakeScreenshot";
    [Export] public string fileSavePath = "user://ShowroomScreenshots/";
    [Export] public float imgQuality = 0.8f;
    [Export] public bool imgLossy = true;
    [Export] public FileFormats imgFormats = FileFormats.WEBP;
    public override void _Ready()
    {
        
    }

    public override void _Input(InputEvent @event)
    {
        
        if(InputMap.HasAction(cmdScreenshot) && @event.IsActionPressed(cmdScreenshot))
        {
            GD.Print("Screenshot has been taken");
            SaveScreenshot();
        }
    }

    void SaveScreenshot()
    {
        int error = 0;
        if(!DirAccess.DirExistsAbsolute(fileSavePath))
        {
           
            //Checks if your able to create a folder
            error = (int)DirAccess.MakeDirRecursiveAbsolute(fileSavePath);
            if(error == 1)
            {
                GD.PushError(String.Format("Couldn't create a folder at {0}, {1} ", fileSavePath,error));

            }
        }

        Image img = GetViewport().GetTexture().GetImage();
        String path = GetFilePath();
        String fileAddition = ""; // If there is another file with the exact same name in the folder.
        String extention = GetFileExtension();

        while(FileAccess.FileExists(path + fileAddition + GetFileExtension()))
        {
            fileAddition = String.Format("{0}",fileAddition.ToInt() + 1);
            
        }
        path += fileAddition;
        path += GetFileExtension();

        switch(imgFormats)
        {

            case FileFormats.WEBP:
                img.SaveWebp(path,imgLossy,imgQuality);
            break;
            case FileFormats.JPG:
                img.SaveJpg(path,imgQuality);
            break;
            case FileFormats.PNG:
                img.SavePng(path);
            break;
        }

        if(error == 1)
        {
               GD.PushError(String.Format("Couldn't Save Image at {0}, {1}"),path,error);
        }


    }

    String GetFilePath()
    {
        String fileName = Time.GetDateStringFromSystem();
        fileName = fileName.Replace('T','_');
        fileName = fileName.Replace(':', '-');
        return fileSavePath + fileName;
    }

    String GetFileExtension()
    {
        string extension = "";
        switch(imgFormats)
        {
            case FileFormats.WEBP:
                extension = ".webp";
            break;
            case FileFormats.JPG:
                extension = ".jpg";
            break;
            case FileFormats.PNG:
                extension = ".png";
            break;
            default:
                extension = ".webp";
                break;
        }
        return extension;
    }
}

