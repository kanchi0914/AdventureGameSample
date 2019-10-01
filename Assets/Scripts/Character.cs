using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class Character : MonoBehaviour {

    private GameObject charactorObject;
    private SpriteRenderer charactorImage;
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private CanvasGroup canvasGroup;

    private string Name;

    // Use this for initialization
    void Start ()
    {
        charactorObject = gameObject;
        charactorImage = charactorObject.GetComponent<SpriteRenderer>();
        LoadImage();
        gameObject.SetActive(false);
        Debug.Log(charactorObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadImage()
    {
        var temp = Resources.LoadAll<Sprite>("Image/Charactors").ToList();
        foreach (Sprite s in temp)
        {
            sprites.Add(s.name, s);
        }
    }

    public void SetImage(string imageID)
    {
        charactorImage.sprite = sprites[imageID];
        charactorObject.SetActive(false);
        Appear();
    }

    public void Appear()
    {
        charactorObject.SetActive(true);
        charactorImage.color = new Color(1f, 1f, 1f, 0);
        charactorImage.DOFade(1.0f, 0.2f);
    }


}
