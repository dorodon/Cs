namespace l2
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
