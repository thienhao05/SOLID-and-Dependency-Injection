using DI.TightCoupling;
using System.Security.Principal;

namespace DI.TightCouplingController
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //main trong này dùng DI.TightCoupling (PHỤ THUỘC CỨNG)
            //CLASS MAIN NÀY ĐÓNG VAI TRÒ UI, CONTROLLER, GỌI ĐIỀU KHIỂN NHỮNG CLASS Ở TẦNG DƯỚI: SERVICE, REPO, JPAUTIL...

            //SAU NÀY THAY BẰNG WEB PAGE, GUI...
            UserService userService = new UserService(); // new Service có sẵn trong là 2 dependency đó là Repo và EmailSender

            //                          new này phải có email, phone, whatsapp...
            userService.RegisterAccount(new Account());
            //THẰNG GỬI MAIL TRONG NÀY BỊ CHE MẤT RỒI
        }
    }
}