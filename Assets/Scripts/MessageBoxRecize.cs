using TMPro;
using UnityEngine;

public class MessageBoxRecize : MonoBehaviour
{
    public SpriteRenderer MessageBox;
    public TextMeshPro Text;

    private void OnEnable()
    {
        int charsCount = 0;
        string newString = "";
        string text = Text.text;
        int size = Text.text.Length;
        bool isEntered = false;

        foreach (var c in text)
        {
            charsCount++;
            newString += c;

            if (c == '.' && charsCount > 34 && size > 54 && isEntered == false)
            {
                isEntered = true;
                newString += "\n";
                size = charsCount;
            }
        }

        Text.text = newString;

        Vector2 newSize = new Vector2(size / 10f + 0.6f, MessageBox.GetComponent<SpriteRenderer>().size.y);
        MessageBox.GetComponent<SpriteRenderer>().size = newSize;
    }
}
