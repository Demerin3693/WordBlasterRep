using UnityEngine;
using System.Collections;
using TMPro; // *** Це ключова зміна для TextMesh Pro ***

public class KeepFocusTMP : MonoBehaviour
{
    // Змінюємо тип з InputField на TMP_InputField
    private TMP_InputField inputField;
    
    void Start()
    {
        // Отримуємо компонент TMP_InputField на цьому ж об'єкті
        inputField = GetComponent<TMP_InputField>();
        
        if (inputField != null)
        {
            // Додаємо слухача до події OnEndEdit
            inputField.onEndEdit.AddListener(OnEndEditHandler);
        }
        else
        {
            Debug.LogError("На об'єкті немає компонента TMP_InputField!");
        }
    }

    private void OnEndEditHandler(string inputString)
    {
        // Запускаємо корутину для відкладеної повторної активації
        StartCoroutine(ReActivateInputFieldDelayed());
    }

    private IEnumerator ReActivateInputFieldDelayed()
    {
        // Чекаємо кінця поточного кадру
        // Це дає Unity час завершити свою внутрішню логіку втрати фокуса
        yield return new WaitForEndOfFrame(); 

        // Тепер повертаємо фокус назад у поле введення
        inputField.ActivateInputField();
        inputField.Select();
    }
}
