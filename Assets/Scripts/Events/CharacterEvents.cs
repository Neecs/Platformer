using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    public static UnityAction<GameObject, int> characterDamaged;
    public static UnityAction<GameObject, int> characterHealed;

}   
