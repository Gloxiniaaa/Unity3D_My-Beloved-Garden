public class FlowerBloomCommand : ICommand
{
    private Flower _flower;

    public FlowerBloomCommand(Flower flower)
    {
        _flower = flower;
    }

    public void Execute()
    {
        _flower.Bloom();
    }

    public void Undo()
    {
        _flower.ReverseBloom();
    }
}