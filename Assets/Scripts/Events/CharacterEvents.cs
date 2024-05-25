using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    public static UnityEvent<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;

    public void Awake()
    {
        // Inicializar characterDamaged si es null
        if (characterDamaged == null)
        {
            characterDamaged = new UnityEvent<GameObject, int>();
        }
    
    }
}   
