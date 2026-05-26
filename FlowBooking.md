Hãy cùng phân tích tại sao câu lệnh này của em lại chống được 2 thread vào **cùng một mili-giây tuyệt đối (song song thực sự)** mà không cần Unique:

SQL

```CSharp
UPDATE generated_slots
SET status = 'BOOKED'
WHERE id = X AND status = 'AVAILABLE';
```

### 1. Bản chất dưới góc nhìn Database: Không có gì là "Song Song"

Khi 2 thread (Khách A và Khách B) cùng bắn lệnh `UPDATE` xuống cùng một dòng `X` vào cùng một thời điểm:

- **Bước 1: DB xếp hàng (Locking)** Dù CPU của em có nhiều nhân đến mấy, khi đụng đến việc ghi (`Write/UPDATE`) vào **cùng một dòng vật lý trên ổ cứng**, Database (SQL Server, PostgreSQL, MySQL) sẽ ép hai lệnh này phải xếp hàng. Thằng nào chạm vào ví dòng `X` trước (dù chỉ nhanh hơn một phần tỷ giây), DB sẽ phát cho nó một cái **Exclusive Lock (Khóa độc quyền)** trên dòng đó.
- **Bước 2: Thread A thực thi** Thread A giữ khóa. Nó kiểm tra điều kiện: `id = X AND status = 'AVAILABLE'`.
  - Điều kiện **ĐÚNG**. DB đổi trạng thái dòng đó sang `BOOKED`.
  - Thread A hoàn thành, DB nhả Lock ra. Kết quả trả về: `1 row affected` (Đặt sân thành công).
- **Bước 3: Thread B thực thi (Ngay lập tức sau khi Thread A nhả Lock)** Lúc này Lock được giao cho Thread B. Thread B nhảy vào dòng `X` và kiểm tra điều kiện y hệt: `id = X AND status = 'AVAILABLE'`.
  - Nhưng hụt mất rồi! Trạng thái lúc này đã bị Thread A đổi thành `BOOKED` ở Bước 2.
  - Điều kiện `WHERE` bị **SAI**. DB lẳng lặng bỏ qua, không update gì cả.
  - Kết quả trả về: `0 row affected` (Đặt sân thất bại).

> **Kết luận:** Nhờ có điều kiện `status = 'AVAILABLE'` nằm ngay trong câu lệnh `UPDATE`, chính Database tự biến mình thành một cái "tấm khiên" chặn đứng Thread B mà Backend C# không cần viết một dòng logic check nào cả. Người ta gọi đây là kỹ thuật **Optimistic Concurrency Control (Kiểm soát song song lạc quan)**.

### 2. Tại sao không cần Unique?

- **Unique Constraint** thường được dùng để đảm bảo rằng không có hai dòng nào trong bảng có cùng một giá trị ở một cột nào đó (ví dụ: `email` phải là duy nhất). Trong trường hợp này, chúng ta không cần đảm bảo rằng `id` là duy nhất (vì nó đã là Primary Key), mà chỉ cần đảm bảo rằng trạng thái của dòng đó phải là `AVAILABLE` trước khi đặt thành `BOOKED`.
- Câu lệnh `UPDATE` với điều kiện `WHERE` đã đủ để đảm bảo rằng chỉ có một thread có thể thành công trong việc đặt sân vào cùng một thời điểm. Nếu có thêm Unique Constraint, nó sẽ không giúp ích gì thêm mà còn làm phức tạp hơn quá trình cập nhật.

### 3. Tóm lại:

- Câu lệnh `UPDATE` với điều kiện `WHERE` đã đủ để đảm bảo rằng chỉ một thread có thể đặt sân thành công vào cùng một thời điểm, nhờ vào cơ chế locking của Database.
- Không cần Unique Constraint vì chúng ta không cần đảm bảo tính duy nhất của một cột nào đó, mà chỉ cần đảm bảo rằng trạng thái của dòng phải là `AVAILABLE` trước khi có thể đặt thành `BOOKED`.

```CSharp
// Pseudo-code minh họa
public bool BookSlot(int slotId)
{
    string sql = "UPDATE generated_slots SET status = 'BOOKED' WHERE id = @slotId AND status = 'AVAILABLE'";
    int rowsAffected = ExecuteNonQuery(sql, new { slotId });
    return rowsAffected > 0; // Nếu > 0, đặt sân thành công
}
```
