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
        Debug.Log(character.name + " ha sido dañado con " + damage + " puntos de daño.");
        // Aquí puedes añadir más lógica para manejar el daño
    }
}
