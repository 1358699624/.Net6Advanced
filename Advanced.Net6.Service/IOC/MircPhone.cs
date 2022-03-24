using Advanced.Net6.Interface;

namespace Advanced.Net6.Service
{
    public class MircPhone:IMircPhone
    {
        public MircPhone() {
            Console.WriteLine($"{this.GetType().Name}被构造了。");
        }
    }
}