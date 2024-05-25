using UnityEngine;

public class EventListener : MonoBehaviour
{
    void OnEnable()
    {
        CharacterEvents.characterDamaged.AddListener(OnCharacterDamaged);
    }

    void OnDisable()
    {
        CharacterEvents.characterDamaged.RemoveListener(OnCharacterDamaged);
    }

    void OnCharacterDamaged(GameObject character, int damage)
    {
        Debug.Log(character.name + " ha sido da�ado con " + damage + " puntos de da�o.");
        // Aqu� puedes a�adir m�s l�gica para manejar el da�o
    }
}
