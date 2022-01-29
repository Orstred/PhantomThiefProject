


public class Treasure : Interactable
{
    public override void Interact()
    {
        base.Interact();
        GameManager.instance.VictoryScreen();
    }
}
