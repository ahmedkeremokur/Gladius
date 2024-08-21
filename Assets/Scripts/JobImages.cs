using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobImages : MonoBehaviour
{

    public Sprite[] images;
    public Image displayImage;

    public void ChangeImage(int imageIndex)
    {
        if (imageIndex >= 0 && imageIndex < images.Length)
        {
            displayImage.sprite = images[imageIndex];
        }
    }
}
