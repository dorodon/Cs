namespace l3
{
    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}
