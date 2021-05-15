using System.Threading.Tasks;

namespace WFEngine.Service.Channels
{
    public interface IChannel<T>
    {
        void SendProducer(T value);
        void ExecuteConsumer();
    }
}
