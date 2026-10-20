using UnityEngine;

public class HangmanDrawing : MonoBehaviour
{
    public GameObject[] bodyParts;

    public void UpdateDrawing(int wrongGuesses)
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].SetActive(i < wrongGuesses);
        }
    }
}