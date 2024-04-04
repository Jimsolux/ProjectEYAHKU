using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ChangeFov : MonoBehaviour
{
    public Camera camera;
    public float fov = 90;
    public float changeRate = 0.001f;

    public void ChangeTheFov()
    {
        camera.fieldOfView =  Mathf.Lerp(camera.fieldOfView, camera.fieldOfView = fov, changeRate);
    }
}
