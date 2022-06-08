using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    TMP_Text stage;
    // Start is called before the first frame update
    public void Init()
    {
        stage = GetComponent<TMP_Text>();
        stage.text = "Stage " + GameData.currStage;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
