using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class icon1script : MonoBehaviour
{
    Sprite image;

    

    void Start()
    {
        //왜인지 씬 넘어오면 list가 비어있음.
        SpriteRenderer spriteR = gameObject.GetComponent<SpriteRenderer>();
        GameData.avility_list.Add("Attack");
        Sprite[] sprites = Resources.LoadAll<Sprite>("Another/Titile,Loading,end/End_attack");
        spriteR.sprite = sprites[0];

        //if (GameData.avility_list[0] == "Attack")
        //    image = Resources.Load <Sprite>("Another/Titlte.Loading,end/End_attack");
        //if (GameData.avility_list[0] == "Hp")
        //    image = Resources.Load<Sprite>("Another/Titlte.Loading,end/End_hp");
        //if (GameData.avility_list[0] == "Speed")
        //    image = Resources.Load<Sprite>("Another/Titlte.Loading,end/End_speed");
    }

}
