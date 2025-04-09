namespace Core.Currency.Interfaces
{
    public interface IBindView
    {
        public void Bind(IBindViewModel viewModel);
        
        public void Unbind();
    }
}
