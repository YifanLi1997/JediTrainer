using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recharge_mana : MonoBehaviour
{
    private int manaMax = -5;
    private int comp = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        comp++;
        if (comp > 10 && this.gameObject.GetComponent<RectTransform>().offsetMax.x < manaMax)
        {
            this.gameObject.GetComponent<RectTransform>().offsetMax += new Vector2(1, 0);
            comp = 0;
        }
    }
}
