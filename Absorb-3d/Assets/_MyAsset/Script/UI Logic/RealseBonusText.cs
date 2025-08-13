using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealseBonusText : MonoBehaviour
{
    private SetBonusText setText;
    public string textBonus;
    void Start()
    {
        setText = GetComponent<SetBonusText>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -1)
        {
            setText.callText(textBonus);
            Destroy(gameObject);
        }
    }
}
