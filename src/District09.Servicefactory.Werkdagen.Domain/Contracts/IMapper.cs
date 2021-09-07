namespace District09.Servicefactory.Werkdagen.Domain.Contracts
{
    public interface IMapper<in TInput, out TOutput>
    {
        public TOutput Map(TInput input);
    }
}