[System.Serializable]
public class Dialogue
{
    public string text;
    public string[] options;
    public int[] nextDialogueIndices;

    public Dialogue(string text, string[] options, int[] nextDialogueIndices)
    {
        this.text = text;
        this.options = options;
        this.nextDialogueIndices = nextDialogueIndices;
    }
}
