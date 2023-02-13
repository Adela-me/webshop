namespace Domain.Common;
public class Result<TValue> : Result
{
    private readonly TValue value;
    protected internal Result(TValue value, bool isSuccess, Error error)
        : base(isSuccess, error)
        => this.value = value;

    public TValue Value => IsSuccess
        ? value
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}
