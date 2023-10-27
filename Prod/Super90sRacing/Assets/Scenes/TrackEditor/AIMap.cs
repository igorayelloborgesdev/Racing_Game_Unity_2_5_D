using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMap : CarMapUI
{
    [SerializeField]
    private Image image1;
    [SerializeField]
    private Image image2;

    public void ChangeImage1(Color color)
    {
        image1.color = color;
    }
    public void ChangeImage2(Color color)
    {
        image2.color = color;
    }
}
