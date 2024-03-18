using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] private Text hpText;
    public void OnInit(float damage)
    {
        this.hpText.text = damage.ToString();
        Invoke(nameof(OnDespawn), 1f);
    }
    
    public void OnDespawn()
    {
        Destroy(gameObject);
    }
}
