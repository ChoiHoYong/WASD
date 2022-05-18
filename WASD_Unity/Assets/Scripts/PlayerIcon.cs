using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    private Image image;
    private Transform playerTransform;
    // Start is called before the first frame update
    public void Init()
    {
        image = GetComponent<Image>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null)
            return;
        UIHelper.LookAt(playerTransform, image.transform);
    }
}
