namespace Project.Runtime.Gameplay.Inventory
{
    public class ObservablePropSO<T> : ObservableSO
    {
        public T _value;

        public T value
        {
            get { return _value; }
            set
            {
                this._value = value;
                Notify();
            }
        }
    }
}