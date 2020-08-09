using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject messageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(messageText.activeInHierarchy);
    }
}
