GIẢI NGỐ VỀ DI - DEPENDENCY INJECTION

> TA SẼ HỌC NHỮNG MÓN

- DEPENDENCY
- DEPENDENCY INJECTION (TIÊM CHÍNH - ĐƯA THỨ BÊN NGOÀI VÀO TRONG)
- TIGHT COUPLING
- LOOSE COUPLING
- S, O, D (S.O.L.I.D)
- IoC (Inversion of Control)
- IoC CONTAINER
  ....

> KĨ NĂNG, KIẾN THỨC Ở TRÊN PHI NGÔN NGỮ, NÓ CÓ THỂ ÁP DỤNG CHUNG CHO MỌI NGÔN NGỮ LẬP TRÌNH, KHÔNG CHỈ RIÊNG JAVA, ÁP DỤNG CHO VIỆC THIẾT KẾ APP, CHIA CODE THÀNH CÁC THÀNH PHẦN ĐỂ DỄ KIỂM SOÁT, BẢO TRÌ, NÂNG CẤP, VÀ MỞ RỘNG TRONG TƯƠNG LAI.
> DÙNG ĐỂ ĐI TRẢ LỜI PHỎNG VẤN

I. DEPENDENCY LÀ GÌ?

1. NẾU CLASS A KHAI BÁO BIẾN THUỘC CLASS B, CẦN B ĐỂ GIÚP CÔNG VIỆC GÌ ĐÓ MÀ B CHUYÊN TRÁCH, B GIỎI, THÌ B GỌI LÀ DEPENDENCY CỦA A, NÓI CÁCH KHÁC A PHỤ THUỘC VÀO B THÌ B LÀ DEPENDENCY CỦA A.

```java
public class A {
    B objB; //objB là obj, thuộc, đc clone từ class B
            //B đc gọi là dependency của A, A thuộc vào B để làm việc gì đó
}

public class B { //giỏi việc nào đó, chuyên việc nào đó
    //...
    void doSomething() {
        //...
    }
}
```

2. DEPENDENCY CÒN LÀ CÁC THƯ VIỆN LẬP TRÌNH (CHẲNG QUA GỒM BÊN TRONG NHIỀU CLASS LÀM VIỆC GÌ ĐÓ RẤT GIỎI), TA CÓ JDBC DEPENDENCY, JUNIT DEPENDENCY, LOMBOK DEPENDENCY, HIBERNATE DEPENDENCY, JPA DEPENDENCY, VÀ CÒN NHIỀU THƯ VIỆN KHÁC NỮA.

3. A PHỤ THUỘC VÀO B, B LÀ DEPENDENCY CỦA A, TỨC LÀ 2 CLASS CÓ GẮN KẾT, CẦN NHAU (A CẦN B ĐÚNG HƠN) GỌI LÀ COUPLING

- GẮN KẾT CHẶT CHẼ HAY LỎNG LẺO

II. TIGHT COUPLING, LOOSE COUPLING - CHẮC CHẮN DINH DÁNG DEPENDENCY, CLASS NÀY CẦN CLASS KIA

1. TIGHT COUPLING - GẮN KẾT, PHỤ THUỘC CHẮT CHẼ

- CLASS A CẦN CLASS B, QUẢN LÍ LUÔN VÒNG ĐỜI OBJECT CLASS B (TẠO, HỦY) TRONG LÒNG CLASS A

```java
public static void main(String[] args) {
    A objA = new A(); //KHI NEW A ĐÃ CÓ NGAY B BÊN TRONG LÒNG
                      // CÓ A LÀ ĐÃ CÓ B
}

public class A {
    B objB = new B(); // tight coupling,
}

public class B { //giỏi việc nào đó, chuyên việc nào đó
    //...
    void doSomething() {
        //...
    }
}
```

> [!NOTE]
> ## GHI CHÚ: TIGHT COUPLING
> CLASS A KHAI BÁO BIẾN OBJECT CỦA CLASS B VÀ TỰ `NEW B()` NGAY TRONG CLASS A.
> - A TRỰC TIẾP KIỂM SOÁT OBJECT B, ĐƯỢC GỌI LÀ **TIGHT COUPLING**, **FULL CONTROL DEPENDENCY** HOẶC **HARD-CODED DEPENDENCY**.
> - TRONG LÒNG A, CODE CỦA A ĐÃ CỨNG SẴN OBJECT B.
> - **DIRECT CONTROL**: A TRỰC TIẾP KIỂM SOÁT VIỆC KHỞI TẠO VÀ SỬ DỤNG B.

2. LOOSE COUPLING - GẮN KẾT LỎNG LẺO, PHỤ THUỘC LỎNG LẺO - A THẢ LỎNG B RA, KO KIỂM SOÁT B CHẶT CHẼ NỮA, DEPENDENCY ĐC THẢ RA, LỘ DIỆN RA NGOÀI, THẢ LỎNG RA, KO NEW B TRONG A!!!!!
XEM THÊM IV, V : DEPENDENCY INJECTION, IoC, CONTAINER

- VẤN ĐỀ CỦA TIGHT COUPLING

* A CHỈ CHƠI VỚI B MÀ THÔI
* KHI B CHƯA CODE XONG, THÌ KHÓ CÓ THỂ RUN ĐC A
* NẾU MUỐN THAY THẾ B BẰNG B' TƯƠNG ĐƯƠNG VỀ KHẢ NĂNG GIẢI QUYẾT VẤN ĐỀ (THAY HIBERNATE BẰNG ECLIPSE LINK ???), THÌ CHẮC CHẮN PHẢI SỬA CODE CỦA A!!!
* ....

---

VÍ DỤ DEMO
- TA CẦN LÀM APP, TRONG ĐÓ CÓ CHỨC NĂNG ĐĂNG KÍ MEMBER - REGISTER AN ACCOUNT/ SIGN UP
- UI(FORM ĐĂNG KÍ)
    - GÕ USERNAME/PASS/RE-PASS, EMAIL, PHONE, WHATSAPP ID, 
    - NHẤN NÚT ĐĂNG KÍ
- ...
            | | | |
- UserService (Business Logic Layer - BLL)
    - hàm/method registerAccount(Account obj) { //obj: chứa username, pass, email, phone, whatsappid
        code phải gọi UserRepository - chuyên giỏi CRUD table Account
                                        //dependency 
        GỬI MAIL CONFIRM; HOẶC 
        GỬI SMS CONFIRM; HOẶC
        GỬI NOTI ĐẾN WHATSAPP;
    }

            | | | |                             | | | | 
                                            TÁCH CODE THÀNH RIÊNG RA KHỎI UserService
                                            thành Service riêng lo SMS, Mail, WhatsApp

- UserRepository (Data Access Layer - DAL)
    - hàm CRUD table Account, dùng JPA/HIBERNATE/JpaUtil -> Spring Data/Spring JPA

            | | | |
            TABLE ACCOUNT

- USER SERVICE CÓ 2 DEPENDENCY 
1. USER REPOSITORY - CHUYÊN GIỎI VIỆC CRUD TABLE ACCOUNT
2. NOTI SENDER - GẢ CHUYÊN LO SMS, MAIL, WHATSAPP

III. S TRONG SOLID - SRP - SINGLE RESPONSIBILITY PRINCIPLE 
1. MỖI CLASS ĐƯỢC THIẾT KẾ RA CHỈ NÊN GIẢI QUYẾT 1 CÔNG VIỆC NÀO ĐÓ, CHUYÊN BIỆT ĐỂ LÀM 1 VIỆC GÌ ĐÓ NÓ GIỎI. NÓ SẼ CUNG CẤP DỊCH VỤ CHO BÊN KHÁC DÙNG
- MẠNG: 1 CLASS KHI CẦN CHỈNH SỬA, THÌ CHỈ CẦN CÓ 1 LÍ DO(CHỦ THỂ NÀO ĐÓ) ĐỂ CHỈNH SỬA, 
                                                  1 CHỖ/CHỦ ĐỂ CHỈNH SỬA

* TA CÓ CLASS pulbic class NotiSender {
    - code gửi SMS              
    - code gửi MAIL
    - code gửi WHATSAPP
}

> ĐANG VI PHẠM SRP, VÌ CÓ ĐẾN 3 CHỖ KHÁC NHAU ĐỂ SỬA KHI CẦN NÂNG CẤP CODE, MỞ RỘNG CODE, FIX CODE, VÍ DỤ
- SỬA SMS ĐỂ SUPPORT CHO TỔNG ĐÀI VIETTEL, VỚI MOBI, VỚI VINA
- SỬA SMS ĐỂ SUPPORT CHO Gmail, Yahoo, Outlook
- Sửa WhatsApp

* TA CÓ CLASS pulbic class NotiSender {
    - code gửi SMS              -> ĐẠT SRP, VÌ CHỈ CÓ SỬA SMS MÀ THÔI
    //- code gửi MAIL
    //- code gửi WHATSAPP
}

* KO NHẦM LẪN SRP NGHĨA LÀ CLASS CHỈ CÓ 1 HÀM MÀ PHẢI HIỂU LÀ 
- CLASS CÓ NHIỀU HÀM, CÁC HÀM XOAY QUANH 1 CHỦ THỂ, VẬT NÀO ĐÓ, ENTITY NÀO ĐÓ, OBJ NÀO ĐÓ CẦN ĐƯỢC XỬ LÝ
- CLASS USER-REPO CÓ 4 HÀM CRUD NHƯNG CHỈ XOAY QUANH USER HOẶC LÀ ACCOUNT TABLE!!!!! 1 CHỦ THỂ 

- NẾU CÓ NHIỀU CHỦ THỂ CẦN XỬ LÝ TRONG 1 CLASS, VI PHẠM SRP 
    NotiSender chứa cả SMS, Mail, WhatsApp, 3 chủ thể, vi phạm rồi

> [!NOTE]
> 1 class chỉ có 1 lí do để sửa

IV. DEPENDENCY INJECTION - TIÊM/CHÍCH DEPENDENCY VÀO CLASS CHÍNH!!!!!
- CLASS CHÍNH KO CÓ CHỦ ĐỘNG KIỂM SOÁT DEPENDENCY NỮA 
* CODE CŨ/TIGHT COUPLING, FULL CONTROL, DIRECT CONTROL, HARD-CODE DEPENDENCY
    new A(); có sẵn B được new bên trong, ta ko biết bên trong A có B luôn, ta nhìn bên ngoài A, ko biết đc rằng bên trong có A có B
* CODE MỚI: KO FULL CONTROL NỮA, B LỘ MẶT!!!! LỘ MẶT RỒI, CÓ KHẢ NĂNG BỊ THAY 
                    THẾ!!! -> MỞ RỘNG CHO TƯƠNG LAI...
    new A(); và phải new B(); ngoài A, chích/tiêm/truyền/inject new B() vào A
    LOOSE COUPLING, TAO CÓ A, CHƯA CHẮC ĐÃ CÓ MÀY B, DÙ MÀY B LÀ PHỤ THUỘC CỦA TAO?????
    TUI CẦN ANH, NHƯNG CHƯA CHẮC ĐÃ CÓ ANH KHI TÔI!!!!
    TUI CẦN ANH, NHƯNG ANH PHẢI CÓ MẶT CHO TUI NHÉ!!!!

>>>> LỢI ÍCH CỦA VIỆC THẢ LỎNG DEPENDENCY LÀ GÌ ?

TA CLASS A, CLASS SERVICE ĐÃ KO FULL-CONTROL VIỆC TẠO OBJECT B, MÀ ĐỂ VIỆC TẠO, VIỆC NEW B() 
NEW DEPENDENCY Ở CHỖ KHÁC, RỒI CHÍCH/TIÊM VÀO
    TA ĐÃ CHUYỂN GIAO, ĐẢO QUYỀN/GIẢM QUYỀN KIỂM SOÁT DEPENDENCY 

            IoC INVERSION OF CONTROL - ĐẢO QUYỀN KIỂM SOÁT VIỆC NEW DEPENDENCY()!!!!

---

1. IoC LÀ 1 NGUYÊN LÝ THIẾT KẾ CODE, CLASS PHỤ THUỘC NHAU, NÓ LÀ Ý TƯỞNG, NÓ LÀ LÍ THUYẾT
                                                        LỜI KÊU GỌI, CHỨ NÓ KO NÓI CỤ THỂ LÀM THẾ NÀO, NÓ ABSTRACT

2. DEPENDENCY INJECTION LÀ PHIÊN BẢN CỤ THỂ, IMPLEMENT CHO IcC

3. NGOÀI DEPENDENCY INJECTION, 1 VÀI CÁCH KHÁC ĐỂ ĐẠT ĐƯỢC IoC, ĐẢM BẢO VIỆC CLASS CHÍNH KO ÔM 
ĐỒN FULL CONTROL MỌI VIỆC, CẦN THÌ GỌI DỊCH VỤ BÊN NGOÀI!!!!

> [!NOTE]
> ## TÓM LẠI VỀ DEPENDENCY INJECTION
> 1. LÀ PHIÊN BẢN CỤ THỂ, IMPLEMENT
> 2. IoC LÀ NGUYÊN LÝ THIẾT KẾ, Ý TƯỞNG, LÍ THUYẾT, NÓ KO NÓI CỤ THỂ LÀM THẾ NÀO, NÓ ABSTRACT
> 3. DEPENDENCY INJECTION LÀ CÁCH ĐỂ ĐẠT ĐƯỢC IoC, ĐẢM BẢO VIỆC CLASS CHÍNH KO ÔM ĐỒN FULL CONTROL MỌI VIỆC, CẦN THÌ GỌI DỊCH VỤ 


IoC giống như ORM ý tưởng nó là abstract thôi, Dependency Injection là cái cụ thể
- Ta cần object này nhưng ta ko muốn new nó, còn viết như thế nào mà ko new nó mà vẫn dùng được ???
- Ta ko muốn kiểm soát hết mọi việc, việc chính của ta là xuống database, chứ ko phải gửi mail, cho nên gửi mail chắc chắn ta nhờ thằng khác làm nhưng ta cũng ko muốn new cái thằng đó luôn, đó là ý tưởng, câu đó chính là IoC.

Còn giờ mình viết code thì đó là Dependency Injection, mình sẽ viết code để đạt được IoC, để thả lỏng dependency ra, để class chính ko kiểm soát việc tạo ra dependency nữa, mà là thằng khác tạo ra rồi chích vào cho class chính dùng, đó là Dependency Injection.

> DI là 1 cái cài đặt hiện thực hóa cái IoC => Solution Architect, Software Architect
> Kiến trúc viết code
> Hiểu được nó và triển khai được nó, thì sẽ viết code tốt hơn, dễ bảo trì hơn, dễ nâng cấp hơn, dễ mở rộng hơn, dễ test hơn, dễ mock hơn, dễ stub hơn, dễ fake hơn, dễ unit test hơn, dễ integration test hơn, dễ end-to-end test hơn, dễ viết code theo hướng TDD hơn, và còn nhiều lợi ích khác nữa.

Đọc thêm: 
- Code theo hướng TDD là viết test trước, rồi viết code sau, viết code để test chạy được, viết code để test pass được, viết code để test fail được, viết code để test cover được, viết code để test có ý nghĩa, viết code để test có giá trị, viết code để test có tác dụng, viết code để test có hiệu quả, viết code để test có độ tin cậy, viết code để test có độ chính xác, viết code để test có độ bao phủ, viết code để test có độ hiệu quả, viết code để test có độ hiệu suất, viết code để test có độ tin cậy, viết code để test có độ chính xác, viết code để test có độ bao phủ, viết code để test có độ hiệu quả, viết code để test có độ hiệu suất.

Đọc thử sách: Design Patterns của GoF (Gang of Four)

- CỨ GẶP BÀI TOÁN X, THÌ ÁP DỤNG NHỮNG CÔNG THỨC NÀY, CÁCH THIẾT KẾ Y TRONG CUỐN NÀY, GỒM NHIỀU CLASS VÀ INTERFACE
        GIÚP BÀI TOÁN CỦA CHÚNG TA ĐẸP, MLEM Ở GỐC ĐỘ, DỄ BẢO TRÌ, NÂNG CẤP, MỞ RỘNG, 
        MÀ KO KHIẾN CODE CŨ BỊ ẢNH HƯỞNG QUÁ NHIỀU

- 23 MẪU THIẾT KẾ CLASS NỔI TIẾNG ÁP DỤNG CHO NHỮNG BÀI TOÁN ĐẶC TRƯNG, PHỔ BIẾN 
- ÁP DỤNG TRIỆT ĐỂ SOLID 

>>>> TIỆM CẬN CÁI NGHỀ KIẾN TRÚC PHẦN MỀM, SOFTWARE ARCHITECT/SOLUTION ARCHITECT 

? Lợi ích của việc dùng Loose Coupling là gì?
- Dễ bảo trì, dễ nâng cấp, dễ mở rộng, dễ test, dễ mock, dễ stub, dễ fake, dễ unit test, dễ integration test, dễ end-to-end test, dễ viết code theo hướng TDD hơn, và còn nhiều lợi ích khác nữa.
