using System;
using System.Collections.Generic;
using System.Text;

namespace DI.LooseCoupling
{
    public class EmailSender
    {
        //TỚ EMAIL-SENDER EMAIL RẤT GIỎI VỤ GỬI EMAIL 
        //SRP THỎA!!!
        public void SendEmail(string recipient, string message)
        {
            //TODO: LOGIC XỬ LÝ GỬI EMAIL: SETUP ACCOUNT ĐỂ ĐÓNG VAI NGƯỜI GỬI (FROM - MÌNH GỬI, APP GỬI)
            //      FORMAT EMAIL CHO PRO....

            //thông báo câu thành công 
            Console.WriteLine("(DI): Mail was sent to: " + recipient + " successfully!!!!!");

            //khai báo dependency bên trong cái Service thông qua cái việc gửi mail
        }
    }
}