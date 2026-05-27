using System;
using System.Collections.Generic;
using System.Text;

namespace DI.TightCoupling
{
    //CLASS CHÍNH LÀ ĐÂY, LO XỬ LÝ DATA TRONG RAM
    //GUID -> CONTROLLER -> SERVICE --> REPO (JPA/UTIL)
    public class UserService
    {
        //CÓ ÍT NHẤT 2 DEPENDENCY SERVICE NÓ CẦN
        //1. USER-REPO GIÚP CRUD TABLE ACCOUNT
        //2. GỬI EMAIL/SMS/WHATSAPP CONFIRM
        private UserRepo userRepo = new UserRepo(); //dependency, tight coupling, chủ động quản lí object dependency

        private EmailSender emailSender = new EmailSender(); //dependency, tight coupling, chủ động tạo object trong lòng!!!
        //new Service, có 2 chú này đc new luôn!!! 
        //hard-coded dependency, cứng dependency vào đây 
        //full-control, direct-control dependency: tự khai báo, tự new!!!
        //vấn đề: sau này thay bằng class SMS, WhatsApp phải sửa code class chính này!!!

        /*
         Nghĩa là mình có class UserService khi mà người dùng ví dụ: trang web dành cho giáo dục thì thường phụ huynh ko có Email thì dùng SMS
        SDT
        => Nhưng mà khi mà đổi qua dùng SMS thì mình phải tạo class mới SMSSender nhưng vậy thì mình đang hard-code => thay đổi luôn class UserService vì mình thay đổi Email sang SMS mà

        *Thiết kế*: phải làm sao cho khi mà thay đổi sang SMS thì mình ko cần phải thay đổi class EmailSender luôn
         */

        //CÓ NHIỀU HÀM LIÊN QUAN ĐẾN TABLE USER: 
        //GetAllAccount()  FindByEmail()   FindByPhone()   DeleteAccount()  UpdateAccount()
        //.....

        //nhận vào full thông tin Account từ cái web form đăng kí, hoặc nhận vào dto
        //chứa email, phone, whatsapp id bên trong trích ra
        public void RegisterAccount(Account acc)
        {
            //TODO: gọi repo để xuống table!!!! XÀI DEPENDENCY 1

            //GỬI EMAIL CONFIRM - XÀI DEPENDENCY 2
            //
            emailSender.SendEmail("demo01@gmail.com", "Please input to the OTP....");
        }


    }
}


// Tight coupling: là class A khai báo biến của class B và chủ động new
//class A chỉ cần new thì có B được new 

//class A: class Service, xài class B, chủ động new luôn -> tight coupling
//class B: class EmailSender - dependency của A 
