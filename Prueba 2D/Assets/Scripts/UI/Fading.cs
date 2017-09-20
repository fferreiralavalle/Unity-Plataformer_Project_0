using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture;
    public float fadeInSpeed = 0.8f;
    public float fadeOutSpeed = 0.8f;
    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;



    private void OnGUI()
    {
        //fade out/in the alpha value using a direction, speed and time.deltaTime
        //to convert the operator in seconds
        if (Mathf.Sign(fadeDir)==1)
            alpha += fadeDir * fadeOutSpeed * Time.deltaTime;
        else
            alpha += fadeDir * fadeInSpeed * Time.deltaTime;
        //force (clamp) the number between 0 and 1 because GUI.color uses alpha between 1 and 0
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;                                                  //Makes render last
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture); //Draws texture to fit hole screen

    }

    //sets fadeDir to the direction parameter

    public float beginFade (int direction)
    {
        fadeDir = direction;
        // return fadeSpeed halps calculate to time the SceneManager.loadLevel();
        if (Mathf.Sign(fadeDir) == 1)
            return (1 / fadeOutSpeed);
        else
            return (1/fadeInSpeed);
    }

}
